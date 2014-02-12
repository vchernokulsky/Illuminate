using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Input;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.GUI.ViewModels.Commands;
using NAudio.Wave;

namespace Intems.LightPlayer.GUI.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private IWavePlayer _player;
        private FrameProcessor _processor;

        public MainViewModel()
        {
            _deviceViewModels = new List<DeviceViewModel>();

            _player = new WaveOutEvent();
            _processor = new FrameProcessor(_player);

            var action = new Action<IEnumerable<Tuple<Device, string>>>(UpdateDevices);
            _deviceConfigurationModel = new DeviceConfigurationModel(_processor, action);

            _startCommand = new StartCompositionCommand(_player, _processor);
            _stopCommand = new StopCompositionCommand(_player, _processor);
        }

        public MainViewModel(IEnumerable<Device> devices) : this()
        {
            foreach (var device in devices)
                _deviceViewModels.Add(new DeviceViewModel(device));
        }

        private readonly ICommand _startCommand;
        public ICommand StartCommand
        {
            get { return _startCommand; }
        }

        private readonly ICommand _stopCommand;
        public ICommand StopCommand
        {
            get { return _stopCommand; }
        }

        private DeviceConfigurationModel _deviceConfigurationModel;
        public DeviceConfigurationModel DeviceConfigurationModel
        {
            get { return _deviceConfigurationModel; }
            set { _deviceConfigurationModel = value; RaisePropertyChanged("DeviceConfigModel"); }
        }

        private ICollection<DeviceViewModel> _deviceViewModels;
        public  ICollection<DeviceViewModel> DeviceViewModels
        {
            get { return _deviceViewModels; }
            set { _deviceViewModels = value; RaisePropertyChanged("Devices"); }
        }

        private int _deviceCount = 5;
        public int DeviceCount
        {
            get { return _deviceCount; }
            set { _deviceCount = value; RaisePropertyChanged("DeviceCount"); }
        }

        public void UpdateDevices(IEnumerable<Device> devices)
        {
            _deviceViewModels.Clear();
            foreach (var device in devices)
            {
                var deviceViewModel = new DeviceViewModel(device);
                _deviceViewModels.Add(deviceViewModel);
            }
            CollectionViewSource.GetDefaultView(_deviceViewModels).Refresh();
        }

        public void UpdateDevices(IEnumerable<Tuple<Device, string>> configs)
        {
            _deviceViewModels.Clear();
            foreach (var config in configs)
            {
                var deviceViewModel = new DeviceViewModel(config.Item1) {CompositionFile = config.Item2};
                _deviceViewModels.Add(deviceViewModel);
            }
            CollectionViewSource.GetDefaultView(_deviceViewModels).Refresh();
        }
    }
}
