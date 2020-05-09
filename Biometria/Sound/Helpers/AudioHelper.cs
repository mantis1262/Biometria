using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public Tuple<double[],int,TimeSpan> openWav(string filename, out short[] sampleBuffer)
        {
            int sampleRate = 0;
            TimeSpan time = new TimeSpan();
            using (WaveFileReader reader = new WaveFileReader(filename))
            {
                sampleRate = reader.WaveFormat.SampleRate;
                time = reader.TotalTime;
               
                byte[] buffer = new byte[reader.Length];
                int read = reader.Read(buffer, 0, buffer.Length);
                sampleBuffer = new short[read/2];
                Buffer.BlockCopy(buffer, 0, sampleBuffer, 0, read);
            }

            
            double[] result = new double[sampleBuffer.Length];
            int i = 0;
            foreach (short tmp in sampleBuffer)
            {
                result[i] = sampleBuffer[i];
                i++;
            }
            result = dft(sampleBuffer);
            return new Tuple<double[],int,TimeSpan>( result,sampleRate,time);
        }

        public double[] dft(short[] data)
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
                result[w] = 2* Math.Sqrt(real[w] * real[w] + imag[w] * imag[w]) / n;
            }
            return result;
        }

    } 
}
