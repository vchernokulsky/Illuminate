using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using Intems.LightPlayer.BL;

namespace Intems.LightPlayer.GUI.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            _devices = new List<DeviceViewModel>();
        }

        public MainViewModel(IEnumerable<Device> devices) : this()
        {
            foreach (var device in devices)
                _devices.Add(new DeviceViewModel(device));
        }

        private Visibility _isLoadDeviceVisile = Visibility.Collapsed;
        public Visibility IsLoadDeviceConfig
        {
            get { return _isLoadDeviceVisile; }
            set { _isLoadDeviceVisile = value; RaisePropertyChanged("IsLoadDeviceConfig"); }
        }

        private string _audioFileName;
        public string AudioFileName
        {
            get { return _audioFileName; }
            set { _audioFileName = value; RaisePropertyChanged("AudioFileName"); }
        }

        private ICollection<DeviceViewModel> _devices;
        public  ICollection<DeviceViewModel> Devices
        {
            get { return _devices; }
            set { _devices = value; RaisePropertyChanged("Devices"); }
        }

        private int _deviceCount = 5;
        public int DeviceCount
        {
            get { return _deviceCount; }
            set { _deviceCount = value; RaisePropertyChanged("DeviceCount"); }
        }

        public void UpdateDevices(ICollection<Device> devices)
        {
            _devices.Clear();
            foreach (var device in devices)
            {
                var deviceViewModel = new DeviceViewModel(device);
                _devices.Add(deviceViewModel);
            }
            CollectionViewSource.GetDefaultView(_devices).Refresh();
        }
    }
}
