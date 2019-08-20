using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryRegistry
{
    class Program
    {
        static void PrintInfo(string networkcardKeyName)
        {
            RegistryKey cardServiceName = Registry.LocalMachine.OpenSubKey(networkcardKeyName);
            if (cardServiceName == null)
            {
                Console.WriteLine($"Bad registry key: {networkcardKeyName}");
                return;
            }

            var deviceServiceName = (string)cardServiceName.GetValue("ServiceName");
            var deviceName = (string)cardServiceName.GetValue("Description");
            Console.WriteLine("\nNetwork card: {0}", deviceName);

            string serviceKey = "SYSTEM\\CurrentControlSet\\Services\\";
            var serviceName = serviceKey + deviceServiceName + "\\Parameters\\Tcpip";

            var networkKey = Registry.LocalMachine.OpenSubKey(serviceName);

            if (networkKey == null)
            {
                Console.WriteLine("  No IP configuration set");
                return;
            }
            
            string[] ipaddresses = (string[])networkKey.GetValue("IPAddress");
            string[] defaultGateways = (string[])networkKey.GetValue("DefaultGateway");
            string[] subnetmasks = (string[])networkKey.GetValue("SubnetMask");

            if (ipaddresses != null)
            {
                foreach (string ipaddress in ipaddresses)
                {
                    Console.WriteLine($"  IP Address: {ipaddress}");
                }
            }

            if (subnetmasks != null)
            {
                foreach (string subnetmask in subnetmasks)
                {
                    Console.WriteLine($"  Subnet Mask: {subnetmask}");
                }
            }

            if (defaultGateways != null)
            {
                foreach (string defaultGateway in defaultGateways)
                {
                    Console.WriteLine($"  Gateway: {defaultGateway}");
                }
            }
                
            networkKey.Close();
        }

        public static void Main()
        {
            string networkcardKey = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\NetworkCards";

            string[] networkCards = GetNetworkCards(networkcardKey);
            if (networkCards != null)
            {
                foreach (string keyName in networkCards)
                {
                    var networkcardKeyName = networkcardKey + "\\" + keyName;

                    PrintInfo(networkcardKeyName);
                }
            }

            //start.Close();

            Console.ReadKey();
        }

        static string[] GetNetworkCards(string networkcardKey)
        {
            RegistryKey serviceNames =
                      Registry.LocalMachine.OpenSubKey(networkcardKey);
            if (serviceNames == null)
            {
                Console.WriteLine("Bad registry key");
                return null;
            }
            string[] networkCards = serviceNames.GetSubKeyNames();
            serviceNames.Close();
            return networkCards;
        }
    }
}
