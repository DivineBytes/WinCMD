using System;
using System.Linq;
using System.Management;

namespace WinCMD.Utilities
{
    public static class ManagementObjectUtilities
    {
        public static string GetValue(string query, string name)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException(nameof(query), "Cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Cannot be null or empty.");
            }

            string result = string.Empty;

            try
            {
                ManagementObjectSearcher win32Proc = new ManagementObjectSearcher(query);

                foreach (ManagementObject obj in win32Proc.Get().Cast<ManagementObject>())
                {
                    result = obj[name].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }
    }
}