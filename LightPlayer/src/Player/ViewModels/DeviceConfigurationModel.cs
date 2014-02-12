using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Intems.LightPlayer.BL;
using Microsoft.Win32;

namespace Intems.LightPlayer.GUI.ViewModels
{
    internal class DeviceConfigurationModel : BaseViewModel
    {
        private static FrameProcessor _processor;
        private static Action<IEnumerable<Tuple<Device, string>>> _updateAction;

        class LoadConfigCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }
            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var dlg = new OpenFileDialog { Filter = "Файлы конфигурации(*.config)|*.config" };
                if (dlg.ShowDialog().Value)
                {
                    var cfg = new Configurator(dlg.FileName, _processor);
                    var deviceConfigurations = cfg.GetDeviceConfiguration();
                    cfg.ConfigureProcessor();
                    if(_updateAction != null)
                        _updateAction.Invoke(deviceConfigurations);
                }
            }
        }

        public DeviceConfigurationModel(FrameProcessor processor)
        {
            _processor = processor;
        }

        public DeviceConfigurationModel(FrameProcessor processor, Action<IEnumerable<Tuple<Device, string>>> updateAction)
            : this(processor)
        {
            _updateAction = updateAction;
        }

        private Visibility _visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get { return _visibility; }
            set { _visibility = value; RaisePropertyChanged("Visibility"); }
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; RaisePropertyChanged("FileName"); }
        }

        public ICommand Command {get{return new LoadConfigCommand();}}
    }
}