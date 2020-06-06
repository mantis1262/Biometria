using Sound.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Sound
{
    public static class Sound
    {

        const double MAXDIS = 12.0;

        public static List<double> MFCCSound(string imagePath)
        {
            if (imagePath != null)
            {
                short[] sample1;
                AudioHelper audioHelper = new AudioHelper();
                Tuple<double[], int, TimeSpan> wave = audioHelper.openWav(imagePath, out sample1);
                double[] result = wave.Item1;
                double sampleRate = Convert.ToDouble(wave.Item2);

                double[] value = new double[result.Length];

                for (int i = 0; i < result.Count(); i++)
                {
                    value[i] = result[i] / sampleRate;
                }

                double[][] FilterValue = audioHelper.Filter(value.Length);
                double[] SumFilter = audioHelper.FitrSum(FilterValue, value);
                double[] MFCC = audioHelper.MFCC(SumFilter);

                return MFCC.ToList();
            }
            return null;
        }

        public static bool SoundFit(List<double> dMFCC, List<double> fitMFCC)
        {
            AudioHelper audioHelper = new AudioHelper();
            double dis = audioHelper.euclides(fitMFCC.ToArray(), dMFCC.ToArray());
            if (dis < MAXDIS)
                return true;
            return false;
        }
    }
}
