using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using System.Net.Http;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Prints
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadSettings();

            SnippingTool.AreaSelected += OnAreaSelected;
            foreverLabel2.Text = Application.ProductVersion;

            notifyIcon1.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon1.ContextMenuStrip.Items.Add("Open", null, MenuItem3_Click);
            notifyIcon1.ContextMenuStrip.Items.Add("Take Screenshot", null, MenuItem1_Click);
            notifyIcon1.ContextMenuStrip.Items.Add("Exit", null, MenuItem2_Click);
        }

        void MenuItem3_Click(object sender, EventArgs e)
        {
            ShowApp();
        }

        void MenuItem2_Click(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }

        void MenuItem1_Click(object sender, EventArgs e)
        {
            SnippingTool.Snip();
        }

        private void foreverButton1_Click_1(object sender, EventArgs e)
        {
            SnippingTool.Snip();
        }

        private static void OnAreaSelected(object sender, EventArgs e)
        {
            System.Drawing.Image bmp = SnippingTool.Image;
            _ = UploadImageAsync(bmp);
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        private static async Task UploadImageAsync(System.Drawing.Image image)
        {
            string imagePath = Path.Combine(Settings.ssFolder, String.Format("{0}.png", DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")));
            image.Save(imagePath);

            FileStream fileStream = File.OpenRead(imagePath);

            Auth.apiClient.SetOAuth2Token(Auth.token);

            ImageEndpoint imageEndpoint = new ImageEndpoint(Auth.apiClient, new HttpClient());
            IImage imageUpload = await imageEndpoint.UploadImageAsync(fileStream);

            if(Settings.appSettings.openInBrowser)
            {
                Process.Start(imageUpload.Link);
            }

            if (Settings.appSettings.copyToClipboard)
            {
                Clipboard.SetText(imageUpload.Link);
            }

            if (!Settings.appSettings.saveLocally)
            {
                File.Delete(imagePath);
            }
        }

        private void foreverCheckBox1_CheckedChanged(object sender)
        {
            UpdateSettings();
        }

        private void LoadSettings()
        {
            foreverCheckBox1.Checked = Settings.appSettings.saveLocally;
            foreverCheckBox3.Checked = Settings.appSettings.usePrtScreenKey;
            foreverCheckBox4.Checked = Settings.appSettings.openInBrowser;
            foreverCheckBox5.Checked = Settings.appSettings.copyToClipboard;
            foreverButton5.BaseColor = Settings.appSettings.outlineColor;
            foreverButton6.BaseColor = Settings.appSettings.hazeColor;
        }

        private void UpdateSettings()
        {
            Settings.appSettings.saveLocally = foreverCheckBox1.Checked;
            Settings.appSettings.usePrtScreenKey = foreverCheckBox3.Checked;
            Settings.appSettings.openInBrowser = foreverCheckBox4.Checked;
            Settings.appSettings.copyToClipboard = foreverCheckBox5.Checked;

            Settings.SaveSettings();
        }

        private void foreverCheckBox3_CheckedChanged(object sender)
        {
            UpdateSettings();
        }

        private void foreverCheckBox4_CheckedChanged(object sender)
        {
            UpdateSettings();
        }

        private void foreverCheckBox5_CheckedChanged(object sender)
        {
            UpdateSettings();
        }

        private void foreverClose1_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }

        private void foreverButton2_Click_1(object sender, EventArgs e)
        {
            Process.Start(Settings.ssFolder);
        }

        private void foreverButton4_Click(object sender, EventArgs e)
        {
            File.Delete(Settings.authFile);
            Service.RestartApplication();
        }

        private void foreverButton3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will delete ALL of your local screenshots? Proceed?", "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Directory.Delete(Settings.ssFolder, true);
                Directory.CreateDirectory(Settings.ssFolder);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;

                if(!Settings.appSettings.hasSeenMessage)
                {
                    notifyIcon1.ShowBalloonTip(1000);
                    Settings.appSettings.hasSeenMessage = true;
                    Settings.SaveSettings();
                }
            }
        }

        private void ShowApp()
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void foreverButton5_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Settings.appSettings.outlineColor = colorDialog1.Color;
                foreverButton5.BaseColor = colorDialog1.Color;
                Settings.SaveSettings();
            }
        }

        private void foreverButton6_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Settings.appSettings.hazeColor = colorDialog1.Color;
                foreverButton6.BaseColor = colorDialog1.Color;
                Settings.SaveSettings();
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                SnippingTool.Snip();
            }
        }
    }
}
