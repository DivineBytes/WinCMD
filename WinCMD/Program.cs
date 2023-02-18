using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WinCMD.Modules;
using WinCMD.Properties;
using WinCMD.Utilities;

namespace WinCMD
{
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            Console.Title = Application.ProductName;

            if (args.Length > 0)
            {
                string initialArgument = args[0].ToLower();
                switch (initialArgument)
                {
                    case "about":
                        ShowDescription();
                        break;

                    case "local-ip":
                        Console.WriteLine(NetworkUtilities.GetLocalIP());
                        break;

                    case "public-ip":
                        Console.WriteLine(NetworkUtilities.GetPublicIP());
                        break;

                    case "network":
                        Console.WriteLine(NetworkUtilities.IsNetworkAvailable);
                        break;

                    case "internet":
                        Console.WriteLine(InternetUtilities.InternetAvailable);
                        break;

                    case "cpu":
                        Console.WriteLine(SystemInfo.GetCPU());
                        break;

                    case "gpu":
                        Console.WriteLine(SystemInfo.GetGPU());
                        break;

                    case "ram":
                        Console.WriteLine(SystemInfo.GetTotalRAM());
                        break;

                    case "os-bit":
                        Console.WriteLine(SystemInfo.GetOSBit());
                        break;

                    case "debug":
                        for (int i = 0; i < args.Length; i++)
                        {
                            string argument = args[i];
                            Console.WriteLine($"Argument ({i}): " + argument);
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown parameter/s.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No parameter/s have been passed.");
            }
        }

        /// <summary>
        /// Shows the description.
        /// </summary>
        private static void ShowDescription()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{Application.ProductName} [Version {Application.ProductVersion}]");

            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            stringBuilder.AppendLine($"(c) {fileVersionInfo.CompanyName}. All rights reserved.");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Website: {Settings.Default.Website}");

            Console.WriteLine(stringBuilder.ToString());
        }
    }
}