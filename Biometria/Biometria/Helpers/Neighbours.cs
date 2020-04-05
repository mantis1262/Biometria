using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria.Helpers
{
    public static class Neighbours
    {
        public static int[,] NeighboursPositions(int posX, int posY)
        {
            int[,] neighboursPos = {
                { posX - 1, posY - 1 },
                { posX,  posY - 1 },
                { posX + 1, posY - 1 },
                { posX + 1, posY },
                { posX + 1, posY + 1 },
                { posX, posY + 1 },
                { posX - 1, posY + 1 },
                { posX - 1, posY }
            };

            return neighboursPos;
        }

        public static int[] NeighboursColors(LockBitmap lockBitmap, int[,] neighboursPos)
        {
            int[] neighboursColors = {
                lockBitmap.GetPixel(neighboursPos[0, 0], neighboursPos[0, 1]).R,
                lockBitmap.GetPixel(neighboursPos[1, 0], neighboursPos[1, 1]).R,
                lockBitmap.GetPixel(neighboursPos[2, 0], neighboursPos[2, 1]).R,
                lockBitmap.GetPixel(neighboursPos[3, 0], neighboursPos[3, 1]).R,
                lockBitmap.GetPixel(neighboursPos[4, 0], neighboursPos[4, 1]).R,
                lockBitmap.GetPixel(neighboursPos[5, 0], neighboursPos[5, 1]).R,
                lockBitmap.GetPixel(neighboursPos[6, 0], neighboursPos[6, 1]).R,
                lockBitmap.GetPixel(neighboursPos[7, 0], neighboursPos[7, 1]).R
             };

            return neighboursColors;
        }
    }
}
