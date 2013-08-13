using System;
using System.Windows.Input;
using Intems.LightDesigner.GUI.ViewModels;

namespace Intems.LightDesigner.GUI.ActionCommands
{
    public class FrameConvertCommand : ICommand
    {
        private readonly Action<FrameView> _action;

        public FrameConvertCommand(Action<FrameView> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var model = parameter as FrameView;
            _action(model);
        }

        public event EventHandler CanExecuteChanged;
    }
}