using System;
using System.Globalization;
using System.Threading;

namespace DeviceStub
{
    class Program
    {
        const int StubCount = 1;
        const int StartPort = 15200;

        private static UdpDevStub[] _devStubs;

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("devicestub <port number>");
                return;
            }

            int port = StartPort;
            Int32.TryParse(args[0], NumberStyles.Number, CultureInfo.InvariantCulture, out port);

            _devStubs = new UdpDevStub[StubCount];
            for (int i = 0; i < StubCount; i++)
            {
                int idx = i;
                _devStubs[i] = new UdpDevStub(idx, port + idx);
                var th = new Thread(() => _devStubs[idx].WaitData());
                th.Start();
                Console.WriteLine("Dev stub start on port: {0}", port);
            }
        }
    }
}
