using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Intems.LightPlayer.BL;

namespace Intems.LightPlayer.GUI
{
    internal class Configurator
    {
        private const string DefaultPort = "9750";

        private readonly string _configPath;
        private readonly FrameProcessor _processor;
        private ICollection<Device> _devices;

        public Configurator(string path)
        {
            _configPath = path;
        }

        public Configurator(string path, FrameProcessor processor) : this(path)
        {
            _processor = processor;
        }

        public IEnumerable<Device> Devices
        {
            get { return _devices; }
        }

        public void ConfigureProcessor()
        {
            var devices = CreateDevices();
            if(_processor != null)
                _processor.SetDevices(devices);
        }

        public IEnumerable<Device> CreateDevices()
        {
            _devices = new List<Device>();
            if (File.Exists(_configPath))
            {
                var xDoc = XDocument.Load(_configPath);
                if (xDoc.Root != null)
                {
                    IEnumerable<XElement> deviceNodes = xDoc.Root.Descendants("device");
                    foreach (var node in deviceNodes)
                    {
                        string addr;
                        var addressAttribute = node.Attributes("address").FirstOrDefault();
                        if (addressAttribute != null)
                            addr = addressAttribute.Value;
                        else
                            throw new Exception("Incorrect device configuration address attribute needs");

                        var port = DefaultPort;
                        var portAttribute = node.Attributes("port").FirstOrDefault();
                        if (portAttribute != null)
                            port = portAttribute.Value;

                        var device = new Device(addr, Int32.Parse(port));
                        _devices.Add(device);
                    }
                }
            }
            return _devices;
        }
    }
}
