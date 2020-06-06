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
    }
}
