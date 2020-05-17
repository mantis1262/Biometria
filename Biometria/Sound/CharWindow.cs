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

        const double MAXDIS = 12.0;
        public CharWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AudioHelper audioHelper = new AudioHelper();

            short[] sample1;

            Tuple<double[], int, TimeSpan> wave = audioHelper.openWav(Path.GetSoundPath(), out sample1);
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

                //if (value[i] > 0.008)
                //{
                //    double tmp = freq[i];
                //}
            }

            double[][] FilterValue =  audioHelper.Filter(value.Length);
            double[] SumFilter = audioHelper.FitrSum(FilterValue, value);
            double[] MFCC = audioHelper.MFCC(SumFilter);

            CharWindow charWindow2 = new CharWindow();
            charWindow2.Histogram.Series.Clear();
            charWindow2.Histogram.Series.Add("MFCC1");
            charWindow2.Histogram.Series["MFCC1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            charWindow2.Histogram.Series["MFCC1"].MarkerSize = 2;

            for (int i = 0; i < MFCC.Count(); i++)
            {
                charWindow2.Histogram.Series["MFCC1"].Points.AddXY(i, MFCC[i]);
            }

            charWindow2.button1.Visible = false;
            charWindow2.Show();

            short[] sample2;
            Tuple<double[], int, TimeSpan> wave2 = audioHelper.openWav(Path.GetSoundPath(), out sample2);

            double[] result2 = wave2.Item1;
            double sampleRate2 = Convert.ToDouble(wave2.Item2);

            double[] time2 = new double[result2.Length];
            double[] value2 = new double[result2.Length];
            double[] freq2 = new double[result2.Length];

            for (int i = 0; i < result2.Count(); i++)
            {
                value2[i] = result2[i] / sampleRate2;
                time2[i] = i / sampleRate2;
                freq2[i] = i * sampleRate2 / result2.Length;

            }

            double[][] FilterValue2 = audioHelper.Filter(value2.Length);
            double[] SumFilter2 = audioHelper.FitrSum(FilterValue2, value2);
            double[] MFCC2 = audioHelper.MFCC(SumFilter2);

            charWindow2.Histogram.Series.Add("MFCC2");
            charWindow2.Histogram.Series["MFCC2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            charWindow2.Histogram.Series["MFCC2"].MarkerSize = 2;

            for (int i = 0; i < MFCC2.Count(); i++)
            {
                charWindow2.Histogram.Series["MFCC2"].Points.AddXY(i, MFCC2[i]);
            }

            double dis = audioHelper.euclides(MFCC2, MFCC);
            if (dis < MAXDIS)
                MessageBox.Show("Dopasowano.");
        }
    }
}
