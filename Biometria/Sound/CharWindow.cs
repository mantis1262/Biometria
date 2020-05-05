using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

            Tuple<double[], int,TimeSpan> wave = audioHelper.openWav(Path.GetSoundPath(), out left);
            double[] result = wave.Item1;
            double sampleRate = Convert.ToDouble(wave.Item2);
            int seconds = wave.Item3.Seconds;
            Histogram.Series.Clear();
            Histogram.Series.Add("Value");
            Histogram.Series["Value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            Histogram.Series["Value"].MarkerSize = 2;

            Histogram.ChartAreas[0].AxisX.Maximum = seconds;
           

            for (int i = 0; i < result.Count(); i++)
            {
                Histogram.Series["Value"].Points.AddXY(i / sampleRate, result[i] / sampleRate);
              
            }

            Histogram.ChartAreas[0].AxisY.Maximum = 1;
            Histogram.ChartAreas[0].AxisY.Minimum = -1;
        }
    }
}
