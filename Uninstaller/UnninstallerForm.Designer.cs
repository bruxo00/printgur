
namespace Uninstaller
{
    partial class UnninstallerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnninstallerForm));
            this.foreverForm1 = new ReaLTaiizor.Forms.ForeverForm();
            this.foreverButton2 = new ReaLTaiizor.Controls.ForeverButton();
            this.foreverButton1 = new ReaLTaiizor.Controls.ForeverButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.foreverForm1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // foreverForm1
            // 
            this.foreverForm1.BackColor = System.Drawing.Color.White;
            this.foreverForm1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverForm1.BorderColor = System.Drawing.Color.DodgerBlue;
            this.foreverForm1.Controls.Add(this.foreverButton2);
            this.foreverForm1.Controls.Add(this.foreverButton1);
            this.foreverForm1.Controls.Add(this.pictureBox1);
            this.foreverForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foreverForm1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.foreverForm1.ForeverColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.foreverForm1.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.foreverForm1.HeaderMaximize = false;
            this.foreverForm1.HeaderTextFont = new System.Drawing.Font("Segoe UI", 12F);
            this.foreverForm1.Image = null;
            this.foreverForm1.Location = new System.Drawing.Point(0, 0);
            this.foreverForm1.MinimumSize = new System.Drawing.Size(210, 50);
            this.foreverForm1.Name = "foreverForm1";
            this.foreverForm1.Padding = new System.Windows.Forms.Padding(1, 51, 1, 1);
            this.foreverForm1.Sizable = true;
            this.foreverForm1.Size = new System.Drawing.Size(313, 263);
            this.foreverForm1.TabIndex = 0;
            this.foreverForm1.Text = "Printgur Unninstaller";
            this.foreverForm1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.foreverForm1.TextLight = System.Drawing.Color.SeaGreen;
            // 
            // foreverButton2
            // 
            this.foreverButton2.BackColor = System.Drawing.Color.Transparent;
            this.foreverButton2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.foreverButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.foreverButton2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.foreverButton2.Location = new System.Drawing.Point(4, 213);
            this.foreverButton2.Name = "foreverButton2";
            this.foreverButton2.Rounded = false;
            this.foreverButton2.Size = new System.Drawing.Size(306, 46);
            this.foreverButton2.TabIndex = 3;
            this.foreverButton2.Text = "Exit";
            this.foreverButton2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.foreverButton2.Click += new System.EventHandler(this.foreverButton2_Click);
            // 
            // foreverButton1
            // 
            this.foreverButton1.BackColor = System.Drawing.Color.Transparent;
            this.foreverButton1.BaseColor = System.Drawing.Color.Red;
            this.foreverButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.foreverButton1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.foreverButton1.Location = new System.Drawing.Point(4, 161);
            this.foreverButton1.Name = "foreverButton1";
            this.foreverButton1.Rounded = false;
            this.foreverButton1.Size = new System.Drawing.Size(306, 46);
            this.foreverButton1.TabIndex = 2;
            this.foreverButton1.Text = "Uninstall";
            this.foreverButton1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.foreverButton1.Click += new System.EventHandler(this.foreverButton1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Uninstaller.Properties.Resources.printgur_white;
            this.pictureBox1.Location = new System.Drawing.Point(4, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(306, 101);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // UnninstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 263);
            this.Controls.Add(this.foreverForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UnninstallerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.foreverForm1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Forms.ForeverForm foreverForm1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ReaLTaiizor.Controls.ForeverButton foreverButton2;
        private ReaLTaiizor.Controls.ForeverButton foreverButton1;
    }
}

