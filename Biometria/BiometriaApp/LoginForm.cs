using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biometria;
using Biometria.Models;
using Sound;

namespace BiometriaApp
{
    public partial class LoginForm : Form
    {

        private Dictionary<string, MinutiaesResult> _fingerprintRepository;
        private Dictionary<string, List<Double>> _soundRepository;
        public Dictionary<string, MinutiaesResult> FingerprintRepository { get => _fingerprintRepository; set => _fingerprintRepository = value; }
        public Dictionary<string, List<double>> SoundRepository { get => _soundRepository; set => _soundRepository = value; }

        public LoginForm()
        {
            InitializeComponent();
            _fingerprintRepository = new Dictionary<string, MinutiaesResult>();
            _soundRepository = new Dictionary<string, List<double>>();
            
        }


        private void LoginButton_Click(object sender, EventArgs e)
        {
            if(_fingerprintRepository.ContainsKey(Login.Text) && _soundRepository.ContainsKey(Login.Text))
            {
                bool fingerFit = false;
                bool soundFit = false;
                string imagePath = Biometria.Helpers.Path.GetImagePath();
                MinutiaesResult minutiaesResult = Fingerprints.fingerPrints(imagePath);
                if (minutiaesResult != null)
                  fingerFit =  Fingerprints.BestFit(_fingerprintRepository[Login.Text], minutiaesResult);

                if(fingerFit)
                {
                    imagePath = Sound.Helpers.Path.GetSoundPath();
                    List<Double> MFCCResult = Sound.Sound.MFCCSound(imagePath);
                    if (MFCCResult != null)
                        soundFit = Sound.Sound.SoundFit(_soundRepository[Login.Text], MFCCResult);
                    if(soundFit)
                    {
                        MessageBox.Show("You are sign in.");
                        MainForm form = new MainForm();
                        form.Login = Login.Text;
                        form.FingerprintRepository = this._fingerprintRepository;
                        form.SoundRepository = this._soundRepository;
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("Your sound dosen't fit to this login.");
                    }
                }
                else
                {
                    MessageBox.Show("Your fingerprint isn't fit for this login.");
                }
               

            }
            else 
            {
                MessageBox.Show("Not found account. First you must sign up.");
            }
                    
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            string imagePath = Biometria.Helpers.Path.GetImagePath();
            MinutiaesResult minutiaesResult = Fingerprints.fingerPrints(imagePath);
            if (minutiaesResult != null)
                _fingerprintRepository.Add(Login.Text, minutiaesResult);

            imagePath = Sound.Helpers.Path.GetSoundPath();
            List<Double> MFCCResult = Sound.Sound.MFCCSound(imagePath);
            if(MFCCResult != null)
            {
                _soundRepository.Add(Login.Text, MFCCResult);
            }
        }
    }
}
