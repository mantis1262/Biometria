using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biometria.Forms
{
    public partial class SecondForm : Form
    {
        public SecondForm()
        {
            InitializeComponent();
        }

        private void SecondForm_Load(object sender, EventArgs e)
        {

        }

        public void SetBinarizedImage(Bitmap bitmap)
        {
            binarizationImage.Image = bitmap;
        }

        public void SetThinnedImage(Bitmap bitmap)
        {
            thinningImage.Image = bitmap;
        }

        public void SetMinutiaesImage(Bitmap bitmap)
        {
            minutiaesImage.Image = bitmap;
        }
    }
}
