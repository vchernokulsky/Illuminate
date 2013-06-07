using System;
using System.Windows.Media;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameModel :BaseViewModel
    {
        private Command _command;

        public FrameModel(Command command)
        {
            _command = command;
        }

        public Color Color { get; set; }

        public string CmdType { get; set; }

        // Time
        public TimeSpan FrameBegin { get; set; }

        public TimeSpan FrameLength { get; set; }
    }
}
