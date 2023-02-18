using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace WinCMD.Utilities
{
    /// <summary>
    /// The <see cref="NetworkUtilities"/> class.
    /// </summary>
    public class NetworkUtilities
    {
        /// <summary>
        /// The device interface enumerator.
        /// </summary>
        public enum DeviceInterface
        {
            /// <summary>
            /// The ethernet.
            /// </summary>
            Ethernet = 0,

            /// <summary>
            /// The wifi.
            /// </summary>
            Wireless = 1
        }

        /// <summary>
        /// Gets the default gateway.
        /// </summary>
        /// <param name="networkInterfaceType">Type of the network interface.</param>
        /// <returns></returns>
        public static string GetDefaultGateway(NetworkInterfaceType networkInterfaceType = NetworkInterfaceType.Ethernet)
        {
            string output = string.Empty;
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.NetworkInterfaceType == networkInterfaceType && networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (GatewayIPAddressInformation gatewayAddress in networkInterface.GetIPProperties().GatewayAddresses)
                    {
                        if (gatewayAddress.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = gatewayAddress.Address.ToString();
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Gets the local ip.
        /// </summary>
        /// <param name="networkInterfaceType">Type of the network interface.</param>
        /// <returns></returns>
        public static string GetLocalIP(NetworkInterfaceType networkInterfaceType = NetworkInterfaceType.Ethernet)
        {
            string output = string.Empty;
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.NetworkInterfaceType == networkInterfaceType && networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation unicastIpAddressInformation in networkInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (unicastIpAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork & !IPAddress.IsLoopback(unicastIpAddressInformation.Address))
                        {
                            output = unicastIpAddressInformation.Address.ToString();
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Gets the local Internet Protocol (IP) version 4 address (IPv4) using the specified <see cref="NetworkInterfaceType"/>.
        /// </summary>
        /// <param name="interfaceType">The network interface type.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetLocalIPv4Address(NetworkInterfaceType interfaceType)
        {
            // Variables
            string output = string.Empty;

            // Loop all network interfaces
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Retrieve gateway IP information
                GatewayIPAddressInformation gatewayIpAddressInformation = networkInterface.GetIPProperties().GatewayAddresses[0];

                // Check if not null or empty
                if (gatewayIpAddressInformation != null && !gatewayIpAddressInformation.ToString().Equals(Empty.ToString()))
                {
                    // Check if is operational and type equals to the specified type
                    if (networkInterface.NetworkInterfaceType == interfaceType && networkInterface.OperationalStatus == OperationalStatus.Up)
                    // && !networkInterface.Description.ToLower().Contains("virtual") && !networkInterface.Description.ToLower().Contains("pseudo"))
                    {
                        // Loop thru each address that is assigned to the interface
                        foreach (UnicastIPAddressInformation unicastIpAddressInformation in networkInterface.GetIPProperties().UnicastAddresses)
                        {
                            // Check if the address family is from the InterNetwork
                            if (unicastIpAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                output = unicastIpAddressInformation.Address.ToString();
                            }
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Gets the local ip.
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }

        /// <summary>
        /// Get the public Internet Protocol (IP) address.
        /// </summary>
        /// <param name="address">The URI address.</param>
        /// <returns>The <see cref="string"/> result.</returns>
        public static string GetPublicIP(string address = IPServices.CheckIPAmazonAws)
        {
            if (!string.IsNullOrEmpty(address))
            {
                string externalIpString = new WebClient().DownloadString(address).Replace("\\r\\n", "").Replace("\\n", "").Trim();
                IPAddress externalIp = IPAddress.Parse(externalIpString);
                return externalIp.ToString();
            }

            return Empty.ToString();
        }

        /// <summary>
        /// The <see cref="IPServices"/> class.///
        /// </summary>
        public class IPServices
        {
            public const string CheckIPAmazonAws = "https://checkip.amazonaws.com/";
            public const string ICanHazIP = "https://icanhazip.com/";
            public const string IPIfy = "https://api.ipify.org";
            public const string IPInfo = "https://ipinfo.io/ip/";
            public const string WTFIsMyIP = "https://wtfismyip.com/text";
        }

        /// <summary>
        /// Gets the <see cref="Empty"/> Internet Protocol (IP) address value.
        /// </summary>
        public static IPAddress Empty
        {
            get
            {
                return IPAddress.Parse("0.0.0.0");
            }
        }

        /// <summary>
        /// Gets the IPv4 gateway address for the current interface.
        /// </summary>
        public static IPAddress Gateway
        {
            get
            {
                // Variables
                IPAddress output = Empty;

                // Loop all network interfaces
                foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    // Retrieve first or default Gateway address information
                    GatewayIPAddressInformation gatewayIpAddressInformation = networkInterface.GetIPProperties().GatewayAddresses[0];

                    // Check if not null
                    if (gatewayIpAddressInformation != null)
                    {
                        output = gatewayIpAddressInformation.Address;
                    }
                }

                return output;
            }
        }

        /// <summary>
        /// Gets the local host Internet Protocol (IP) address value.
        /// </summary>
        public static IPAddress Localhost
        {
            get
            {
                return IPAddress.Parse("127.0.0.1");
            }
        }

        /// <summary>
        /// Returns objects that describe network interfaces on the local computer.
        /// </summary>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<NetworkInterface> GetAllNetworkInterfaces()
        {
            List<NetworkInterface> list = new List<NetworkInterface>();

            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                list.Add(netInterface);
            }

            return list;
        }

        /// <summary>
        /// Gets the local Internet Protocol (IP) addresses using the specified device interface.
        /// </summary>
        /// <param name="deviceInterface">The device interface type.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetLocalIPv4Address(DeviceInterface deviceInterface)
        {
            // Variables
            string ipAddress;

            // Determine the device interface
            switch (deviceInterface)
            {
                case DeviceInterface.Ethernet:
                    {
                        ipAddress = GetLocalIPv4Address(NetworkInterfaceType.Ethernet);
                        break;
                    }

                case DeviceInterface.Wireless:
                    {
                        ipAddress = GetLocalIPv4Address(NetworkInterfaceType.Wireless80211);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(deviceInterface), deviceInterface, null);
                    }
            }

            return ipAddress;
        }

        /// <summary>
        /// Get the public Internet Protocol (IP) address.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetPublicIP()
        {
            // Variables
            string publicIPAddress = string.Empty;

            try
            {
                // Variables
                string webContentResponse;

                // Create web request for the public Internet Protocol (IP) address
                WebRequest webRequest = WebRequest.Create(IPServices.CheckIPAmazonAws);

                // Retrieve the web response content to a string
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    // Read the content response using the stream reader
                    using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        webContentResponse = streamReader.ReadToEnd();
                    }
                }

                // Return value
                publicIPAddress = webContentResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return publicIPAddress;
        }

        /// <summary>
        /// Indicates whether any network connection is available.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsNetworkAvailable
        {
            get
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
        }

        /// <summary>
        /// Gets all the local Internet Protocol (IP) addresses from the host.
        /// </summary>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<string> GetAllLocalIPAddresses()
        {
            // Variables
            List<string> list = new List<string>();

            try
            {
                // Resolve hostname
                IPHostEntry hostName = Dns.GetHostEntry(Dns.GetHostName());

                // Loop thru each Internet Protocol (IP) address
                foreach (IPAddress internetProtocolAddress in hostName.AddressList)
                {
                    // Add only if Address Family is InterNetwork
                    if (internetProtocolAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        list.Add(internetProtocolAddress.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return list;
        }
    }
}