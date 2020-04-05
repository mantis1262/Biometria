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
                Bitmap grayBitmapImage = Effect.GrayMode(_originalBitmap);
                //grayBitmapImage = Effect.MedianFilter(grayBitmapImage, 9);
                Bitmap binarizationBitmapImage = Otsu.threshold(grayBitmapImage, Otsu.getOtsuThreshold(grayBitmapImage));
                binarizationBitmapImage = Effect.ClipBoundaries(binarizationBitmapImage, 10);
                binarizationImage.Image = binarizationBitmapImage;
                Bitmap thinningBitmapImage = Effect.Skeletonization(binarizationBitmapImage);
                thinningBitmapImage = Effect.ClipBoundaries(thinningBitmapImage, 10);
                thinningBitmapImage = Effect.RemoveBugPixels(thinningBitmapImage);
                thinningImage.Image = thinningBitmapImage;
                MinutiaesResult minutiaesResult = Effect.ExtractMinutiaes(thinningBitmapImage, 40, 300);
                // Center - purple
                // Termination - red (crossing number = 1)
                // Bifurcation - blue (crossing number = 3)
                Bitmap minutiaesBitmapImage = Effect.MarkMinutiaes(thinningBitmapImage, minutiaesResult);
                minutiaesImage.Image = minutiaesBitmapImage;
                MessageBox.Show(minutiaesResult.Minutiaes.Count.ToString(), "Liczba minucji", MessageBoxButtons.OK);
            }
        }

        private void LoadedImage_Click(object sender, EventArgs e)
        {

        }

        private void BinarizationImage_Click(object sender, EventArgs e)
        {

        }

        private void ThinningImage_Click(object sender, EventArgs e)
        {

        }

        private void MinutiaesImage_Click(object sender, EventArgs e)
        {

        }
    }
}
