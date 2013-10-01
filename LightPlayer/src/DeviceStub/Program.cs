using System;
using System.Threading;

namespace DeviceStub
{
    class Program
    {
        private static UdpDevStub[] _devStubs;

        static void Main(string[] args)
        {
            const int stubCount = 3;
            _devStubs = new UdpDevStub[stubCount];

            const int startPort = 15200;
            for (int i = 0; i < stubCount; i++)
            {
                int idx = i;
                _devStubs[i] = new UdpDevStub(idx, startPort + idx);
                var th = new Thread(() => _devStubs[idx].WaitData());
                th.Start();
            }

            Console.ReadKey();
        }
    }
}
