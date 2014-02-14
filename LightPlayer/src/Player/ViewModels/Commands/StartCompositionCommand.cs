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
            var model = parameter as MainViewModel;
            if (model != null)
            {
                if (!String.IsNullOrEmpty(model.TrackFilename))
                {
                    var provider = new AudioFileReader(model.TrackFilename);
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
