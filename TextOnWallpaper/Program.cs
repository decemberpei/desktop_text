using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextOnWallpaper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            // set auto start
            AutoStart();

            // show init content
            string initContent = GenerateTextContent();
            File.WriteAllText("TextShow.txt", initContent);
            Thread.Sleep(1000);
            Process.Start("Bginfo64.exe", "TextShow.bgi /TIMER:0 /NOLICPROMPT");

            while (true)
            {
                string lastContent = GenerateTextContent();
                Thread.Sleep(30 * 1000);
                string thisContent = GenerateTextContent();
                if(lastContent == thisContent)
                {
                    Console.WriteLine("No content change.");
                }
                else
                {
                    Console.WriteLine("Content changed, do a refresh .");
                    File.WriteAllText("TextShow.txt", thisContent);
                    Thread.Sleep(1000);
                    Process.Start("Bginfo64.exe", "TextShow.bgi /TIMER:0 /NOLICPROMPT");
                }
            }
        }

        public static void AutoStart()
        {
            string src = AppDomain.CurrentDomain.FriendlyName + ".lnk";
            string dest = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\"
                + src;
            if (!File.Exists(dest))
            {
                File.Copy(src, dest, false);
            }
        }

        // override this function to do something different.
        public static string GenerateTextContent()
        {
            return GetHostAndIP();
        }
        private static string GetHostAndIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            string result = Dns.GetHostName() + ":\t";
            foreach (IPAddress ip in host.AddressList)
            {
                string localIP = ip.ToString();
                if(localIP != null && localIP.Split('.').Length == 4)
                {
                    result = result  + localIP + Environment.NewLine + "\t";
                }
            }
            return result;
        }
    }
}
