using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using Intems.LightPlayer.Transport;

namespace Intems.LightPlayer.BL
{
    public class Device : IPackageSender
    {
        private readonly IPEndPoint _ipEndPoint;
        private readonly IPackageSender _sender;
        private SequenceCollection _sequenceCollection;


        public Device(string portName)
        {
#if DEBUG
            var serialPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);
            serialPort.Open();
            _sender = new SerialportSender(serialPort);
#endif
        }

        public Device(string addr, int port)
        {
            _sender = new UdpSender(addr, port);
        }

        public Device(IPEndPoint point)
        {
            _ipEndPoint = new IPEndPoint(point.Address, point.Port);
            _sender = new UdpSender(_ipEndPoint.Address.ToString(), _ipEndPoint.Port);
        }

        public Device(IPEndPoint point, SequenceCollection collection) : this(point)
        {
            _sequenceCollection = collection;
        }

        public IPEndPoint IPEndPoint
        {
            get { return _ipEndPoint; }
        }

        public SequenceCollection SequenceCollection
        {
            get { return _sequenceCollection; }
            set { _sequenceCollection = value; }
        }

        public void SetTime(TimeSpan time)
        {
            var frames = _sequenceCollection.FramesByTime(time);
            var packages =
                frames.Select(frm => frm.Command.GetBytes()).Select(bytes => new Package(bytes)).ToList();
            _sender.SendPackages(packages);
        }

        public void SendPackage(Package package)
        {
            if (_sender != null)
                _sender.SendPackage(package);
        }

        public void SendPackages(IEnumerable<Package> packages)
        {
            if(_sender != null)
                _sender.SendPackages(packages);
        }
    }
}
