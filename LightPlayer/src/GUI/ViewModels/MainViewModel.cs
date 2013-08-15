using System.Collections.Generic;
using System.ComponentModel;

namespace Intems.LightPlayer.GUI.ViewModels
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propName)
        {
            var pc = PropertyChanged;
            if (pc != null)
                pc(this, new PropertyChangedEventArgs(propName));
        }
    }

    internal class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            _devices = new List<DeviceViewModel>();
            _devices.Add(new DeviceViewModel());
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
    }
}
