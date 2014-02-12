using System;
using System.Windows.Input;

namespace Intems.LightPlayer.GUI.ViewModels.Commands
{
    class TurnOffDeviceCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}
