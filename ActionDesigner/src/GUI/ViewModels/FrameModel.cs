using System;
using System.Windows.Media;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using CmdEnum = Intems.LightPlayer.BL.CmdEnum;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameModel :BaseViewModel
    {
        private readonly Frame _frame;

        public FrameModel(Frame frame)
        {
            _frame = frame;
        }

        public Brush FillBrush1
        {
            get
            {
                var color = Color.FromRgb(128, 128, 128);
                if ((_frame.Command is SetColor)||(_frame.Command is BlinkColor))
                {
                    byte[] bytes = _frame.Command.GetBytes();
                    color = Color.FromRgb(bytes[4], bytes[6], bytes[8]);
                }

                return new SolidColorBrush(color);
            }
        }

        public Brush FillBrush2
        {
            get
            {
                Color? color = null;
                if (_frame.Command is FadeColor)
                {
                    byte[] bytes = _frame.Command.GetBytes();
                    color = Color.FromRgb(bytes[10], bytes[12], bytes[14]);
                }
                var result = color.HasValue ? new SolidColorBrush(color.Value) : null;
                return result;
            }
        }

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
