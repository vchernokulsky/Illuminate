using System;
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
                var endpoint = new IPEndPoint(IPAddress.Any, 15000);
                var dataBytes = _client.Receive(ref endpoint);
                var senderInfo = String.Format("Sender (ADDR:{0}; PORT:{1})", endpoint.Address, endpoint.Port);
                var requestData = Encoding.ASCII.GetString(dataBytes);
                Console.WriteLine(requestData + "  |  " + senderInfo);

                var str = String.Format("ADDR:{0};PORT:{1};ID:{2}", "127.0.0.1", _port, _id);
                var response = Encoding.ASCII.GetBytes(str);
                _client.Send(response, response.Length, endpoint);
            }
        }
    }
}
