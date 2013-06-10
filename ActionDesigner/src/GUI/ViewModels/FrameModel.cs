using System;
using System.Windows.Media;
using Intems.LightPlayer.BL;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameModel :BaseViewModel
    {
        private readonly Frame _frame;

        public FrameModel(Frame frame)
        {
            _frame = frame;
        }

        public Color Color { get; set; }

        public string CmdType
        {
            get
            {
                var result = String.Empty;
                switch (_frame.Command.Function)
                {
                    case (byte)CmdEnum.SetColor:
                        result = "SET COLOR";
                        break;
                    case (byte)CmdEnum.Fade:
                        result = "COLOR FADE";
                        break;
                    case (byte)CmdEnum.Blink:
                        result = "COLOR BLINK";
                        break;
                }
                return result;
            }
        }

        // Time
        public TimeSpan FrameBegin
        {
            get { return _frame.StartTime; }
        }

        public TimeSpan FrameLength
        {
            get { return _frame.Length; }
            set { _frame.Length = value; }
        }
    }
}
