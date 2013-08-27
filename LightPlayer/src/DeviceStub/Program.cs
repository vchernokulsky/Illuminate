using System;

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
                _devStubs[i] = new UdpDevStub(i, startPort + i);
                _devStubs[i].WaitData();
            }

            Console.ReadKey();
        }
    }
}
