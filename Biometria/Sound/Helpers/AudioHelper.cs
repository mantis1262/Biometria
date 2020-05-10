using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sound.Helpers
{
    class AudioHelper
    {


        private const short CHNL = 1;
        private const int SMPL_RATE = 16000;
        private const int BIT_PER_SMPL = 16;


        // convert two bytes to one double in the range -1 to 1
        static double bytesToDouble(byte firstByte, byte secondByte)
        {
            // convert two bytes to one short (little endian)
            int s = (secondByte << 8) | firstByte;
            // convert to range from -1 to (just below) 1
            return s / 32768.0;
        }

        // Returns left and right double arrays. 'right' will be null if sound is mono.
        public Tuple<double[], int, TimeSpan> openWav(string filename, out short[] sampleBuffer)
        {
            int sampleRate = 0;
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


            double[] result = new double[sampleBuffer.Length];
            int i = 0;
            foreach (short tmp in sampleBuffer)
            {
                result[i] = sampleBuffer[i];
                i++;
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

        public double[] dft(double[] data)
        {
            int n = data.Length;
            int m = n;
            double[] real = new double[n];
            double[] imag = new double[n];
            double[] result = new double[m];
            double pi_div = 2.0 * Math.PI / n;
            for (int w = 0; w < m; w++)
            {
                double a = w * pi_div;
                for (int t = 0; t < n; t++)
                {
                    real[w] += data[t] * Math.Cos(a * t);
                    imag[w] += data[t] * Math.Sin(a * t);
                }
                result[w] = 2 * Math.Sqrt(real[w] * real[w] + imag[w] * imag[w]) / n;
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
        private static double Modulus(double real, double imaginary)
        {
            return Math.Sqrt((real * real) + (imaginary * imaginary));
        }
    }
}
