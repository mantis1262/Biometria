using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Biometria.Helpers
{
   class Otsu
    {
        // function is used to compute the q values in the equation
        private static float Px(int init, int end, int[] hist)
        {
            int sum = 0;
            int i;
            for (i = init; i <= end; i++)
                sum += hist[i];

            return (float)sum;
        }

        // function is used to compute the mean values in the equation (mu)
        private static float Mx(int init, int end, int[] hist)
        {
            int sum = 0;
            int i;
            for (i = init; i <= end; i++)
                sum += i * hist[i];

            return (float)sum;
        }

        // finds the maximum element in a vector
        private static int findMax(float[] vec, int n)
        {
            float maxVec = 0;
            int idx=0;
            int i;

            for (i = 1; i < n - 1; i++)
            {
                if (vec[i] > maxVec)
                {
                    maxVec = vec[i];
                    idx = i;
                }
            }
            return idx;
        }

        // simply computes the image histogram
        private static void getHistogram(LockBitmap p, int w, int h, int[] hist)
        {
            hist.Initialize();
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    hist[p.GetPixel(i,j).R]++;
                }
            }
        }

        // find otsu threshold
        public static int getOtsuThreshold(Bitmap original)
        {
            byte t=0;
	        float[] vet=new float[256];
            int[] hist=new int[256];
            vet.Initialize();

	        float p1,p2,p12;
	        int k;

            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);

            getHistogram(originalBitmapLock, originalBitmapLock.Width, originalBitmapLock.Height, hist);
                // loop through all possible t values and maximize between class variance
                for (k = 1; k != 255; k++)
                {
                    p1 = Px(0, k, hist);
                    p2 = Px(k + 1, 255, hist);
                    p12 = p1 * p2;
                    if (p12 == 0) 
                        p12 = 1;
                    float diff=(Mx(0, k, hist) * p2) - (Mx(k + 1, 255, hist) * p1);
                    vet[k] = (float)diff * diff / p12;
                }
            originalBitmapLock.UnlockBits();
            t = (byte)findMax(vet, 256);

            return t;
        }


        public static Bitmap threshold(Bitmap original, int thresh)
        {

            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

                int h= originalBitmapLock.Height;
                int w = originalBitmapLock.Width;

                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                    if (originalBitmapLock.GetPixel(i, j).R > (byte)thresh)
                        processedBitmapLock.SetPixel(i, j,Color.FromArgb(255,255,255,255));
                    else processedBitmapLock.SetPixel(i, j, Color.FromArgb(255,0,0,0));
                    }
                }
            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }
    }
}

