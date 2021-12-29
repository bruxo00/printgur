using Imgur.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prints
{
    class AuthServer // Based on: https://gist.github.com/define-private-public/d05bc52dd0bed1c4699d49e2737e80e7
    {
        public static HttpListener listener;
        public static string url = "http://localhost:6547/";
        public static int pageViews = 0;
        public static int requestCount = 0;
        public static string authFixPage = Properties.Resources.auth_fix.ToString();
        public static string authPage = Properties.Resources.auth.ToString();

        public static async Task HandleIncomingConnections()
        {
            while (true)
            {
                try
                {
                    HttpListenerContext ctx = await listener.GetContextAsync();
                    HttpListenerRequest req = ctx.Request;
                    HttpListenerResponse resp = ctx.Response;

                    if (req.HttpMethod == "GET" && req.Url.AbsolutePath == "/authorize_fix")
                    {
                        byte[] data = Encoding.UTF8.GetBytes(authFixPage);
                        resp.ContentType = "text/html";
                        resp.ContentEncoding = Encoding.UTF8;
                        resp.ContentLength64 = data.LongLength;

                        await resp.OutputStream.WriteAsync(data, 0, data.Length);
                        resp.Close();
                    } 
                    else if(req.HttpMethod == "GET" && req.Url.AbsolutePath == "/authorize")
                    {
                        OAuth2Token token = new OAuth2Token
                        {
                            AccessToken = req.QueryString["access_token"],
                            RefreshToken = req.QueryString["refresh_token"],
                            AccountId = int.Parse(req.QueryString["account_id"]),
                            AccountUsername = req.QueryString["account_username"],
                            ExpiresIn = int.Parse(req.QueryString["expires_in"]),
                            TokenType = req.QueryString["token_type"]
                        };

                        string output = JsonConvert.SerializeObject(token);
                        File.WriteAllText(Settings.authFile, output);

                        byte[] data = Encoding.UTF8.GetBytes(authPage);
                        resp.ContentType = "text/html";
                        resp.ContentEncoding = Encoding.UTF8;
                        resp.ContentLength64 = data.LongLength;

                        await resp.OutputStream.WriteAsync(data, 0, data.Length);
                        resp.Close();

                        Thread.CurrentThread.Abort();
                    } 
                    else
                    {
                        resp.Close();
                    }
                }
                catch(Exception ex) 
                {
                    Console.Write(ex.ToString());
                    throw ex;
                }
            }
        }

        public static void Start()
        {
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();

            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            listener.Close();
        }
    }
}
