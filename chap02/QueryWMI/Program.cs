using System;
using System.Management;

namespace QueryWMI
{
    class WMICardGrab
    {
        //private const long OPAQUE = -72057589759737856L; //0xFF000000FF000000
        private const long OPAQUE = unchecked((long)0xFF000000FF000000UL); //-72057589759737856
        public static void Main()
        {
            Console.WriteLine(OPAQUE);

            ManagementObjectSearcher query = new
            ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
        
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                string[] addresses = (string[])mo["IPAddress"];
                string[] subnets = (string[])mo["IPSubnet"];
                string[] defaultgateways =
                     (string[])mo["DefaultIPGateway"];
                Console.WriteLine("Network Card: {0}",
                       mo["Description"]);
                Console.WriteLine("  MAC Address: {0}",
                       mo["MACAddress"]);
                foreach (string ipaddress in addresses)
                {
                    Console.WriteLine("  IP Address: {0}",
                         ipaddress);
                }
                foreach (string subnet in subnets)
                {
                    Console.WriteLine("  Subnet Mask: {0}", subnet);
                }
                if (defaultgateways != null)
                {
                    foreach (string defaultgateway in defaultgateways)
                    {
                        Console.WriteLine("  Gateway: {0}",
                             defaultgateway);
                    }
                }
                
            }

            Console.ReadKey();
        }
    }

}
