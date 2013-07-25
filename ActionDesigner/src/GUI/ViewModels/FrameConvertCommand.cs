using System;
using System.Windows.Input;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameConvertCommand : ICommand
    {
        private readonly Action<string, FrameModel> _action;
        private readonly string _cmdType;

        public FrameConvertCommand(string cmdType, Action<string, FrameModel> action)
        {
            _cmdType = cmdType;
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
//            var canExecute = parameter is FrameModel;
            return true;
        }

        public void Execute(object parameter)
        {
            var model = parameter as FrameModel;
            _action(_cmdType, model);
        }

        public event EventHandler CanExecuteChanged;
    }
}