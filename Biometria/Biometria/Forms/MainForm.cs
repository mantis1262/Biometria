using ImageSoundProcessing.Factories;
using Biometria.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biometria.Models;

namespace Biometria
{
    public partial class MainForm : Form
    {
        private string _imagePath;
        private Bitmap _originalBitmap;

        public string ImagePath { get => _imagePath; set => _imagePath = value; }

        public Bitmap OriginalBitmap { get => _originalBitmap; set => _originalBitmap = value; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            string imagePath = Path.GetImagePath();
            if (!imagePath.Equals(""))
            {
                _imagePath = imagePath;
                _originalBitmap = BitmapFactory.CreateBitmap(imagePath);
                _originalBitmap = Effect.GrayMode(_originalBitmap);
                //_originalBitmap = Effect.MedianFilter(_originalBitmap, 4);
                _originalBitmap = Otsu.threshold(_originalBitmap,Otsu.getOtsuThreshold(_originalBitmap));
                _originalBitmap = Effect.ClipBoundaries(_originalBitmap, 255, 10);
                _originalBitmap = Effect.Skeletonization(_originalBitmap);
                _originalBitmap = Effect.ClipBoundaries(_originalBitmap, 255, 10);
                MinutiaesResult minutiaesResult = Effect.ExtractMinutiaes(_originalBitmap, 40, 300);
                // Center - purple
                // Termination - red (crossing number = 1)
                // Bifurcation - blue (crossing number = 3)
                _originalBitmap = Effect.MarkMinutiaes(_originalBitmap, minutiaesResult);
                loadedImage.Image = _originalBitmap;
                MessageBox.Show(minutiaesResult.Minutiaes.Count.ToString(), "Liczba minucji", MessageBoxButtons.OK);
            }
        }

        private void LoadedImage_Click(object sender, EventArgs e)
        {

        }
    }
}
