﻿using ImageSoundProcessing.Factories;
using Biometria.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biometria
{
    public partial class MainForm : Form
    {
        private string _imagePath;
        private Bitmap _originalBitmap;

        public string ImagePath { get => _imagePath; set => _imagePath = value; }

        public Bitmap OriginalBitmap { get => _originalBitmap; set => _originalBitmap = value; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            string imagePath = Path.GetImagePath();
            if (!imagePath.Equals(""))
            {
                _imagePath = imagePath;
                _originalBitmap = BitmapFactory.CreateBitmap(imagePath);
                _originalBitmap = Effect.GrayMode(_originalBitmap);
                _originalBitmap = Effect.Binarization(_originalBitmap,128);
                _originalBitmap = Effect.Skeletonization(_originalBitmap);
                loadedImage.Image = _originalBitmap;
            }
        }

        private void LoadedImage_Click(object sender, EventArgs e)
        {

        }
    }
}
