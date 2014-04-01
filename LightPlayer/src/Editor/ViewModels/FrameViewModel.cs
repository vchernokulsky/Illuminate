using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Intems.LightDesigner.GUI.ViewModels.Commands;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightDesigner.GUI.ViewModels
{
    [Serializable]
    public class FrameViewModel : BaseViewModel
    {
        private Frame _frame;
        private bool _isSelected;

        public FrameViewModel()
        {
            _frame = null;
        }

        public FrameViewModel(Frame frame)
        {
            _frame = frame;
        }

        public FrameViewModel(Frame frame, ActionGroup actionGroup) : this(frame)
        {
            _actionGroup = actionGroup;
        }

        public event EventHandler ModelChanged;
        private void RaiseModelChanged()
        {
            var handler = ModelChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private readonly ActionGroup _actionGroup;
        public ActionGroup ActionGroup
        {
            get { return _actionGroup; }
        }

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

        public Visibility BlinkVisibility
        {
            get
            {
                var result = Visibility.Collapsed;
                if (_frame.Command is BlinkColor)
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

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaiseModelChanged();
            }
        }
    }
}
