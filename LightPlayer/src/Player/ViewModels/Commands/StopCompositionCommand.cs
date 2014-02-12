using System;
using System.Windows.Input;
using Intems.LightPlayer.BL;
using NAudio.Wave;

namespace Intems.LightPlayer.GUI.ViewModels.Commands
{
    internal class StopCompositionCommand : ICommand
    {
        private IWavePlayer _player;
        private readonly FrameProcessor _processor;

        public StopCompositionCommand(IWavePlayer player, FrameProcessor processor)
        {
            _player = player;
            _processor = processor;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_processor != null)
                _processor.Stop();
        }

        public event EventHandler CanExecuteChanged;
    }
}
