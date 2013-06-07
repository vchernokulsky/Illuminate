using System;
using System.Windows.Media;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameModel :BaseViewModel
    {
        public Color Color { get; set; }

        public string CmdType { get; set; }

        // Time
        public TimeSpan FrameBegin { get; set; }

        public TimeSpan FrameLength { get; set; }
    }
}
