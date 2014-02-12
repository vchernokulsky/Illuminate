using System;
using System.Windows.Input;
using Intems.LightPlayer.BL;
using NAudio.Wave;

namespace Intems.LightPlayer.GUI.ViewModels.Commands
{
    internal class StartCompositionCommand : ICommand
    {
        private readonly IWavePlayer _player;
        private readonly FrameProcessor _processor;

        public StartCompositionCommand(IWavePlayer player, FrameProcessor processor)
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
            var obj = parameter as DeviceConfigurationModel;
            if (obj != null)
            {
                if (!String.IsNullOrEmpty(obj.FileName))
                {
                    var provider = new AudioFileReader(obj.FileName);
                    _player.Init(provider);

                    //start processing composition
                    _processor.AudioReader = provider;
                    _processor.Start(null);
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
