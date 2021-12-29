using Imgur.API.Endpoints;
using Imgur.API.Models;
using System;
using System.Collections.Generic;
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
    class Service
    {
        public static void RestartApplication()
        {
            Application.Restart();
            Environment.Exit(0);
        }
    }
}
