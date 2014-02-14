using System;
using System.Threading;
using System.Windows.Input;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using Intems.LightPlayer.Transport;

namespace Intems.LightPlayer.GUI.ViewModels.Commands
{
    class TurnOffDeviceCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var dev = parameter as Device;
            if (dev == null) return;

            var cmd = new TurnOff(){Channel = Command.DefaultChannel};
            var package = new Package(cmd.GetBytes());
            var thread = new Thread(x => dev.SendPackage(package));
            thread.Start();
        }

        public event EventHandler CanExecuteChanged;
    }
}
