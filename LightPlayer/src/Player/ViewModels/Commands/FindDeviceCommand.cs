using System;
using System.Collections.Generic;
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
        private readonly Device _device;
        public FindDeviceCommand(Device device)
        {
            _device = device;
        }

        public bool CanExecute(object parameter)
        {
            return _device != null;
        }

        public void Execute(object parameter)
        {
            var dev = parameter as Device;
            if (dev == null) return;

            var packages = CreateBlinkPackages();
            var thread = new Thread(x =>
            {
                foreach (var package in packages)
                {
                    dev.SendPackage(package);
                    Thread.Sleep(500);
                }
            });
            thread.Start();
        }

        private static IEnumerable<Package> CreateBlinkPackages()
        {
            var cmd1 = new SetColor(Colors.Red) {Channel = 1};
            var cmd2 = new SetColor(Colors.Green) {Channel = 1};
            var pkg1 = new Package(cmd1.GetBytes());
            var pkg2 = new Package(cmd2.GetBytes());
            var packages = new[] {pkg1, pkg2, pkg1, pkg2, pkg1, pkg2};
            return packages;
        }

        public event EventHandler CanExecuteChanged;
    }
}
