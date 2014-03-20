using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Xml.Linq;
using Intems.LightPlayer.BL;

namespace Intems.LightPlayer.GUI
{
    internal class Configurator
    {
        private const string DefaultPort = "9750";

        private readonly string _configPath;
        private readonly FrameProcessor _processor;
        private readonly ICollection<Device> _devices;

        public Configurator(string path)
        {
            _configPath = path;
            _devices = new Collection<Device>();
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
            var devices = GetDeviceConfiguration();
            if (_processor != null)
            {
                var onlyDevs = devices.Select(x => x.Item1);
                _processor.SetDevices(onlyDevs);
            }
        }

        public IEnumerable<Tuple<Device, string>> GetDeviceConfiguration()
        {
            var devices = new List<Tuple<Device, string>>();
            if (!File.Exists(_configPath)) return devices;

            var xDoc = XDocument.Load(_configPath);
            if (xDoc.Root == null) return devices;

            var deviceNodes = xDoc.Root.Descendants("device");
            foreach (var node in deviceNodes)
            {
                var addr = GetAddress(node);
                var port = GetPort(node);
                var file = GetFile(node);

                var device = new Device(addr, Int32.Parse(port));

                try
                {
                    LoadComposition(file, device);

                    devices.Add(new Tuple<Device, string>(device, Path.GetFullPath(file)));
                    _devices.Add(device);
                }
                catch (FileNotFoundException fnfex)
                {
                    var msg = String.Format("Отсутствует файл: {0}, проверьте конфигурационный файл.", fnfex.FileName);
                    SimpleLog.Log.Error(msg, fnfex);
                    MessageBox.Show(msg, "Ошибка", MessageBoxButton.OK);
                    break;
                }
                catch (Exception ex)
                {
                    var msg = "Ошибка загрузки композиции, возможно файл повреждён.";
                    SimpleLog.Log.Error(msg, ex);
                    MessageBox.Show(msg);
                    break;
                }
            }
            return devices;
        }

        private static void LoadComposition(string file, Device device)
        {
            using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var bf = new BinaryFormatter();
                var fsc = (FrameSequenceCollection) bf.Deserialize(stream);
                device.SequenceCollection = fsc;
            }
        }

        private static string GetFile(XElement node)
        {
            var file = String.Empty;
            var fileAttribute = node.Attributes("file").FirstOrDefault();
            if (fileAttribute != null)
                file = fileAttribute.Value;
            return file;
        }

        private static string GetPort(XElement node)
        {
            var port = DefaultPort;
            var portAttribute = node.Attributes("port").FirstOrDefault();
            if (portAttribute != null)
                port = portAttribute.Value;
            return port;
        }

        private static string GetAddress(XElement node)
        {
            string addr;
            var addressAttribute = node.Attributes("address").FirstOrDefault();
            if (addressAttribute != null)
                addr = addressAttribute.Value;
            else
                throw new Exception("Incorrect device configuration address attribute needs");
            return addr;
        }
    }
}
