using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sound.Helpers
{
    class AudioHelper
    {
        int sampleRate = 0;

        private const int K = 30;
        private const int Dis = 100;

        public Tuple<double[], int, TimeSpan> openWav(string filename, out short[] sampleBuffer)
        {
            TimeSpan time = new TimeSpan();
            using (WaveFileReader reader = new WaveFileReader(filename))
            {
                sampleRate = reader.WaveFormat.SampleRate;
                time = reader.TotalTime;

                byte[] buffer = new byte[reader.Length];
                int read = reader.Read(buffer, 0, buffer.Length);
                sampleBuffer = new short[read / 2];
                Buffer.BlockCopy(buffer, 0, sampleBuffer, 0, read);
            }

            int WinodwSize = 1024;
            double[] result = new double[WinodwSize];
            int i = 0;
            foreach (short tmp in sampleBuffer)
            {
                result[i] = sampleBuffer[i];
                i++;
                if (i == WinodwSize)
                    break;
            }

            result = TriangleWindow(result);
            Complex[] resultComplex = new Complex[result.Length];
            for (int z = 0; z < result.Length; z++)
            {
                resultComplex[z] = result[z];
            }

            resultComplex = FFT(resultComplex);
            for (int z = 0; z < result.Length; z++)
            {
                result[z] =2 * Modulus(resultComplex[z].Real, resultComplex[z].Imaginary)/result.Length;
            }

            Filter(result);

            return new Tuple<double[], int, TimeSpan>(result, sampleRate, time);
        }


        public double[] TriangleWindow(double[] data)
        {
            double[] result = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = data[i] * (1 - (i * 1.0) / data.Length);
            }
            return result;
        }

        public static int BitReverse(int n, int bits)
        {
            int reversedN = n;
            int count = bits - 1;

            n >>= 1;
            while (n > 0)
            {
                reversedN = (reversedN << 1) | (n & 1);
                count--;
                n >>= 1;
            }

            return ((reversedN << count) & ((1 << bits) - 1));
        }

        public static Complex[] FFT(Complex[] buffer)
        {

            int bits = (int)Math.Log(buffer.Length, 2);
            for (int j = 1; j < buffer.Length; j++)
            {
                int swapPos = BitReverse(j, bits);
                if (swapPos <= j)
                {
                    continue;
                }
                var temp = buffer[j];
                buffer[j] = buffer[swapPos];
                buffer[swapPos] = temp;
            }




            for (int N = 2; N <= buffer.Length; N <<= 1)
            {
                for (int i = 0; i < buffer.Length; i += N)
                {
                    for (int k = 0; k < N / 2; k++)
                    {

                        int evenIndex = i + k;
                        int oddIndex = i + k + (N / 2);


                        Complex even = 0.0;

                        Complex odd = 0.0;

                        if (oddIndex < buffer.Length)
                        {
                            odd = buffer[oddIndex];
                        }
                        if (evenIndex < buffer.Length)
                        {
                            even = buffer[evenIndex];
                        }

                        double term = -2 * Math.PI * k / (double)N;
                        Complex exp = new Complex(Math.Cos(term), Math.Sin(term)) * odd;
                        if (evenIndex < buffer.Length)
                        {
                            buffer[evenIndex] = even + exp;
                        }
                        if (oddIndex < buffer.Length)
                        {
                            buffer[oddIndex] = even - exp;
                        }
                    }
                }
            }

                return buffer;
        }

        public double[][] Filter(double[] data)
        {
            double[][] result = new double[data.Length][];
            for (int i = 0; i<data.Length; i++)
            {
                result[i] = new double[K];
            }
            for(int k=0; k<K;k++)
            {
                double ck = Ck(k + 1);
                double lk = Ck(k);
                double rk = Ck(k + 2);
                {
                    for(int i = 0; i< data.Length; i++)
                    {
                        double freq = i * (sampleRate * 1.0) / data.Length;
                        if (lk < freq && freq < ck)
                            result[i][k] = (freq - lk) / (ck - lk);
                        else if (ck < freq && freq < rk)
                            result[i][k] = (rk - freq) / (rk - ck);
                        else
                            result[i][k] = 0;
                    }
                }
            }
            return result;
        }

        private double Ni(double m)
        {
            return 700 * (Math.Pow(10, m / 2595) - 1); ;
        }

        private double Ck(double k)
        {
            return Ni(k * Dis);
        }

        private static double Modulus(double real, double imaginary)
        {
            return Math.Sqrt((real * real) + (imaginary * imaginary));
        }


        private const double LOG_2_DB = 8.6858896380650365530225783783321;
        private const double DB_2_LOG = 0.11512925464970228420089957273422;


        public static double LinearToDecibels(double lin)
        {
            return Math.Log(lin) * LOG_2_DB;
        }

        public static double DecibelsToLinear(double dB)
        {
            return Math.Exp(dB * DB_2_LOG);
        }

        public static double HztoMel(double hz)
        {
            return 2595 * Math.Log10(1 + hz / 700);
        }


        public static double MeltoHz(double mel)
        {
            return 700 * (Math.Pow(10, mel/2595) - 1);
        }
    }
}
