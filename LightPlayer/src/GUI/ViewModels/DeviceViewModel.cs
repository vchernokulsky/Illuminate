using Intems.LightPlayer.BL;

namespace Intems.LightPlayer.GUI.ViewModels
{
    class DeviceViewModel : BaseViewModel
    {
        private Device _device;

        private FrameSequence _sequence;

        public string SerialPortName { get; set; }

        public string Address { get; set; }

        public string Port { get; set; }

        public string CompositionFile { get; set; }
    }
}
