using GamingFramework.ConnectFour.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace GamingFramework
{
    static class Helpers
    {
        public const string HelpGamingURL = "https://github.com/sandeep-bhaskar/Sharepoint-List-Console-Application";
        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        public static int GetGameOptionsFromString(string str)
        {
            switch (str)
            {
                case "S":
                case "s":
                    Console.WriteLine("Game Saved...");
                        return (int)GameOptions.Save;
                case "H":
                case "h":
                    OpenBrowser(Helpers.HelpGamingURL);
                    return (int)GameOptions.Help;
                case "R":
                case "r":
                    return (int)GameOptions.Resume;
                case "N":
                case "n":
                    return (int)GameOptions.NewGame;
                case "E":
                case "e":
                    Environment.Exit(-1);
                    return (int)GameOptions.Exit;
                default:
                    var jugada = -1;
                    if (int.TryParse(str, out jugada))
                    {
                        return jugada;
                    }
                    else 
                    {
                        return 0;
                    }
            }
        }
    }
}
