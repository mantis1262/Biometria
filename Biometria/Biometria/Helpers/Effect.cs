using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria.Helpers
{
    class Effect
    {
        public static Bitmap GrayMode(Bitmap original)
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

            for (int i = 0; i < originalBitmapLock.Width; i++)
            {
                for (int j = 0; j < originalBitmapLock.Height; j++)
                {
                    Color temp = originalBitmapLock.GetPixel(i, j);
                    int gray = (int)(0.299 * temp.R + 0.587 * temp.G + 0.114 * temp.B);
                    processedBitmapLock.SetPixel(i, j, Color.FromArgb(temp.A, (byte)gray, (byte)gray, (byte)gray));
                }
            }

            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }
    }
}
