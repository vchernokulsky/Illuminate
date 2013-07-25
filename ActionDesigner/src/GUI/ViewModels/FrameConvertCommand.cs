using System;
using System.Windows.Input;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameConvertCommand : ICommand
    {
        private readonly Action<FrameModel> _action;

        public FrameConvertCommand(Action<FrameModel> action)
        {
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
            _action(model);
        }

        public event EventHandler CanExecuteChanged;
    }
}