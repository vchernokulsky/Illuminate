using System;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using Intems.LightPlayer.Transport;

namespace Intems.LightPlayer.GUI.ViewModels.Commands
{
    internal class FindDeviceCommand : ICommand
    {

        public FindDeviceCommand(Device device)
        {
        }

        public bool CanExecute(object parameter)
        {
            var dev = parameter as Device;
            return true;
        }

        public void Execute(object parameter)
        {
            var dev = parameter as Device;
            if (dev != null)
            {
                var cmd1 = new SetColor(Colors.Red){Channel = 1};
                var cmd2 = new SetColor(Colors.Green) { Channel = 1 };
                var pkg1 = new Package(cmd1.GetBytes());
                var pkg2 = new Package(cmd2.GetBytes());

                var packages = new[] {pkg1, pkg2, pkg1, pkg2, pkg1, pkg2};
                foreach (var package in packages)
                {
                    dev.SendPackage(package);
                    Thread.Sleep(500);
                }
            }
        }
        public event EventHandler CanExecuteChanged;
    }
}
