using Intems.LightPlayer.BL;

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

        public string CompositionFile { get; set; }
    }
}
