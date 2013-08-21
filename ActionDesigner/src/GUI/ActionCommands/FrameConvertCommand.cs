﻿using System;
using System.Windows.Input;
using Intems.LightDesigner.GUI.ViewModels;

namespace Intems.LightDesigner.GUI.ActionCommands
{
    public class FrameConvertCommand : ICommand
    {
        private readonly Action<FrameViewModel> _action;

        public FrameConvertCommand(Action<FrameViewModel> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var model = parameter as FrameViewModel;
            _action(model);
        }

        public event EventHandler CanExecuteChanged;
    }
}