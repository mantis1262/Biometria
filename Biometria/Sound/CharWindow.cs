using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Dsp;
using Sound.Helpers;

namespace Sound
{
    public partial class CharWindow : Form
    {
        public CharWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AudioHelper audioHelper = new AudioHelper();

            short[] left;

            Tuple<double[], int, TimeSpan> wave = audioHelper.openWav(Path.GetSoundPath(), out left);
            double[] result = wave.Item1;
            //result = audioHelper.TriangleWindow(result);
            double sampleRate = Convert.ToDouble(wave.Item2);
            int seconds = wave.Item3.Seconds;
            Histogram.Series.Clear();
            Histogram.Series.Add("Value");
            Histogram.Series["Value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            Histogram.Series["Value"].MarkerSize = 2;

            //Histogram.ChartAreas[0].AxisX.Maximum = seconds;
            //  Histogram.ChartAreas[0].AxisX.Minimum = 0;
            // Histogram.ChartAreas[0].AxisY.Maximum = 0.1;
            // Histogram.ChartAreas[0].AxisY.Minimum = -0.1;

            double[] time = new double[result.Length];
            double[] value = new double[result.Length];
            double[] freq = new double[result.Length];

            for (int i = 0; i < result.Count(); i++)
            {
                value[i] = result[i] / sampleRate;
                time[i] = i / sampleRate;
                freq[i] = i * sampleRate / result.Length;
                Histogram.Series["Value"].Points.AddXY(freq[i], value[i]);

                if (value[i] > 0.008)
                {
                    double tmp = freq[i];
                }
            }

            ;

            /// badanie częstotliwości
            int licz = 0;
            double[] times = new double[2];
            for (int i = 0; i < result.Count() - 1; i++)
            {
                if (value[i] > 0 && value[i + 1] < 0)
                {
                    times[licz] = time[i];
                    licz++;
                }
                if (licz == 2)
                {
                    break;
                }
            }

            double okres = times[1] - times[0];
            double f = 1 / okres;
            double df = sampleRate / result.Length / 2;

        }
    }
}
