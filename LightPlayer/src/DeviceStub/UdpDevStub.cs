using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DeviceStub
{
    internal class UdpDevStub
    {
        private readonly int _id;
        private readonly int _port;
        private readonly UdpClient _client;

        private readonly object _locker = new object();

        public UdpDevStub(int id, int port)
        {
            _id = id;
            _port = port;

            _client = new UdpClient(port, AddressFamily.InterNetwork);
        }

        public void WaitData()
        {
            while (true)
            {
                lock (_locker)
                {
                    var endpoint = new IPEndPoint(IPAddress.Any, 15000);
                    var dataBytes = _client.Receive(ref endpoint);
                    var requestData = Encoding.ASCII.GetString(dataBytes);

                    if (requestData.ToUpper() == "WHERE ARE YOU?")
                    {
                        ProcessDeviceRequest(endpoint, requestData);
                    }
                    else
                    {
                        ProcessCommand(dataBytes);
                    }
                }
            }
        }

        private static void ProcessCommand(byte[] dataBytes)
        {
            var dt = DateTime.Now;

            var sb = new StringBuilder();
            foreach (byte data in dataBytes)
                sb.Append(data.ToString("X") + " ");

            var sResult = String.Format( "{0} : [{1}]", dt.ToString("hh:mm:ss.fff"), sb);
            Console.WriteLine(sResult);
        }

        private void ProcessDeviceRequest(IPEndPoint endpoint, string requestData)
        {
            var str = String.Format("ADDR:{0};PORT:{1};ID:{2}", "127.0.0.1", _port, _id);
            var response = Encoding.ASCII.GetBytes(str);
            _client.Send(response, response.Length, endpoint);

            var senderInfo = String.Format("Sender (ADDR:{0}; PORT:{1})", endpoint.Address, endpoint.Port);
            Console.WriteLine(requestData + "  |  " + senderInfo);
        }
    }
}
