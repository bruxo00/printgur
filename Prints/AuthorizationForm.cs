using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prints
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void foreverButton1_Click(object sender, EventArgs e)
        {
            Auth.Authorize();
            Service.RestartApplication(); // workaround for form not showing bug, fix later
        }

        private void AuthorizationForm_Shown(object sender, EventArgs e)
        {
            LoadApp();
        }

        private void LoadApp()
        {
            if (!Directory.Exists(Settings.appDataFolder))
            {
                Directory.CreateDirectory(Settings.appDataFolder);
            }

            if (!Directory.Exists(Settings.ssFolder))
            {
                Directory.CreateDirectory(Settings.ssFolder);
            }

            Settings.LoadSettings();

            bool isAuthorized = Auth.LoadAuthorization();

            if (isAuthorized)
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
        }

        private void foreverClose1_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }
    }
}
