using System;
using System.Collections.Generic;
using System.Windows.Input;
using Intems.LightPlayer.BL;
using Microsoft.Win32;

namespace Intems.LightPlayer.GUI.ViewModels.Commands
{
    internal class LoadConfigCommand : ICommand
    {
        private readonly FrameProcessor _processor;
        private readonly Action<IEnumerable<Tuple<Device, string>>> _updateAction;

        public LoadConfigCommand(FrameProcessor processor, Action<IEnumerable<Tuple<Device, string>>> updateAction)
        {
            _processor = processor;
            _updateAction = updateAction;
        }

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
                if (_updateAction != null)
                    _updateAction.Invoke(deviceConfigurations);
            }
        }
    }
}