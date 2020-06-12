namespace BiometriaApp
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbStanKonta = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrzelew = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.edKwota = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.edRach = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.edOdbiorca = new System.Windows.Forms.TextBox();
            this.grpPrzelewu = new System.Windows.Forms.GroupBox();
            this.btnAnuluj = new System.Windows.Forms.Button();
            this.btnPotwierdz = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Odbiorca = new System.Windows.Forms.ColumnHeader();
            this.Rachunek = new System.Windows.Forms.ColumnHeader();
            this.Kwota = new System.Windows.Forms.ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.grpPrzelewu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(33, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stan konta";
            // 
            // lbStanKonta
            // 
            this.lbStanKonta.AutoSize = true;
            this.lbStanKonta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lbStanKonta.Font = new System.Drawing.Font("Tahoma", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbStanKonta.Location = new System.Drawing.Point(67, 80);
            this.lbStanKonta.Name = "lbStanKonta";
            this.lbStanKonta.Size = new System.Drawing.Size(94, 41);
            this.lbStanKonta.TabIndex = 1;
            this.lbStanKonta.Text = "1000";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbStanKonta);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 179);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btnPrzelew
            // 
            this.btnPrzelew.Location = new System.Drawing.Point(282, 31);
            this.btnPrzelew.Name = "btnPrzelew";
            this.btnPrzelew.Size = new System.Drawing.Size(75, 23);
            this.btnPrzelew.TabIndex = 3;
            this.btnPrzelew.Text = "przelew";
            this.btnPrzelew.UseVisualStyleBackColor = true;
            this.btnPrzelew.Click += new System.EventHandler(this.btnPrzelew_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kwota";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // edKwota
            // 
            this.edKwota.Location = new System.Drawing.Point(105, 23);
            this.edKwota.Name = "edKwota";
            this.edKwota.Size = new System.Drawing.Size(100, 23);
            this.edKwota.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Na rachunek: ";
            // 
            // edRach
            // 
            this.edRach.Location = new System.Drawing.Point(105, 52);
            this.edRach.Name = "edRach";
            this.edRach.Size = new System.Drawing.Size(223, 23);
            this.edRach.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Odbiorca";
            // 
            // edOdbiorca
            // 
            this.edOdbiorca.Location = new System.Drawing.Point(105, 84);
            this.edOdbiorca.Name = "edOdbiorca";
            this.edOdbiorca.Size = new System.Drawing.Size(223, 23);
            this.edOdbiorca.TabIndex = 5;
            // 
            // grpPrzelewu
            // 
            this.grpPrzelewu.Controls.Add(this.btnAnuluj);
            this.grpPrzelewu.Controls.Add(this.btnPotwierdz);
            this.grpPrzelewu.Controls.Add(this.edOdbiorca);
            this.grpPrzelewu.Controls.Add(this.label3);
            this.grpPrzelewu.Controls.Add(this.label5);
            this.grpPrzelewu.Controls.Add(this.edKwota);
            this.grpPrzelewu.Controls.Add(this.edRach);
            this.grpPrzelewu.Controls.Add(this.label4);
            this.grpPrzelewu.Location = new System.Drawing.Point(381, 22);
            this.grpPrzelewu.Name = "grpPrzelewu";
            this.grpPrzelewu.Size = new System.Drawing.Size(384, 169);
            this.grpPrzelewu.TabIndex = 2;
            this.grpPrzelewu.TabStop = false;
            this.grpPrzelewu.Text = "dane przelewu";
            this.grpPrzelewu.Visible = false;
            // 
            // btnAnuluj
            // 
            this.btnAnuluj.Location = new System.Drawing.Point(303, 140);
            this.btnAnuluj.Name = "btnAnuluj";
            this.btnAnuluj.Size = new System.Drawing.Size(75, 23);
            this.btnAnuluj.TabIndex = 2;
            this.btnAnuluj.Text = "Anuluj";
            this.btnAnuluj.UseVisualStyleBackColor = true;
            this.btnAnuluj.Click += new System.EventHandler(this.btnAnuluj_Click);
            // 
            // btnPotwierdz
            // 
            this.btnPotwierdz.Location = new System.Drawing.Point(151, 140);
            this.btnPotwierdz.Name = "btnPotwierdz";
            this.btnPotwierdz.Size = new System.Drawing.Size(75, 23);
            this.btnPotwierdz.TabIndex = 2;
            this.btnPotwierdz.Text = "Zatwierdź";
            this.btnPotwierdz.UseVisualStyleBackColor = true;
            this.btnPotwierdz.Click += new System.EventHandler(this.btnPotwierdz_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Odbiorca,
            this.Rachunek,
            this.Kwota});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 237);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(799, 270);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Odbiorca
            // 
            this.Odbiorca.Name = "Odbiorca";
            this.Odbiorca.Text = "Odbiorca";
            this.Odbiorca.Width = 150;
            // 
            // Rachunek
            // 
            this.Rachunek.Name = "Rachunek";
            this.Rachunek.Text = "Rachunek";
            this.Rachunek.Width = 200;
            // 
            // Kwota
            // 
            this.Kwota.Name = "Kwota";
            this.Kwota.Text = "Kwota";
            this.Kwota.Width = 100;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 507);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.grpPrzelewu);
            this.Controls.Add(this.btnPrzelew);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpPrzelewu.ResumeLayout(false);
            this.grpPrzelewu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbStanKonta;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrzelew;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edKwota;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox edRach;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox edOdbiorca;
        private System.Windows.Forms.GroupBox grpPrzelewu;
        private System.Windows.Forms.Button btnPotwierdz;
        private System.Windows.Forms.Button btnAnuluj;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Odbiorca;
        private System.Windows.Forms.ColumnHeader Rachunek;
        private System.Windows.Forms.ColumnHeader Kwota;
    }
}