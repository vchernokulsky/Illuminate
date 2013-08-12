using System;
using System.Net;
using System.Net.Sockets;

namespace UdpRecv
{
    class Program
    {
        private const int ListenPort = 5566;

        static void Main(string[] args)
        {
            var listener = new UdpClient(ListenPort);
            var groupEP = new IPEndPoint(IPAddress.Any, ListenPort);

            while (true)
            {
                var bytes = listener.Receive(ref groupEP);
                foreach (var b in bytes)
                    Console.Write(b.ToString("X") + " ");
                Console.WriteLine();
            }
        }
    }
}
