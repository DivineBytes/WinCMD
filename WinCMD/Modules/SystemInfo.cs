using Microsoft.VisualBasic.Devices;
using System;
using System.Management;
using WinCMD.Utilities;

namespace WinCMD.Modules
{
    internal class SystemInfo
    {
        public static string GetTotalRAM()
        {
            ComputerInfo computerInfo = new ComputerInfo();
            string size = FileSizeUtilities.GetReadableSize((long)computerInfo.TotalPhysicalMemory);
            return size;
        }

        public static string GetCPU()
        {
            string result = ManagementObjectUtilities.GetValue("select * from Win32_Processor", "Name");
            return result;
        }

        public static string GetGPU()
        {
            string v = ManagementObjectUtilities.GetValue("select * from Win32_VideoController", "Name");
            v = v + " (" + FileSizeUtilities.GetReadableSize(Convert.ToInt64(ManagementObjectUtilities.GetValue("select * from Win32_VideoController", "AdapterRAM"))) + ")"; 
            return v;
        }

        public static string GetOSBit()
        {
            string bitText;
            if (Environment.Is64BitOperatingSystem)
            {
                bitText = "64-bit";
            }
            else
            {
                bitText = "32-bit";
            }

            return bitText;
        }
    }
}