using Biometria;
using Biometria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BiometriaApp
{
    public partial class MainForm : Form
    {
        private Dictionary<string, MinutiaesResult> _fingerprintRepository;
        private Dictionary<string, List<Double>> _soundRepository;
        public Dictionary<string, MinutiaesResult> FingerprintRepository { get => _fingerprintRepository; set => _fingerprintRepository = value; }
        public Dictionary<string, List<double>> SoundRepository { get => _soundRepository; set => _soundRepository = value; }
        List<Przelew> przelewy;
        decimal stanKonta = 1000m;
        public string Login;
        public MainForm()
        {
            InitializeComponent();
            przelewy = new List<Przelew>();
            lbStanKonta.Text = stanKonta.ToString();




        }


        private void RefreshView()
        {
            foreach (Przelew przelew in przelewy)
            {
                ListViewItem item = new ListViewItem(przelewy[0].Odbiorca);

                item.SubItems.Add(przelewy[0].Rachunek);
                item.SubItems.Add(przelewy[0].Kwota.ToString());

                listView1.Items.Add(item);
            }
            lbStanKonta.Text = stanKonta.ToString();
        }

        private void btnPrzelew_Click(object sender, EventArgs e)
        {
            grpPrzelewu.Visible = true;
        }

        private void btnAnuluj_Click(object sender, EventArgs e)
        {
            grpPrzelewu.Visible = false;
            edKwota.Text = "";
            edOdbiorca.Text = "";
            edRach.Text = "";
        }

        private void btnPotwierdz_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("W następnym kroku zweryfikujemy Twój odcisk palca...", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                string imagePath = Biometria.Helpers.Path.GetImagePath();
                MinutiaesResult minutiaesResult = Fingerprints.fingerPrints(imagePath);
                if (minutiaesResult != null)
                {
                    bool ifGood = Fingerprints.BestFit(_fingerprintRepository[Login], minutiaesResult);

                    if (ifGood)
                    {
                        przelewy.Add(new Przelew(edOdbiorca.Text, edRach.Text, Convert.ToDecimal(edKwota.Text)));
                        MessageBox.Show("Autoryzacja pomyślna, przelew zrealizowany.");
                        stanKonta -= Convert.ToDecimal(edKwota.Text);
                        btnAnuluj.PerformClick();
                        RefreshView();
                    }
                }

            }
        }
    }
}
