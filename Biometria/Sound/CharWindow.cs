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
            double[] result = audioHelper.openWav(Path.GetSoundPath(), out left); 
            Histogram.Series.Clear();
            Histogram.Series.Add("Value");
            Histogram.Series["Value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            for (int i = 0; i < result.Count(); i++)
                Histogram.Series["Value"].Points.AddXY(i/16000.0f, left[i]);
        }
    }
}
