using WinCMD.InteropServices;

namespace WinCMD.Utilities
{
    /// <summary>
    ///     The <see cref="InternetUtilities" /> class.
    /// </summary>
    public class InternetUtilities
    {
        /// <summary>Retrieves the connected state of the local system.</summary>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsInternetAvailable
        {
            get
            {
                try
                {
                    int flags;
                    return WinINet.InternetGetConnectedState(out flags, 0);
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
