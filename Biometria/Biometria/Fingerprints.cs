using Biometria.Factories;
using Biometria.Helpers;
using Biometria.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria
{
    public static class Fingerprints
    {

        public static MinutiaesResult fingerPrints(string imagePath)
        {
            if (!imagePath.Equals(""))
            {
                Bitmap _originalBitmap = BitmapFactory.CreateBitmap(imagePath);
                Bitmap grayBitmapImage = Effect.GrayMode(_originalBitmap);
                grayBitmapImage = Effect.MedianFilter(grayBitmapImage, 3);
                Bitmap binarizationBitmapImage = Otsu.threshold(grayBitmapImage, Otsu.getOtsuThreshold(grayBitmapImage));
                binarizationBitmapImage = Effect.ClipBoundaries(binarizationBitmapImage, 10);
                Bitmap thinningBitmapImage = Effect.Skeletonization(binarizationBitmapImage);
                thinningBitmapImage = Effect.ClipBoundaries(thinningBitmapImage, 10);
                thinningBitmapImage = Effect.RemoveBugPixels(thinningBitmapImage);
                MinutiaesResult minutiaesResult = Effect.ExtractMinutiaes(thinningBitmapImage, 30, 6, 10, 70);
                return minutiaesResult;
            }
            return null;

        }


        public static bool BestFit(MinutiaesResult dMinutiaes, MinutiaesResult fitMinutiaes)
        {

            double alfa = 0;
            double maxalfa = 0;
            double o;
            double maxo = 0; 
            do
            {
                MinutiaesResult temp = Effect.Rotation(fitMinutiaes, alfa, (dMinutiaes.CenterX - fitMinutiaes.CenterX), (dMinutiaes.CenterY - fitMinutiaes.CenterY));
                o = 0;

                foreach (Minutiae minutiae2 in temp.Minutiaes)
                {
                    foreach (Minutiae minutiae in dMinutiaes.Minutiaes)
                    {
                        if (minutiae2.ChceckFit(minutiae, 15, 30))
                        {
                            o += 1;
                            break;
                        }
                    }
                }

                if (o > maxo)
                {
                    maxo = o;
                    maxalfa = alfa;
                }

                alfa += 1;
            }
            while (alfa <= 90);


          MinutiaesResult  minutiaesResult2 = Effect.Rotation(fitMinutiaes, maxalfa, (dMinutiaes.CenterX - fitMinutiaes.CenterX), (dMinutiaes.CenterY - fitMinutiaes.CenterY));
            o = maxo;

            if (o >= 0.6 * minutiaesResult2.Minutiaes.Count)
                return true;

            return false;
        }
    }
}
