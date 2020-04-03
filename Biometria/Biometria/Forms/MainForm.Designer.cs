namespace Biometria
{
    partial class MainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.loadImageButton = new System.Windows.Forms.Button();
            this.binarizationLabel = new System.Windows.Forms.Label();
            this.thinningLabel = new System.Windows.Forms.Label();
            this.minutiaesLabel = new System.Windows.Forms.Label();
            this.binarizationImage = new System.Windows.Forms.PictureBox();
            this.thinningImage = new System.Windows.Forms.PictureBox();
            this.minutiaesImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.binarizationImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thinningImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutiaesImage)).BeginInit();
            this.SuspendLayout();
            // 
            // loadImageButton
            // 
            this.loadImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loadImageButton.Location = new System.Drawing.Point(12, 12);
            this.loadImageButton.Name = "loadImageButton";
            this.loadImageButton.Size = new System.Drawing.Size(93, 40);
            this.loadImageButton.TabIndex = 0;
            this.loadImageButton.Text = "Load";
            this.loadImageButton.UseVisualStyleBackColor = true;
            this.loadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // binarizationLabel
            // 
            this.binarizationLabel.AutoSize = true;
            this.binarizationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.binarizationLabel.Location = new System.Drawing.Point(49, 77);
            this.binarizationLabel.Name = "binarizationLabel";
            this.binarizationLabel.Size = new System.Drawing.Size(113, 25);
            this.binarizationLabel.TabIndex = 3;
            this.binarizationLabel.Text = "Binaryzacja";
            // 
            // thinningLabel
            // 
            this.thinningLabel.AutoSize = true;
            this.thinningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.thinningLabel.Location = new System.Drawing.Point(393, 77);
            this.thinningLabel.Name = "thinningLabel";
            this.thinningLabel.Size = new System.Drawing.Size(103, 25);
            this.thinningLabel.TabIndex = 4;
            this.thinningLabel.Text = "Ścienianie";
            // 
            // minutiaesLabel
            // 
            this.minutiaesLabel.AutoSize = true;
            this.minutiaesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.minutiaesLabel.Location = new System.Drawing.Point(735, 77);
            this.minutiaesLabel.Name = "minutiaesLabel";
            this.minutiaesLabel.Size = new System.Drawing.Size(80, 25);
            this.minutiaesLabel.TabIndex = 5;
            this.minutiaesLabel.Text = "Minucje";
            // 
            // binarizationImage
            // 
            this.binarizationImage.Location = new System.Drawing.Point(54, 105);
            this.binarizationImage.Name = "binarizationImage";
            this.binarizationImage.Size = new System.Drawing.Size(100, 50);
            this.binarizationImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.binarizationImage.TabIndex = 6;
            this.binarizationImage.TabStop = false;
            this.binarizationImage.Click += new System.EventHandler(this.BinarizationImage_Click);
            // 
            // thinningImage
            // 
            this.thinningImage.Location = new System.Drawing.Point(398, 105);
            this.thinningImage.Name = "thinningImage";
            this.thinningImage.Size = new System.Drawing.Size(100, 50);
            this.thinningImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.thinningImage.TabIndex = 7;
            this.thinningImage.TabStop = false;
            this.thinningImage.Click += new System.EventHandler(this.ThinningImage_Click);
            // 
            // minutiaesImage
            // 
            this.minutiaesImage.Location = new System.Drawing.Point(740, 105);
            this.minutiaesImage.Name = "minutiaesImage";
            this.minutiaesImage.Size = new System.Drawing.Size(100, 50);
            this.minutiaesImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.minutiaesImage.TabIndex = 8;
            this.minutiaesImage.TabStop = false;
            this.minutiaesImage.Click += new System.EventHandler(this.MinutiaesImage_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1068, 450);
            this.Controls.Add(this.minutiaesImage);
            this.Controls.Add(this.thinningImage);
            this.Controls.Add(this.binarizationImage);
            this.Controls.Add(this.minutiaesLabel);
            this.Controls.Add(this.thinningLabel);
            this.Controls.Add(this.binarizationLabel);
            this.Controls.Add(this.loadImageButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "MainForm";
            this.Text = "Biometryczne wspomaganie komunikacji człowiek-komputer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.binarizationImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thinningImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutiaesImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadImageButton;
        private System.Windows.Forms.Label binarizationLabel;
        private System.Windows.Forms.Label thinningLabel;
        private System.Windows.Forms.Label minutiaesLabel;
        private System.Windows.Forms.PictureBox binarizationImage;
        private System.Windows.Forms.PictureBox thinningImage;
        private System.Windows.Forms.PictureBox minutiaesImage;
    }
}

