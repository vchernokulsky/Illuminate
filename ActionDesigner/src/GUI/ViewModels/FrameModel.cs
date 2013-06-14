using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using CmdEnum = Intems.LightPlayer.BL.CmdEnum;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameModel : BaseViewModel
    {
        private readonly Frame _frame;

        public FrameModel(Frame frame)
        {
            _frame = frame;
            _frame.FrameChanged += OnFrameChanged;
        }

        public Color FillBrush1
        {
            get
            {
                byte[] bytes = _frame.Command.GetBytes();
                var color = Color.FromRgb(bytes[4], bytes[6], bytes[8]);
                return color;
            }
            set
            {
                if ((_frame.Command is SetColor) || (_frame.Command is BlinkColor))
                {
                    var type = _frame.Command.GetType();
                    type.InvokeMember("Color", BindingFlags.SetProperty, null, _frame.Command, new object[] {value});
                }
                if (_frame.Command is FadeColor)
                {
                    var type = _frame.Command.GetType();
                    type.InvokeMember("StartColor", BindingFlags.SetProperty, null, _frame.Command, new object[] { value });
                }
            }
        }

        public Color FillBrush2
        {
            get
            {
                Color? color = null;
                if (_frame.Command is FadeColor)
                {
                    byte[] bytes = _frame.Command.GetBytes();
                    color = Color.FromRgb(bytes[10], bytes[12], bytes[14]);
                }
                return color.HasValue ? color.Value : Colors.White;
            }
            set
            {
                if (_frame.Command is FadeColor)
                {
                    var type = _frame.Command.GetType();
                    type.InvokeMember("StopColor", BindingFlags.SetProperty, null, _frame.Command, new object[] { value });
                }
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

        public Visibility FadeVisibility
        {
            get
            {
                var result = Visibility.Collapsed;
                if (_frame.Command is FadeColor)
                    result = Visibility.Visible;
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

        private void OnFrameChanged(object sender, FrameEventArgs e)
        {
//            RaisePropertyChanged("FrameBegin");
//            RaisePropertyChanged("FrameLength");
        }
    }
}
