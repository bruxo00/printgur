using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Prints
{
    class Auth
    {
        public static OAuth2Token token;
        public static ApiClient apiClient = new ApiClient(ConfigurationManager.AppSettings["apiClientId"], ConfigurationManager.AppSettings["apiSecret"]);
        public static void Authorize()
        {
            HttpClient httpClient = new HttpClient();
            OAuth2Endpoint oAuth2Endpoint = new OAuth2Endpoint(apiClient, httpClient);
            string authUrl = oAuth2Endpoint.GetAuthorizationUrl();

            Process.Start(authUrl);

            Thread thread = new Thread(AuthServer.Start);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            while (true)
            {
                if (!thread.IsAlive)
                {
                    break;
                }

                Thread.Sleep(1000);
            }
        }

        public static bool LoadAuthorization()
        {
            if(File.Exists(Settings.authFile))
            {
                try
                {
                    string fileContent = File.ReadAllText(Settings.authFile);
                    token = JsonConvert.DeserializeObject<OAuth2Token>(fileContent);

                    return true;
                }
                catch 
                {
                    return false;
                }
            } 
            else
            {
                return false;
            }
        }
    }
}
