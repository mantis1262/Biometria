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
                grayBitmapImage = Effect.MedianFilter(grayBitmapImage, 3);
                Bitmap binarizationBitmapImage = Otsu.threshold(grayBitmapImage, Otsu.getOtsuThreshold(grayBitmapImage));
                binarizationBitmapImage = Effect.ClipBoundaries(binarizationBitmapImage, 10);
                binarizationImage.Image = binarizationBitmapImage;
                Bitmap thinningBitmapImage = Effect.Skeletonization(binarizationBitmapImage);
                thinningBitmapImage = Effect.ClipBoundaries(thinningBitmapImage, 10);
                thinningBitmapImage = Effect.RemoveBugPixels(thinningBitmapImage);
                thinningImage.Image = thinningBitmapImage;
                MinutiaesResult minutiaesResult = Effect.ExtractMinutiaes(thinningBitmapImage, 40, 300, 2);
                // Center - purple
                // Termination - red (crossing number = 1)
                // Bifurcation - blue (crossing number = 3)
                Bitmap minutiaesBitmapImage = Effect.MarkMinutiaes(thinningBitmapImage, minutiaesResult);
                minutiaesImage.Image = minutiaesBitmapImage;
                MessageBox.Show(minutiaesResult.Minutiaes.Count.ToString(), "Liczba minucji", MessageBoxButtons.OK);


                Bitmap grayBitmapImage2 = Effect.GrayMode(BitmapFactory.CreateBitmap(Path.GetImagePath()));
                grayBitmapImage2 = Effect.MedianFilter(grayBitmapImage2, 3);
                Bitmap binarizationBitmapImage2 = Otsu.threshold(grayBitmapImage2, Otsu.getOtsuThreshold(grayBitmapImage2));
                binarizationBitmapImage2 = Effect.ClipBoundaries(binarizationBitmapImage2, 10);
                Bitmap thinningBitmapImage2 = Effect.Skeletonization(binarizationBitmapImage2);
                thinningBitmapImage2 = Effect.ClipBoundaries(thinningBitmapImage2, 10);
                thinningBitmapImage2 = Effect.RemoveBugPixels(thinningBitmapImage2);
                MinutiaesResult minutiaesResult2 = Effect.ExtractMinutiaes(thinningBitmapImage2, 40, 300, 2);
               // Bitmap minutiaesBitmapImage2 = Effect.MarkMinutiaes(thinningBitmapImage2, minutiaesResult2);
                // thinningImage.Image = minutiaesBitmapImage2;
                double alfa = 0;
                double maxalfa = 0;
                double o;
                double maxo = 0;
                do
                {
                    MinutiaesResult temp = Effect.Rotation(minutiaesResult2, alfa, (minutiaesResult.CenterX - minutiaesResult2.CenterX), (minutiaesResult.CenterY - minutiaesResult2.CenterY));
                    o = 0;

                    foreach (Minutiae minutiae2 in temp.Minutiaes)
                    {
                        foreach (Minutiae minutiae in minutiaesResult.Minutiaes)
                        {
                            if (minutiae.ChceckFit(minutiae2, 15, 30)) // sprawdzana minucjia, akceptowalna odleglosci i roznica katów
                            {
                                o += 1;
                                break;
                            }
                        }
                    }

                    if(o > maxo)
                    {
                        maxo = o;
                        maxalfa = alfa;
                    }

                    alfa += 1;
                }
                while (alfa <= 90);


                minutiaesResult2 = Effect.Rotation(minutiaesResult2, maxalfa, (minutiaesResult.CenterX - minutiaesResult2.CenterX), (minutiaesResult.CenterY - minutiaesResult2.CenterY));
                o = maxo;
                Bitmap result = new Bitmap(thinningBitmapImage2.Width * 2, thinningBitmapImage2.Height * 2);
               // minutiaesResult2.CenterX = (int)((Math.Cos(maxalfa) * minutiaesResult2.CenterX - Math.Sin(maxalfa) * minutiaesResult2.CenterX + (minutiaesResult.CenterX - minutiaesResult2.CenterX)));
               // minutiaesResult2.CenterY = (int)((Math.Cos(maxalfa) * minutiaesResult2.CenterY - Math.Sin(maxalfa) * minutiaesResult2.CenterY + (minutiaesResult.CenterY - minutiaesResult2.CenterY)));
                Bitmap minutiaesBitmapImage2 = Effect.MarkMinutiaes(result, minutiaesResult2);
                 thinningImage.Image = minutiaesBitmapImage2;
                
                //if(o >= minutiaesResult2.Minutiaes.Count)
                    MessageBox.Show(maxo.ToString() +
                         "  " + maxalfa,
                         "Licza dopasowanych minucji", MessageBoxButtons.OK);

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
