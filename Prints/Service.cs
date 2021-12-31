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
        public static GlobalKeyboardHook keyboardHook;
        public static void RestartApplication()
        {
            Application.Restart();
            Environment.Exit(0);
        }

        public static void LoadKeyboardHook()
        {
            keyboardHook = new GlobalKeyboardHook();
            keyboardHook.KeyboardPressed += OnKeyPressed;
        }

        private static void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if(e.KeyboardData.VirtualCode == GlobalKeyboardHook.VkSnapshot && e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown && Settings.appSettings.usePrtScreenKey)
            {
                SnippingTool.Snip();
                e.Handled = true;
            }
        }
    }
}
