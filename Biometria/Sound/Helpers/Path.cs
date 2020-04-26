using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Sound.Helpers
{
    public static class Path
    {
        public static string GetImagePath()
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Browse Image Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "tif",
                Filter =
                    "TIF (*.tif)|*.tif|" +
                    "TIFF (*.tiff)|*.tiff|" +
                    "BMP (*.bmp)|*.bmp|" +
                    "JPG (*.jpg)|*.jpg|" +
                    "JPEG (*.jpeg)|*.jpeg|" +
                    "JPE (*.jpe)|*.jpe|" +
                    "PNG (*.png)|*.png|" +
                    "All images (*.bmp, *.jpg, *.jpeg, *.jpe, *.tif, *.tiff,*.png) | *.bmp; *.jpg; *.jpeg; *.jpe; *tif; *tiff; *.png",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
            }
            return path;
        }

        public static string GetSoundPath()
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Browse Sound Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "wav",
                Filter =
                    "WAV (*.wav)|*.wav",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
            }
            return path;
        }


    }
}
