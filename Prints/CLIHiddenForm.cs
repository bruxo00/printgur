using Imgur.API.Endpoints;
using Imgur.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prints
{
    public partial class CLIHiddenForm : Form
    {
        public CLIHiddenForm(string[] args)
        {
            InitializeComponent();

            this.Opacity = 0;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();

            UploadFromCLI(args);
        }

        private static void UploadFromCLI(string[] args)
        {
            if (args.Length > 1 && args[1] == "-upload")
            {
                string filePath = args[2];

                if(CheckFileExtension(filePath))
                {
                    _ = UploadImageCLIAsync(filePath);
                }
                else
                {
                    Environment.Exit(-1);
                }
            }
        }

        private static async Task UploadImageCLIAsync(string imagePath)
        {
            FileStream fileStream = File.OpenRead(imagePath);
            Auth.apiClient.SetOAuth2Token(Auth.token);

            ImageEndpoint imageEndpoint = new ImageEndpoint(Auth.apiClient, new HttpClient());
            IImage imageUpload = await imageEndpoint.UploadImageAsync(fileStream);

            Process.Start(imageUpload.Link);

            Environment.Exit(-1);
        }

        private static bool CheckFileExtension(string imagePath)
        {
            string[] allowedExtensions = { "jpeg", "jpg", "png", "gif", "apng", "tiff" };
            string[] temp = imagePath.Split('.');
            string extension = temp[temp.Length - 1];

            return allowedExtensions.Contains(extension);
        }
    }
}
