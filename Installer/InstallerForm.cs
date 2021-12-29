using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Installer
{
    public partial class InstallerForm : Form
    {
        private static string latestReleaseLink = "https://github.com/bruxo00/printgur/releases/latest/download/binaries.zip";
        private string tempFilePath = null;
        private string programFilesPath = null;
        private string exePath = null;
        private string iconPath = null;

        public InstallerForm()
        {
            InitializeComponent();

            Thread t1 = new Thread(GetLatestVersionTag);
            t1.Start();

            programFilesPath = Path.Combine(GetProgramFilesx86Path(), "Printgur");
            exePath = Path.Combine(programFilesPath, "Printgur.exe");
            tempFilePath = Path.Combine(Path.GetTempPath(), "printgur_release.zip");
            iconPath = Path.Combine(programFilesPath, "printgur.ico");
        }

        private void foreverButton1_Click(object sender, EventArgs e)
        {
            if (CheckInstall())
            {
                Install();
            }
        }

        private bool CheckInstall()
        {
            if(Directory.Exists(programFilesPath))
            {
                MessageBox.Show("You already have Printgur installed. Remove the old version if you wish to install again.", "Information");
                return false;
            }

            return true;
        }

        public void GetLatestVersionTag() // just pretend this is good
        {
            string version = "Unknown";

            try
            {
                using (WebClient wc = new WebClient())
                {
                    string html = wc.DownloadString("https://github.com/bruxo00/printgur/releases/latest");
                    version = GetStringBetween(html, "<span class=\"ml-1\">", "</span>").Trim();
                }
            }
            catch { }

            this.foreverLabel1.Invoke((MethodInvoker) delegate {
                this.foreverLabel1.Text = String.Format("You will install version: {0}", version);
            });
        }

        private void DownloadRelease()
        {
            if (System.IO.File.Exists(tempFilePath))
            {
                System.IO.File.Delete(tempFilePath);
            }

            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(latestReleaseLink, Path.Combine(Path.GetTempPath(), "printgur_release.zip"));
            }
        }

        private void ExtractToProgramFiles()
        {
            if (!Directory.Exists(programFilesPath))
            {
                Directory.CreateDirectory(programFilesPath);
            }

            ZipFile.ExtractToDirectory(tempFilePath, programFilesPath);
        }

        private void CreateShortcut()
        {
            string shortcutLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Printgur.lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "Printgur";
            shortcut.IconLocation = iconPath;
            shortcut.TargetPath = exePath;
            shortcut.Save();
        }

        private void AddToStartup()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.SetValue("Printgur", exePath);
        }

        private void AddToContextMenu()
        {
            RegistryKey rk = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\SystemFileAssociations\\image\\shell\\Printgur", true);
            rk.SetValue("", "Upload with Printgur");
            rk.SetValue("Icon", iconPath);

            rk = rk.CreateSubKey("command", true);
            rk.SetValue("", string.Format("{0} \"%1\" -context", exePath));
        }

        public void Install()
        {
            try
            {
                foreverProgressBar1.Value = 10; // classic fake progressbar

                DownloadRelease();

                foreverProgressBar1.Value = 30;

                ExtractToProgramFiles();
                System.IO.File.Delete(tempFilePath);

                foreverProgressBar1.Value = 60;

                if(foreverCheckBox3.Checked)
                {
                    CreateShortcut();
                }

                foreverProgressBar1.Value = 70;

                if(foreverCheckBox2.Checked)
                {
                    AddToStartup();
                }

                foreverProgressBar1.Value = 80;

                if (foreverCheckBox1.Checked)
                {
                    AddToContextMenu();
                }

                foreverProgressBar1.Value = 100;

                MessageBox.Show(null, "Printgur installed successfully! Click OK to exit the installer.", "Success!", MessageBoxButtons.OK);

                if (foreverCheckBox4.Checked)
                {
                    Process.Start(exePath);
                }

                Environment.Exit(-1);
            }
            catch { }
        }

        private string GetProgramFilesx86Path()
        {
            if (8 == IntPtr.Size || (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        private string GetStringBetween(string text, string start, string end) // https://stackoverflow.com/a/46940181
        {
            int p1 = text.IndexOf(start) + start.Length;
            int p2 = text.IndexOf(end, p1);

            if (end == "") return (text.Substring(p1));
            else return text.Substring(p1, p2 - p1);
        }

        private void foreverClose1_Click(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }
    }
}
