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
        public void openWav(string filename, out double[] left)
        {
            byte[] wav = File.ReadAllBytes(filename);

            // Get past all the other sub chunks to get to the data subchunk:
            int pos = 12;   // First Subchunk ID from 12 to 16

            // Keep iterating until we find the data chunk (i.e. 64 61 74 61 ...... (i.e. 100 97 116 97 in decimal))
            while (!(wav[pos] == 100 && wav[pos + 1] == 97 && wav[pos + 2] == 116 && wav[pos + 3] == 97))
            {
                pos += 4;
                int chunkSize = wav[pos] + wav[pos + 1] * 256 + wav[pos + 2] * 65536 + wav[pos + 3] * 16777216;
                pos += 4 + chunkSize;
            }
            pos += 8;

            // Pos is now positioned to start of actual sound data.
            int samples = (wav.Length - pos) / 2;     // 2 bytes per sample (16 bit sound mono)

            // Allocate memory (right will be null if only mono sound)
            left = new double[samples];


            // Write to double array/s:
            int i = 0;
            while (pos < wav.Length)
            {
                left[i] = bytesToDouble(wav[pos], wav[pos + 1]);
                left[i] -= 1;
                pos += 2;
                i++;
            }

           left = dft(left);
        }

        public double[] dft(double[] data)
        {
            int n = data.Length;
            int m = n;// I use m = n / 2d;
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
                result[w] = Math.Sqrt(real[w] * real[w] + imag[w] * imag[w]) / n;
            }
            return result;
        }

    } 
}
