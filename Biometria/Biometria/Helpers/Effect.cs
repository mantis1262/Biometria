using Biometria.Models;
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

        public static Bitmap Binarization(Bitmap original, int granica)
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
                    if (originalBitmapLock.GetPixel(i, j).R > granica)
                        processedBitmapLock.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                    else
                        processedBitmapLock.SetPixel(i, j, Color.FromArgb(255, 0, 0, 0));
                }
            }

            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }

        public static Bitmap Skeletonization(Bitmap original)
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            Bitmap originalToProcessBmp = new Bitmap(original.Width + 2, original.Height + 2);
            LockBitmap originalToProcessBitmapLock = new LockBitmap(originalToProcessBmp);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);

            List<int> fours = new List<int>() { 3, 6, 7, 12, 14, 15, 24, 28, 30, 48, 56, 60, 96, 112, 120, 129, 131, 135, 192, 193, 195, 224, 225, 240 };
            List<int> cuts = new List<int>() { 3, 5, 7, 12, 13, 14, 15, 20, 21, 22, 23, 28, 29, 30, 31, 48, 52, 53, 54, 55, 56, 60, 61, 62, 63, 65, 67, 69,
                71, 77, 79, 80, 81, 83, 84, 85, 86, 87, 88, 89, 91, 92, 93, 94, 95, 97, 99, 101, 103, 109, 111, 112, 113, 115, 116, 117, 118, 119, 120, 121,
                123, 124, 125, 126, 127, 131, 133, 135, 141, 143, 149, 151, 157, 159, 181, 183, 189, 191, 192, 193, 195, 197, 199, 205, 207, 208, 209, 211,
                212, 213, 214, 215, 216, 217, 219, 220, 221, 222, 223, 224, 225, 227, 229, 231, 237, 239, 240, 241, 243, 244, 245, 246, 247, 248, 249, 251,
                252, 253, 254, 255 };
            int[,] checker = new int[3, 3] { { 128, 64, 32 }, { 1, 0, 16 }, { 2, 4, 8 } };
            int sumChecker = 0;
            foreach (int param in checker)
            {
                sumChecker += param;
            }

            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);
            originalToProcessBitmapLock.LockBits(ImageLockMode.ReadWrite);

            for (int i = 0; i < originalToProcessBitmapLock.Width; i++)
            {
                for (int j = 0; j < originalToProcessBitmapLock.Height; j++)
                {
                    if (i == 0 || i == originalToProcessBitmapLock.Width - 1 || j == 0 || j == originalToProcessBitmapLock.Height - 1)
                    {
                        originalToProcessBitmapLock.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                    }
                    else
                    {
                        originalToProcessBitmapLock.SetPixel(i, j, originalBitmapLock.GetPixel(i - 1, j - 1));
                    }


                }
            }
            for (int i = 0; i < processedBitmapLock.Width; i++)
            {
                for (int j = 0; j < processedBitmapLock.Height; j++)
                {
                    processedBitmapLock.SetPixel(i,j,Color.FromArgb(255,255,255,255));

                }
            }



                    bool ifChanged = false;

            do
            {

                for (int i = 0; i < originalBitmapLock.Width; i++)
                {
                    for (int j = 0; j < originalBitmapLock.Height; j++)
                    {
                        if (originalBitmapLock.GetPixel(i, j).R == 0)
                        {
                            if (originalToProcessBitmapLock.GetPixel(i - 1, j).R == 255 || originalToProcessBitmapLock.GetPixel(i + 1, j).R == 255
                                || originalToProcessBitmapLock.GetPixel(i, j - 1).R == 255 || originalToProcessBitmapLock.GetPixel(i, j + 1).R == 255)
                            {
                                processedBitmapLock.SetPixel(i, j, Color.FromArgb(255, 2, 2, 2));


                                int value = 0;
                                for (int n = -1; n <= 1; n++)
                                {
                                    for (int m = -1; m <= 1; m++)
                                    {
                                        value += checker[n + 1, m + 1] * originalToProcessBitmapLock.GetPixel(i + n, j + m).R;

                                    }
                                }
                                value /= sumChecker;
                                if (fours.Contains(value))
                                {
                                    processedBitmapLock.SetPixel(i, j, Color.FromArgb(255, 4, 4, 4));
                                }
                                //  ifChanged = true;
                            }
                            else if (originalToProcessBitmapLock.GetPixel(i - 1, j - 1).R == 255 || originalToProcessBitmapLock.GetPixel(i + 1, j + 1).R == 255
                                || originalToProcessBitmapLock.GetPixel(i + 1, j - 1).R == 255 || originalToProcessBitmapLock.GetPixel(i - 1, j + 1).R == 255)
                            {
                                processedBitmapLock.SetPixel(i, j, Color.FromArgb(255, 3, 3, 3));
                                //  ifChanged = true;
                            }
                            else
                            {
                                //  ifChanged = false;
                            }
                        }
                        else
                        {
                            processedBitmapLock.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                        }
                    }
                }

                for (int i = 0; i < originalBitmapLock.Width; i++)
                {
                    for (int j = 0; j < originalBitmapLock.Height; j++)
                    {
                        int value = 0;
                        if (processedBitmapLock.GetPixel(i, j).R == 4 || processedBitmapLock.GetPixel(i, j).R == 2 || processedBitmapLock.GetPixel(i, j).R == 3)
                        {

                            for (int n = -1; n <= 1; n++)
                            {
                                for (int m = -1; m <= 1; m++)
                                {
                                    value += checker[n + 1, m + 1] * originalToProcessBitmapLock.GetPixel(i + n, j + m).R;

                                }
                            }

                            value /= sumChecker;
                            if (cuts.Contains(value))
                            {
                                processedBitmapLock.SetPixel(i, j, Color.FromArgb( 255, 255, 255));
                                ifChanged = true;
                            }
                            else
                            {
                                processedBitmapLock.SetPixel(i, j, Color.FromArgb( 0, 0, 0));
                                ifChanged = true;
                            }


                        }

                        else
                        {

                            ifChanged = false;
                        }


                    }
                }
            } while (ifChanged);


            for (int i = 0; i < originalBitmapLock.Width; i++)
            {
                for (int j = 0; j < originalBitmapLock.Height; j++)
                {
                    if (processedBitmapLock.GetPixel(i, j).R != 0 && processedBitmapLock.GetPixel(i, j).R != 255)
                    {
                        int f = processedBitmapLock.GetPixel(i, j).R;
                    }
                }
            }


            originalToProcessBitmapLock.UnlockBits();
            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }
        
        public static List<Minutiae> ExtractMinutiaes(Bitmap original)
        {
            List<Minutiae> minutiaes = new List<Minutiae>();
            LockBitmap originalBitmapLock = new LockBitmap(original);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            int blackColorLimit = 10;

            for (int i = 1; i < originalBitmapLock.Width - 1; i++)
            {
                for (int j = 1; j < originalBitmapLock.Height - 1; j++)
                {
                    int pixelValue = originalBitmapLock.GetPixel(i, j).R;
                    if (pixelValue < blackColorLimit)
                    {
                        int[] neighbours = {
                            originalBitmapLock.GetPixel(i - 1, j - 1).R,
                            originalBitmapLock.GetPixel(i - 1, j).R,
                            originalBitmapLock.GetPixel(i - 1, j + 1).R,
                            originalBitmapLock.GetPixel(i, j + 1).R,
                            originalBitmapLock.GetPixel(i + 1, j + 1).R,
                            originalBitmapLock.GetPixel(i + 1, j).R,
                            originalBitmapLock.GetPixel(i + 1, j - 1).R,
                            originalBitmapLock.GetPixel(i, j - 1).R,
                            originalBitmapLock.GetPixel(i - 1, j - 1).R,
                        };

                        int crossingNumber = 0;
                        for (int k = 0; k < neighbours.Length; k++)
                        {
                            if (k < (neighbours.Length - 1))
                            {
                                int firstPixel = neighbours[k] < blackColorLimit ? 1 : 0;
                                int secondPixel = neighbours[k + 1] < blackColorLimit ? 1 : 0;
                                crossingNumber += Math.Abs(firstPixel - secondPixel);
                            }
                            else
                            {
                                int firstPixel = neighbours[k] < blackColorLimit ? 1 : 0;
                                int secondPixel = neighbours[0] < blackColorLimit ? 1 : 0;
                                crossingNumber += Math.Abs(firstPixel - secondPixel);
                            }
                        }

                        crossingNumber /= 2;

                        if (crossingNumber == 1 || crossingNumber == 3)
                        {
                            Minutiae minutiae = new Minutiae(i, j, crossingNumber);
                            minutiaes.Add(minutiae);
                        }
                    }
                }
            }

            originalBitmapLock.UnlockBits();
            return minutiaes;
        }

        public static Bitmap MarkMinutiaes(Bitmap original, List<Minutiae> minutiaes)
        {
            Bitmap processedBmp = new Bitmap(original);
            float circleWidth = 3;

            // Termination - red (crossing number = 1)
            // Bifurcation - blue (crossing number = 3)
            foreach (Minutiae minutiae in minutiaes)
            {
                using (Graphics grf = Graphics.FromImage(processedBmp))
                {
                    Color color = minutiae.CrossingNumber == 1 ? Color.Red : 
                        minutiae.CrossingNumber == 3 ? Color.Blue : Color.Yellow;
                    using (Pen penRed = new Pen(color, 1.2f))
                    {
                        int posX = minutiae.X - (int)circleWidth;
                        int posY = minutiae.Y - (int)circleWidth;
                        grf.DrawEllipse(penRed, new RectangleF(posX, posY, circleWidth, circleWidth));
                    }
                }
            }

            return processedBmp;
        }
    }
}
