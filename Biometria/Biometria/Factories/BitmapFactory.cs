using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSoundProcessing.Factories
{
    public static class BitmapFactory
    {
        public static Bitmap CreateBitmap(string path)
        {
            Bitmap image = (Bitmap)Image.FromFile(path);
            return image;
        }
    }
}
