using System.Windows.Input;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using Intems.LightPlayer.GUI.ViewModels.Commands;
using Intems.LightPlayer.Transport;

namespace Intems.LightPlayer.GUI.ViewModels
{
    class DeviceViewModel : BaseViewModel
    {
        private readonly Device _device;

        public DeviceViewModel(Device device)
        {
            _device = device;
        }

        public string Address { get{return _device.IPEndPoint.Address.ToString();} }

        public int Port { get { return _device.IPEndPoint.Port; } }

        public string SerialPortName { get; set; }

        public Device Device
        {
            get { return _device; }
        }

        private string _compositionFile;
        public string CompositionFile
        {
            get { return _compositionFile; }
            set { _compositionFile = value; RaisePropertyChanged("CompositionFile"); }
        }

        public ICommand FindDeviceCommand
        {
            get
            {
                var command = new FindDeviceCommand(){};
                return command;
            }
        }

        public void TurnOff()
        {
            if (_device == null) return;

            var cmd = new TurnOff();
            var package = new Package(cmd.GetBytes());
            _device.SendPackage(package);
        }

        public void SetSequenceCollection(FrameSequenceCollection collection)
        {
            if(_device != null)
                _device.SequenceCollection = collection;
        }
    }
}
