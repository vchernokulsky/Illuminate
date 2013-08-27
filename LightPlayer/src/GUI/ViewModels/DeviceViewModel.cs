using Intems.LightPlayer.BL;

namespace Intems.LightPlayer.GUI.ViewModels
{
    class DeviceViewModel : BaseViewModel
    {
        private readonly Device _device;

        private FrameSequence _sequence;

        public DeviceViewModel(Device device)
        {
            _device = device;
        }

        public string Address { get{return _device.Address;} }

        public int Port { get { return _device.Port; } }

        public string SerialPortName { get; set; }

        public string CompositionFile { get; set; }
    }
}
