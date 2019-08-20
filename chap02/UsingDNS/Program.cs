using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UsingDNS
{
    class Program
    {
        public static void Main()
        {
            string hostName = Dns.GetHostName();
            Console.WriteLine("Local hostname: {0}", hostName);
            IPHostEntry myself = Dns.GetHostEntry(hostName); // Dns.GetHostByName(hostName);
            foreach (IPAddress address in myself.AddressList)
            {
                Console.WriteLine("IP Address: {0}", address.ToString());
            }

            foreach (string alias in myself.Aliases)
            {
                Console.WriteLine("IP Address: {0}", alias);
            }

            Console.ReadKey();
        }
    }
}
