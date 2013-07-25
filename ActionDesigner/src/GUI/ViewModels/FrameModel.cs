using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameModel : BaseViewModel
    {
        private Frame _frame;

        public FrameModel(Frame frame)
        {
            _frame = frame;
        }

        public event EventHandler ModelChanged;
        private void RaiseModelChanged()
        {
            var handler = ModelChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        #region COMMAND PROPERTIES

        public FrameConvertCommand ToSetColor
        {
            get
            {
                var action = new Action<FrameModel>(
                    fm =>
                    {
                        var cmd = new SetColor(1, fm.FillBrush1);
                        fm.Frame.Command = cmd;
                        RaiseModelChanged();
                    });
                var convertCommand = new FrameConvertCommand(action);
                return convertCommand;
            }
        }

        public FrameConvertCommand ToFadeColor
        {
            get
            {
                var action = new Action<FrameModel>(
                    fm =>
                    {
                        var length = (short) fm.FrameLength.TotalSeconds;
                        var cmd = new FadeColor(1, fm.FillBrush1, fm.FillBrush2, length);
                        fm.Frame.Command = cmd;
                        RaiseModelChanged();
                    });
                var convertCommand = new FrameConvertCommand(action);
                return convertCommand;
            }
        }

        public FrameConvertCommand ToBlinkColor
        {
            get
            {
//                var action = new Action<string, FrameModel>((s, fm) => { });
//                var cmd = new FrameConvertCommand(action);
//                return cmd;
                return null;
            }
        }

        #endregion

        //поля для связи с логикой
        public Frame Frame
        {
            get { return _frame; }
            set { _frame = value; }
        }

        //поля для binding'а на формах
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
