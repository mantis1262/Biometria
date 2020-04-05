namespace Biometria.Forms
{
    partial class SecondForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.minutiaesImage = new System.Windows.Forms.PictureBox();
            this.thinningImage = new System.Windows.Forms.PictureBox();
            this.binarizationImage = new System.Windows.Forms.PictureBox();
            this.minutiaesLabel = new System.Windows.Forms.Label();
            this.thinningLabel = new System.Windows.Forms.Label();
            this.binarizationLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.minutiaesImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thinningImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.binarizationImage)).BeginInit();
            this.SuspendLayout();
            // 
            // minutiaesImage
            // 
            this.minutiaesImage.Location = new System.Drawing.Point(791, 63);
            this.minutiaesImage.Name = "minutiaesImage";
            this.minutiaesImage.Size = new System.Drawing.Size(100, 50);
            this.minutiaesImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.minutiaesImage.TabIndex = 14;
            this.minutiaesImage.TabStop = false;
            // 
            // thinningImage
            // 
            this.thinningImage.Location = new System.Drawing.Point(403, 63);
            this.thinningImage.Name = "thinningImage";
            this.thinningImage.Size = new System.Drawing.Size(100, 50);
            this.thinningImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.thinningImage.TabIndex = 13;
            this.thinningImage.TabStop = false;
            // 
            // binarizationImage
            // 
            this.binarizationImage.Location = new System.Drawing.Point(17, 63);
            this.binarizationImage.Name = "binarizationImage";
            this.binarizationImage.Size = new System.Drawing.Size(100, 50);
            this.binarizationImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.binarizationImage.TabIndex = 12;
            this.binarizationImage.TabStop = false;
            // 
            // minutiaesLabel
            // 
            this.minutiaesLabel.AutoSize = true;
            this.minutiaesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.minutiaesLabel.Location = new System.Drawing.Point(786, 35);
            this.minutiaesLabel.Name = "minutiaesLabel";
            this.minutiaesLabel.Size = new System.Drawing.Size(80, 25);
            this.minutiaesLabel.TabIndex = 11;
            this.minutiaesLabel.Text = "Minucje";
            // 
            // thinningLabel
            // 
            this.thinningLabel.AutoSize = true;
            this.thinningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.thinningLabel.Location = new System.Drawing.Point(398, 35);
            this.thinningLabel.Name = "thinningLabel";
            this.thinningLabel.Size = new System.Drawing.Size(103, 25);
            this.thinningLabel.TabIndex = 10;
            this.thinningLabel.Text = "Ścienianie";
            // 
            // binarizationLabel
            // 
            this.binarizationLabel.AutoSize = true;
            this.binarizationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.binarizationLabel.Location = new System.Drawing.Point(12, 35);
            this.binarizationLabel.Name = "binarizationLabel";
            this.binarizationLabel.Size = new System.Drawing.Size(113, 25);
            this.binarizationLabel.TabIndex = 9;
            this.binarizationLabel.Text = "Binaryzacja";
            // 
            // SecondForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1041, 478);
            this.Controls.Add(this.minutiaesImage);
            this.Controls.Add(this.thinningImage);
            this.Controls.Add(this.binarizationImage);
            this.Controls.Add(this.minutiaesLabel);
            this.Controls.Add(this.thinningLabel);
            this.Controls.Add(this.binarizationLabel);
            this.Name = "SecondForm";
            this.Text = "SecondForm";
            this.Load += new System.EventHandler(this.SecondForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.minutiaesImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thinningImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.binarizationImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label binarizationLabel;
        private System.Windows.Forms.Label thinningLabel;
        private System.Windows.Forms.Label minutiaesLabel;
        private System.Windows.Forms.PictureBox binarizationImage;
        private System.Windows.Forms.PictureBox thinningImage;
        private System.Windows.Forms.PictureBox minutiaesImage; 
    }
}