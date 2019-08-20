using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AddressSample
{
    class Program
    {
        public static void Main()
        {
            IPAddress test1 = IPAddress.Parse("192.168.1.1");
            IPAddress loopback = IPAddress.Loopback;
            IPAddress broadcast = IPAddress.Broadcast;
            IPAddress any = IPAddress.Any;
            IPAddress none = IPAddress.None;
            IPHostEntry ihe = Dns.GetHostEntry(Dns.GetHostName());
            //Dns.GetHostByName(Dns.GetHostName());
            IPAddress myself = ihe.AddressList.FirstOrDefault(a=>a.AddressFamily==AddressFamily.InterNetwork); // ihe.AddressList[0];
            if (IPAddress.IsLoopback(loopback))
            {
                Console.WriteLine("The Loopback address is: {0}", loopback.ToString());
            }
            else
            {
                Console.WriteLine("Error obtaining the loopback address");
            }
                
            Console.WriteLine("The Local IP address is: {0}\n", myself.ToString());
            if (myself == loopback)
            {
                Console.WriteLine("The loopback address is the same as local address.\n");
            }

            else
            {
                Console.WriteLine("The loopback address is not the local address.\n");
            }

                

            Console.WriteLine("The test address is: {0}",
                    test1.ToString());
            Console.WriteLine("Broadcast address: {0}",
                    broadcast.ToString());
            Console.WriteLine("The ANY address is: {0}",
                    any.ToString());
            Console.WriteLine("The NONE address is: {0}",
                    none.ToString());

            Console.ReadKey();
        }

    }
}
