using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uninstaller
{
    public partial class UnninstallerForm : Form
    {
        private string tempFilePath = null;
        private string programFilesPath = null;
        private string exePath = null;
        private string iconPath = null;
        private string appDataFolder;

        public UnninstallerForm()
        {
            InitializeComponent();

            programFilesPath = Path.Combine(GetProgramFilesx86Path(), "Printgur");
            exePath = Path.Combine(programFilesPath, "Printgur.exe");
            tempFilePath = Path.Combine(Path.GetTempPath(), "printgur_release.zip");
            iconPath = Path.Combine(programFilesPath, "printgur.ico");
            appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "printgur");

            if (!Directory.Exists(programFilesPath))
            {
                MessageBox.Show(null, "Printgur doesn't seem to be installed.", "Error", MessageBoxButtons.OK);
                Environment.Exit(-1);
            }
        }

        private void foreverButton2_Click(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }

        private void foreverButton1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(null, "This will delete ALL files related to Printgur, screenshots included. Do you want to proceed?", "Warning", MessageBoxButtons.YesNo);

            if(res == DialogResult.Yes)
            {
                Uninstall();
            }
        }

        private string GetProgramFilesx86Path()
        {
            if (8 == IntPtr.Size || (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        private void Uninstall()
        {
            Process[] processList = Process.GetProcessesByName("Printgur");

            foreach(Process p in processList)
            {
                p.Kill();
            }

            // HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.DeleteValue("Printgur");

            // HKEY_LOCAL_MACHINE\SOFTWARE\Classes\SystemFileAssociations\image\shell
            rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Classes\\SystemFileAssociations\\image\\shell", true);
            rk.DeleteSubKeyTree("Printgur");

            Directory.Delete(appDataFolder, true);
            Directory.Delete(programFilesPath, true);

            MessageBox.Show(null, "Uninstall complete. The uninstaller will now exit.", "Success", MessageBoxButtons.OK);

            Environment.Exit(-1);
        }
    }
}
