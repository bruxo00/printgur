using Imgur.API.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Prints
{
    class Settings
    {
        public bool saveLocally { get; set; }
        public bool usePrtScreenKey { get; set; }
        public bool openInBrowser { get; set; }
        public bool copyToClipboard { get; set; }
        public bool hasSeenMessage { get; set; }
        public Color outlineColor { get; set; }
        public Color hazeColor { get; set; }

        public static string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "printgur");
        public static string ssFolder = Path.Combine(Settings.appDataFolder, "screenshots");
        public static string configFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "printgur", "config.json");
        public static string authFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "printgur", "auth.json");
        public static Settings appSettings; // pretend you are not seeing this

        public static void LoadSettings()
        {
            if (File.Exists(Settings.configFile))
            {
                try
                {
                    string fileContent = File.ReadAllText(Settings.configFile);
                    Settings.appSettings = JsonConvert.DeserializeObject<Settings>(fileContent);
                }
                catch 
                {
                    throw new Exception("Could not load settings.json");
                }
            }
            else
            {
                File.Create(Settings.configFile).Close();

                Settings settings = new Settings();
                settings.saveLocally = false;
                settings.usePrtScreenKey = true;
                settings.openInBrowser = true;
                settings.copyToClipboard = true;
                settings.hasSeenMessage = false;
                settings.outlineColor = Color.FromArgb(35, 168, 109);
                settings.hazeColor = Color.FromArgb(35, 168, 109);

                Settings.appSettings = settings;
                Settings.SaveSettings();
            }
        }

        public static void SaveSettings()
        {
            string output = JsonConvert.SerializeObject(Settings.appSettings);
            File.WriteAllText(Settings.configFile, output);
        }
    }
}
