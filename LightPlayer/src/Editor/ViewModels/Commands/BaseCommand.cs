using System;
using System.Windows.Input;

namespace Intems.LightDesigner.GUI.ViewModels.Commands
{
    public class BaseCommand<T> : ICommand where T : class
    {
        private readonly Action<T> _action;

        public BaseCommand(Action<T> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var model = parameter as T;
            _action(model);
        }

        public event EventHandler CanExecuteChanged;
    }
}