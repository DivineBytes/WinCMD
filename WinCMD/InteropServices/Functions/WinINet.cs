using System.Runtime.InteropServices;
using System.Security;

namespace WinCMD.InteropServices
{
    /// <summary>
    /// The <see cref="WinINet"/> interoperability enables you to invoke unmanaged code.
    /// </summary>
    /// <remarks>
    /// <para>Description: Internet Extensions for Win32.</para>
    /// <para>Entry point: <c>WinINet.dll</c></para>
    /// </remarks>
    [SuppressUnmanagedCodeSecurity]
    public static class WinINet
    {
        /// <summary>
        /// Retrieves the connected state of the local system.
        /// </summary>
        /// <param name="lpdwFlags">
        /// Pointer to a variable that receives the connection description. This parameter may
        /// return a valid flag even when the function returns FALSE. This parameter can be one or
        /// more of the following values.
        /// </param>
        /// <param name="dwReserved">This parameter is reserved and must be 0.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport(ExternalDll.WinInet, SetLastError = true)]
        public static extern bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);
    }
}