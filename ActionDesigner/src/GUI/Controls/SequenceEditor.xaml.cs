using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Intems.LightDesigner.GUI.ViewModels;
using Intems.LightPlayer.BL.Commands;
using Frame = Intems.LightPlayer.BL.Frame;

namespace Intems.LightDesigner.GUI.Controls
{
    /// <summary>
    /// Interaction logic for SequenceEditor.xaml
    /// </summary>
    public partial class SequenceEditor : UserControl
    {
        public SequenceEditor()
        {
            InitializeComponent();
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as SequenceViewModel;
            var btn = sender as Button;

            if(btn != null && viewModel != null)
                viewModel.NewFrame(btn.Tag.ToString());
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var curCell = sender as Grid;
            if (curCell == null) return;

            var frameView = curCell.Tag as FrameViewModel;
            if (frameView == null) return;

            //_viewModel.CurrentView = frameView;
            switch (Keyboard.Modifiers)
            {
                case ModifierKeys.Shift:
                    //_viewModel.SelectGroup(frameView);
                    break;

                case ModifierKeys.None:
                    frameView.IsSelected ^= true;
                    break;
            }

        }
    }
}
