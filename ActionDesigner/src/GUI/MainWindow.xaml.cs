using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Intems.LightDesigner.GUI.ViewModels;
using Intems.LightPlayer.BL.Commands;
using Microsoft.Win32;

namespace Intems.LightDesigner.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string TargetFileFilter = "Light composition file|*.cmps";
        private readonly SequenceViewModel _viewModel = new SequenceViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _viewModel;
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            var cmdEnum = (CmdEnum) Int32.Parse(btn.Tag.ToString());
           _viewModel.PushBack(cmdEnum);
        }

        private void OnSaveBtnClick(object sender, RoutedEventArgs e)
        {
            var saveDlg = new SaveFileDialog {Filter = TargetFileFilter, DefaultExt = "*.cmps", AddExtension = true};
            if (saveDlg.ShowDialog().Value)
                _viewModel.SaveToFile(saveDlg.FileName);
        }

        private void OnLoadBtnClick(object sender, RoutedEventArgs e)
        {
            var openDlg = new OpenFileDialog(){Filter = TargetFileFilter};
            if (openDlg.ShowDialog().Value)
                _viewModel.LoadFromFile(openDlg.FileName);
        }

        private void OnCellClick(object sender, MouseButtonEventArgs e)
        {
            var curCell = sender as Grid;
            if (curCell == null) return;

            var frameView = curCell.Tag as FrameViewModel;
            if (frameView == null) return;

            _viewModel.CurrentView = frameView;
            switch (Keyboard.Modifiers)
            {
                case ModifierKeys.Shift:
                    _viewModel.SelectGroup(frameView);
                    break;

                case ModifierKeys.Control:
                    frameView.IsSelected ^= true;
                    break;

                case ModifierKeys.None:
                    _viewModel.ClearSelection();
                    frameView.IsSelected ^= true;
                    break;
            }
        }

        private void OnCopyButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.CopySelected();
        }

        private void OnPasteButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.InsertAfter(_viewModel.CurrentView, null);
        }
    }
}
