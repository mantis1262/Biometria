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

        public static Bitmap ClipBoundaries(Bitmap original, int backgroundColor, int clippingAdditionalSpace)
        {
            Bitmap processedBitmap = AddSpaceBoundaries(original, backgroundColor, clippingAdditionalSpace);
            LockBitmap processedBitmapLock = new LockBitmap(processedBitmap);
            processedBitmapLock.LockBits(ImageLockMode.ReadOnly);

            int minPosX = processedBitmapLock.Width, minPosY = processedBitmapLock.Height, maxPosX = 0, maxPosY = 0;

            for (int i = 0; i < processedBitmapLock.Width; i++)
            {
                for (int j = 0; j < processedBitmapLock.Height; j++)
                {
                    if (processedBitmapLock.GetPixel(i, j).R < backgroundColor)
                    {
                        if (i < minPosX) minPosX = i;
                        if (j < minPosY) minPosY = j;
                        if (i > maxPosX) maxPosX = i;
                        if (j > maxPosY) maxPosY = j;
                    }
                }
            }

            processedBitmapLock.UnlockBits();

            minPosX -= clippingAdditionalSpace;
            minPosY -= clippingAdditionalSpace;
            maxPosX += clippingAdditionalSpace;
            maxPosY += clippingAdditionalSpace;

            if (minPosX < 0) minPosX = 0;
            if (minPosY < 0) minPosY = 0;
            if (maxPosX > (processedBitmap.Width - 1)) maxPosX = processedBitmap.Width - 1;
            if (maxPosY > (processedBitmap.Height - 1)) maxPosY = processedBitmap.Height - 1;

            int newImageWidth = maxPosX - minPosX;
            int newImageHeight = maxPosY - minPosY;
            Bitmap clippedBitmap = new Bitmap(newImageWidth, newImageHeight);
            Rectangle processedClipRegion = new Rectangle(minPosX, minPosY, newImageWidth, newImageHeight);
            Rectangle destRegion = new Rectangle(0, 0, newImageWidth, newImageHeight);
            CopyRegionIntoImage(processedBitmap, processedClipRegion, ref clippedBitmap, destRegion);

            return clippedBitmap;
        }

        public static Bitmap AddSpaceBoundaries(Bitmap original, int backgroundColor, int spaceSize)
        {
            Bitmap processedBmp = null;

            LockBitmap originalBitmapLock = new LockBitmap(original);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);

            //scan top border
            bool topNeedsSpace = false;
            for (int i = 0; i < originalBitmapLock.Width; i++)
            {
                if (originalBitmapLock.GetPixel(i, 0).R < backgroundColor)
                    topNeedsSpace = true;
            }

            //scan bottom border
            bool bottomNeedsSpace = false;
            for (int i = 0; i < originalBitmapLock.Width; i++)
            {
                if (originalBitmapLock.GetPixel(i, originalBitmapLock.Height - 1).R < backgroundColor)
                    bottomNeedsSpace = true;
            }

            //scan left border
            bool leftNeedsSpace = false;
            for (int i = 0; i < originalBitmapLock.Height; i++)
            {
                if (originalBitmapLock.GetPixel(0, i).R < backgroundColor)
                    leftNeedsSpace = true;
            }

            //scan right border
            bool rightNeedsSpace = false;
            for (int i = 0; i < originalBitmapLock.Height; i++)
            {
                if (originalBitmapLock.GetPixel(originalBitmapLock.Width - 1, i).R < backgroundColor)
                    rightNeedsSpace = true;
            }

            originalBitmapLock.UnlockBits();

            int newPosX = 0, newPosY = 0;
            int newImageWidth = originalBitmapLock.Width;
            int newImageHeigth = originalBitmapLock.Height;

            if (topNeedsSpace | bottomNeedsSpace | leftNeedsSpace | rightNeedsSpace)
            {
                if (topNeedsSpace)
                {
                    newPosY += spaceSize;
                    newImageHeigth += spaceSize;
                }

                if (bottomNeedsSpace)
                {
                    newImageHeigth += spaceSize;
                }

                if (leftNeedsSpace)
                {
                    newPosX += spaceSize;
                    newImageWidth += spaceSize;
                }

                if (rightNeedsSpace)
                {
                    newImageWidth += spaceSize;
                }

                processedBmp = new Bitmap(newImageWidth, newImageHeigth);

                using (Graphics grf = Graphics.FromImage(processedBmp))
                {
                    Color color = Color.White;
                    using (SolidBrush brush = new SolidBrush(Color.White))
                    {
                        grf.FillRectangle(brush, new Rectangle(0, 0, processedBmp.Width, processedBmp.Height));
                    }
                }

                Rectangle originalClipRegion = new Rectangle(0, 0, original.Width, original.Height);
                Rectangle destRegion = new Rectangle(newPosX, newPosY, newImageWidth, newImageHeigth);
                CopyRegionIntoImage(original, originalClipRegion, ref processedBmp, destRegion);
            }
            else
                processedBmp = original;

            return processedBmp;
        }

        public static void CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap, Rectangle destRegion)
        {
            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }

        public static Bitmap MedianFilter(Bitmap original, int MaskSize)
        {
            Bitmap processedBmp = new Bitmap(original.Width, original.Height);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

            int midIndex;
            if (MaskSize % 2 == 0) midIndex = (MaskSize + 1) / 2;
            else midIndex = MaskSize / 2;
            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    int x, y;
                    List<int> R = new List<int>();
                    List<int> G = new List<int>();
                    List<int> B = new List<int>();
                    for (int n = -midIndex; n <= midIndex; n++)
                    {
                        x = i + n;
                        if (x < 0) x = 0;
                        if (x >= originalBitmapLock.Width) x = originalBitmapLock.Width - 1;
                        for (int m = -midIndex; m <= midIndex; m++)
                        {
                            y = j + m;
                            if (y < 0)
                                y = 0;
                            if (y >= originalBitmapLock.Height)
                                y = originalBitmapLock.Height - 1;
                            R.Add(originalBitmapLock.GetPixel(x, y).R);
                            G.Add(originalBitmapLock.GetPixel(x, y).G);
                            B.Add(originalBitmapLock.GetPixel(x, y).B);
                        }
                    }
                    int midleListIndex = (MaskSize * MaskSize) / 2;
                    R.Sort();
                    G.Sort();
                    B.Sort();
                    processedBitmapLock.SetPixel(i, j, Color.FromArgb(originalBitmapLock.GetPixel(i, j).A, R[midleListIndex], G[midleListIndex], B[midleListIndex]));
                }

            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }


    }
}
