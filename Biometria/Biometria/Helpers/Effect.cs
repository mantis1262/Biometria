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
                        processedBitmapLock.SetPixel(i, j, Color.FromArgb(255, ColorsValues.WHITE, ColorsValues.WHITE, ColorsValues.WHITE));
                    else
                        processedBitmapLock.SetPixel(i, j, Color.FromArgb(255, ColorsValues.BLACK, ColorsValues.BLACK, ColorsValues.BLACK));
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
                        originalToProcessBitmapLock.SetPixel(i, j, Color.FromArgb(255, ColorsValues.WHITE, ColorsValues.WHITE, ColorsValues.WHITE));
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
                    processedBitmapLock.SetPixel(i,j,Color.FromArgb(255, ColorsValues.WHITE, ColorsValues.WHITE, ColorsValues.WHITE));

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
                            if (originalToProcessBitmapLock.GetPixel(i - 1, j).R == ColorsValues.WHITE || originalToProcessBitmapLock.GetPixel(i + 1, j).R == ColorsValues.WHITE
                                || originalToProcessBitmapLock.GetPixel(i, j - 1).R == ColorsValues.WHITE || originalToProcessBitmapLock.GetPixel(i, j + 1).R == ColorsValues.WHITE)
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
                            else if (originalToProcessBitmapLock.GetPixel(i - 1, j - 1).R == ColorsValues.WHITE || originalToProcessBitmapLock.GetPixel(i + 1, j + 1).R == ColorsValues.WHITE
                                || originalToProcessBitmapLock.GetPixel(i + 1, j - 1).R == ColorsValues.WHITE || originalToProcessBitmapLock.GetPixel(i - 1, j + 1).R == ColorsValues.WHITE)
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
                            processedBitmapLock.SetPixel(i, j, Color.FromArgb(255, ColorsValues.WHITE, ColorsValues.WHITE, ColorsValues.WHITE));
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
                                processedBitmapLock.SetPixel(i, j, Color.FromArgb(ColorsValues.WHITE, ColorsValues.WHITE, ColorsValues.WHITE));
                                ifChanged = true;
                            }
                            else
                            {
                                processedBitmapLock.SetPixel(i, j, Color.FromArgb(ColorsValues.BLACK, ColorsValues.BLACK, ColorsValues.BLACK));
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
                    if (processedBitmapLock.GetPixel(i, j).R != 0 && processedBitmapLock.GetPixel(i, j).R != ColorsValues.WHITE)
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

        public static Bitmap RemoveBugPixels(Bitmap original)
        {
            Bitmap processedBmp = new Bitmap(original);
            LockBitmap originalBitmapLock = new LockBitmap(original);
            LockBitmap processedBitmapLock = new LockBitmap(processedBmp);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);
            processedBitmapLock.LockBits(ImageLockMode.WriteOnly);

            for (int i = 1; i < originalBitmapLock.Width - 1; i++)
            {
                for (int j = 1; j < originalBitmapLock.Height - 1; j++)
                {
                    int pixelValue = originalBitmapLock.GetPixel(i, j).R;
                    if (pixelValue == ColorsValues.BLACK)
                    {
                        int[] neighbours = {
                            originalBitmapLock.GetPixel(i - 1, j - 1).R,
                            originalBitmapLock.GetPixel(i - 1, j).R,
                            originalBitmapLock.GetPixel(i - 1, j + 1).R,
                            originalBitmapLock.GetPixel(i, j + 1).R,
                            originalBitmapLock.GetPixel(i + 1, j + 1).R,
                            originalBitmapLock.GetPixel(i + 1, j).R,
                            originalBitmapLock.GetPixel(i + 1, j - 1).R,
                            originalBitmapLock.GetPixel(i, j - 1).R
                        };

                        int neighboursCount = 0;
                        for (int k = 0; k < neighbours.Length; k++)
                        {
                            if (neighbours[k] == 0)
                                ++neighboursCount;
                        }

                        if (neighboursCount >= 4)
                            processedBitmapLock.SetPixel(i, j, Color.FromArgb(ColorsValues.WHITE, ColorsValues.WHITE, ColorsValues.WHITE));
                    }
                }
            }

            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();

            return processedBmp;
        }
        
        public static MinutiaesResult ExtractMinutiaes(Bitmap original, int offsetFromImageBorders, int threshold, int angleIntervalLeft, int angleIntervalRight)
        {
            List<Minutiae> minutiaes = new List<Minutiae>();
            LockBitmap originalBitmapLock = new LockBitmap(original);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);

            for (int i = 1; i < originalBitmapLock.Width - 1; i++)
            {
                for (int j = 1; j < originalBitmapLock.Height - 1; j++)
                {
                    int pixelValue = originalBitmapLock.GetPixel(i, j).R;
                    if (pixelValue == ColorsValues.BLACK)
                    {
                        int[] neighbours = {
                            originalBitmapLock.GetPixel(i - 1, j - 1).R,
                            originalBitmapLock.GetPixel(i - 1, j).R,
                            originalBitmapLock.GetPixel(i - 1, j + 1).R,
                            originalBitmapLock.GetPixel(i, j + 1).R,
                            originalBitmapLock.GetPixel(i + 1, j + 1).R,
                            originalBitmapLock.GetPixel(i + 1, j).R,
                            originalBitmapLock.GetPixel(i + 1, j - 1).R,
                            originalBitmapLock.GetPixel(i, j - 1).R
                        };

                        int crossingNumber = 0;
                        for (int k = 0; k < neighbours.Length; k++)
                        {
                            if (k < (neighbours.Length - 1))
                            {
                                int firstPixel = neighbours[k] == ColorsValues.BLACK ? 1 : 0;
                                int secondPixel = neighbours[k + 1] == ColorsValues.BLACK ? 1 : 0;
                                crossingNumber += Math.Abs(firstPixel - secondPixel);
                            }
                            else
                            {
                                int firstPixel = neighbours[k] == ColorsValues.BLACK ? 1 : 0;
                                int secondPixel = neighbours[0] == ColorsValues.BLACK ? 1 : 0;
                                crossingNumber += Math.Abs(firstPixel - secondPixel);
                            }
                        }

                        crossingNumber /= 2;

                        if (crossingNumber == 1 || crossingNumber == 3)
                        {
                            Minutiae minutiae = new Minutiae(i, j, crossingNumber);
                            CalculateMinutiaeDirectionAngle(originalBitmapLock, minutiae);
                            minutiaes.Add(minutiae);
                        }
                    }
                }
            }

            originalBitmapLock.UnlockBits();

            List<Minutiae> limitedMinutiaes = new List<Minutiae>();
            int centerPosX = 0, centerPosY = 0;

            foreach (Minutiae minutiae in minutiaes)
            {
                centerPosX += minutiae.X;
                centerPosY += minutiae.Y;
            }

            centerPosX /= minutiaes.Count;
            centerPosY /= minutiaes.Count;

            //foreach (Minutiae minutiae in minutiaes)
            //{
            //    double distance = Math.Sqrt(Math.Pow(minutiae.X, 2) + Math.Pow(minutiae.Y, 2));
            //    if (minutiae.X >= offsetFromImageBorders && 
            //        minutiae.X <= (original.Width - offsetFromImageBorders) &&
            //        minutiae.Y >= offsetFromImageBorders &&
            //        minutiae.Y <= (original.Height - offsetFromImageBorders) &&
            //        distance <= offsetFromCenter)
            //        limitedMinutiaes.Add(minutiae);
            //}

            // remove minutiaes near the edge
            limitedMinutiaes = RemoveMinutiaesNearBorders(minutiaes, original.Width, original.Height, offsetFromImageBorders);

            // remove false minutiaes
            limitedMinutiaes = RemoveFalseMinutiaes(original, limitedMinutiaes, threshold, angleIntervalLeft, angleIntervalRight);

            return new MinutiaesResult(centerPosX, centerPosY, limitedMinutiaes);
        }

        public static void CalculateMinutiaeDirectionAngle(LockBitmap lockBitmap, Minutiae minutiae)
        {
            int posX = minutiae.X;
            int posY = minutiae.Y;

            int[,] neighboursPos = Neighbours.NeighboursPositions(posX, posY);
            int[] neighboursColors = Neighbours.NeighboursColors(lockBitmap, neighboursPos);

            int secondPointPosX = -2;
            int secondPointPosY = -2;
            bool foundSecondPoint = false;

            if (minutiae.CrossingNumber == 1)
            {
                for (int k = 0; k < neighboursColors.Length && !foundSecondPoint; k++)
                {
                    int pixel = neighboursColors[k] == ColorsValues.BLACK ? 1 : 0;
                    
                    if (pixel == 1)
                    {
                        foundSecondPoint = true;
                        secondPointPosX = neighboursPos[k, 0];
                        secondPointPosY = neighboursPos[k, 1];
                    }
                }
            }
            else
            if (minutiae.CrossingNumber == 3)
            {
                for (int k = 0; k < neighboursColors.Length && !foundSecondPoint; k++)
                {
                    int pixel = neighboursColors[k] == ColorsValues.BLACK ? 1 : 0;
                    int leftNeighbour, rightNeighbour;

                    if (pixel == 0)
                    {
                        if (k == 0)
                        {
                            leftNeighbour = neighboursColors[neighboursColors.Length - 1];
                            rightNeighbour = neighboursColors[k + 1];
                        }
                        else
                        {
                            if (k == (neighboursColors.Length - 1))
                            {
                                leftNeighbour = neighboursColors[k - 1];
                                rightNeighbour = neighboursColors[0];
                            }
                            else
                            {
                                leftNeighbour = neighboursColors[k - 1];
                                rightNeighbour = neighboursColors[k + 1];
                            }
                        }

                        if (leftNeighbour == 0 && rightNeighbour == 0)
                        {
                            foundSecondPoint = true;
                            secondPointPosX = neighboursPos[k, 0];
                            secondPointPosY = neighboursPos[k, 1];
                        }
                    }
                }
            }

            if (foundSecondPoint)
            {
                minutiae.DirectionX = posX - secondPointPosX;
                minutiae.DirectionY = posY - secondPointPosY;
                float angleRadians = VectorsAngle(1, 0, minutiae.DirectionX, minutiae.DirectionY);
                float angleDegrees = ConvertToDegrees(angleRadians);
                minutiae.OrientationAngle = angleDegrees >= 0 ? angleDegrees : (360 + angleDegrees);
                return;
            }
            else
                throw new Exception("Minutiae is broken. Counld not find direction.");
        }

        public static float ConvertToRadians(float degrees)
        {
            return (float)((Math.PI / 180.0) * degrees);
        }

        public static float ConvertToDegrees(float radians)
        {
            return (float)((180.0 / Math.PI) * radians);
        }

        public static float VectorsAngle(int x1, int y1, int x2, int y2)
        {
            //dot product
            int dot = x1 * x2 + y1 * y2;
            //determinant
            int det = x1 * y2 - y1 * x2;

            return (float)Math.Atan2(det, dot);
        }

        public static List<Minutiae> RemoveMinutiaesNearBorders(List<Minutiae> minutiaes, int imageWidth, int imageHeight, int offsetFromImageBorders)
        {
            List<Minutiae> limitedMinutiaes = new List<Minutiae>();
            foreach (Minutiae minutiae in minutiaes)
            {
                if (minutiae.X >= offsetFromImageBorders &&
                    minutiae.X <= (imageWidth - offsetFromImageBorders) &&
                    minutiae.Y >= offsetFromImageBorders &&
                    minutiae.Y <= (imageHeight - offsetFromImageBorders))
                    limitedMinutiaes.Add(minutiae);
            }
            return limitedMinutiaes;
        }

        public static List<Minutiae> RemoveFalseMinutiaes(Bitmap original, List<Minutiae> minutiaes, float threshold, float angleIntervalLeft, float angleIntervalRight)
        {
            List<Minutiae> limitedMinutiaes = new List<Minutiae>(minutiaes);
            List<Minutiae> minutiaesToRemove = new List<Minutiae>();

            for (int i = 0; i < minutiaes.Count - 1; i++)
            {
                if (!minutiaesToRemove.Contains(minutiaes[i])) //  && minutiaes[i].CrossingNumber == 1
                {
                    for (int j = i + 1; j < minutiaes.Count; j++)
                    {
                        if (!minutiaesToRemove.Contains(minutiaes[j])) // && minutiaes[i].CrossingNumber == 1
                        {
                            float distance = Minutiae.Distance(minutiaes[i], minutiaes[j]);
                            if (distance < threshold)
                            {
                                //float angleDiff = Math.Abs(minutiaes[i].OrientationAngle - minutiaes[j].OrientationAngle);
                                //if (angleDiff >= angleIntervalLeft && angleDiff <= angleIntervalRight)
                                //{
                                //    int lineVectorPosX = minutiaes[j].X - minutiaes[i].X;
                                //    int lineVectorPosY = minutiaes[j].Y - minutiaes[i].Y;
                                //    float lineAngle = ConvertToDegrees(VectorsAngle(1, 0, lineVectorPosX, lineVectorPosY));
                                //    lineAngle = lineAngle >= 0 ? lineAngle : (360 + lineAngle);

                                //    float diffOne = Math.Abs(lineAngle - minutiaes[i].OrientationAngle);
                                //    float diffTwo = Math.Abs(lineAngle - minutiaes[j].OrientationAngle);

                                //    if ((diffOne >= angleIntervalLeft && diffOne <= angleIntervalRight) || (diffTwo >= angleIntervalLeft && diffTwo <= angleIntervalRight))
                                //    {
                                //        minutiaesToRemove.Add(minutiaes[i]);
                                //        minutiaesToRemove.Add(minutiaes[j]);
                                //    }
                                //}  
                                minutiaesToRemove.Add(minutiaes[i]);
                                minutiaesToRemove.Add(minutiaes[j]);
                                break;
                            }
                        }
                    }
                }
            }

            foreach (Minutiae minutiae in minutiaesToRemove)
            {
                limitedMinutiaes.Remove(minutiae);
            }
            
            return limitedMinutiaes;
        }

        public static Bitmap MarkMinutiaes(Bitmap original, MinutiaesResult minutiaesResult)
        {
            Bitmap processedBmp = new Bitmap(original);
            float shapeWidth = 3;

            // Center - purple
            // Termination - red (crossing number = 1)
            // Bifurcation - blue (crossing number = 3)

            using (Graphics grf = Graphics.FromImage(processedBmp))
            {
                foreach (Minutiae minutiae in minutiaesResult.Minutiaes)
                {
                    Color color = minutiae.CrossingNumber == 1 ? Color.Red :
                    minutiae.CrossingNumber == 3 ? Color.Blue : Color.Yellow;
                    int shapeType = minutiae.CrossingNumber == 1 ? 0 : 1;
                    int posX = minutiae.X - (int)shapeWidth;
                    int posY = minutiae.Y - (int)shapeWidth;

                    using (Pen pen = new Pen(color, 1.0f))
                    { 
                        if (shapeType == 0)
                        {
                            grf.DrawEllipse(pen, posX - shapeWidth, posY - shapeWidth, shapeWidth, shapeWidth);
                        }
                        else
                        {
                            grf.DrawRectangle(pen, posX - shapeWidth, posY - shapeWidth, shapeWidth, shapeWidth);
                        }

                        grf.DrawLine(pen, (float)posX, (float)posY, (float)(posX + minutiae.DirectionX * shapeWidth), (float)(posY + minutiae.DirectionY * shapeWidth));

                    }
                }  
            }

            using (Graphics grf = Graphics.FromImage(processedBmp))
            {
                using (Pen penRed = new Pen(Color.DarkViolet, 2.2f))
                {
                    int posX = minutiaesResult.CenterX - 10;
                    int posY = minutiaesResult.CenterY - 10;
                    grf.DrawEllipse(penRed, new RectangleF(posX - shapeWidth, posY - shapeWidth, 10, 10));
                }
            }

            return processedBmp;
        }

        public static Bitmap ClipBoundaries(Bitmap original, int clippingAdditionalSpace)
        {
            Bitmap processedBitmap = AddSpaceBoundaries(original, clippingAdditionalSpace);
            LockBitmap processedBitmapLock = new LockBitmap(processedBitmap);
            processedBitmapLock.LockBits(ImageLockMode.ReadOnly);

            int minPosX = processedBitmapLock.Width, minPosY = processedBitmapLock.Height, maxPosX = 0, maxPosY = 0;

            for (int i = 0; i < processedBitmapLock.Width; i++)
            {
                for (int j = 0; j < processedBitmapLock.Height; j++)
                {
                    if (processedBitmapLock.GetPixel(i, j).R == ColorsValues.BLACK)
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

        public static Bitmap AddSpaceBoundaries(Bitmap original, int spaceSize)
        {
            Bitmap processedBmp = null;

            LockBitmap originalBitmapLock = new LockBitmap(original);
            originalBitmapLock.LockBits(ImageLockMode.ReadOnly);

            //scan top border
            bool topNeedsSpace = false;
            for (int i = 0; i < originalBitmapLock.Width; i++)
            {
                if (originalBitmapLock.GetPixel(i, 0).R == ColorsValues.BLACK)
                    topNeedsSpace = true;
            }

            //scan bottom border
            bool bottomNeedsSpace = false;
            for (int i = 0; i < originalBitmapLock.Width; i++)
            {
                if (originalBitmapLock.GetPixel(i, originalBitmapLock.Height - 1).R == ColorsValues.BLACK)
                    bottomNeedsSpace = true;
            }

            //scan left border
            bool leftNeedsSpace = false;
            for (int i = 0; i < originalBitmapLock.Height; i++)
            {
                if (originalBitmapLock.GetPixel(0, i).R == ColorsValues.BLACK)
                    leftNeedsSpace = true;
            }

            //scan right border
            bool rightNeedsSpace = false;
            for (int i = 0; i < originalBitmapLock.Height; i++)
            {
                if (originalBitmapLock.GetPixel(originalBitmapLock.Width - 1, i).R == ColorsValues.BLACK)
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
                    List<int> values = new List<int>();
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
                            values.Add(originalBitmapLock.GetPixel(x, y).R);
                        }
                    }
                    int midleListIndex = (MaskSize * MaskSize) / 2;
                    values.Sort();
                    processedBitmapLock.SetPixel(i, j, Color.FromArgb(originalBitmapLock.GetPixel(i, j).A, values[midleListIndex], values[midleListIndex], values[midleListIndex]));
                }

            originalBitmapLock.UnlockBits();
            processedBitmapLock.UnlockBits();
            return processedBmp;
        }

        public static MinutiaesResult Rotation(MinutiaesResult original, double alfa, int DX, int DY)
        {
            MinutiaesResult result = new MinutiaesResult();
            result.CenterX = original.CenterX;
            result.CenterY = original.CenterY;
            for (int i = 0; i < original.Minutiaes.Count; i++)
            {

                int tempX = (int)(Math.Cos(alfa) * original.Minutiaes[i].X - Math.Sin(alfa) * original.Minutiaes[i].Y + DX);
                int tempY = (int)(Math.Sin(alfa) * original.Minutiaes[i].X + Math.Cos(alfa) * original.Minutiaes[i].Y + DY);
                result.Minutiaes.Add(new Minutiae(tempX, tempY, (float)(original.Minutiaes[i].OrientationAngle + alfa), original.Minutiaes[i].CrossingNumber));
            }


            return result;

        }
           

    }
}
