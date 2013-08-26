using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Intems.LightDesigner.GUI.ViewModels;
using Microsoft.Win32;

namespace Intems.LightDesigner.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string TargetFileFilter = "Light composition file|*.cmps";
        //private readonly SequenceViewModel _viewModel = new SequenceViewModel();

        private SequenceContainerViewModel _containerViewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnNewProjectClick(object sender, RoutedEventArgs e)
        {
            var sChnCount = _txtChannelCount.Text;

            if (!String.IsNullOrEmpty(sChnCount))
            {
                int channelCount;
                if (Int32.TryParse(sChnCount, out channelCount))
                {
                    _containerViewModel = new SequenceContainerViewModel(channelCount);
                    DataContext = _containerViewModel;
                }
            }
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            var saveDlg = new SaveFileDialog {AddExtension = true, DefaultExt = "*.cmps", Filter = "Composition files | *.cmps"};
            if (saveDlg.ShowDialog().Value)
                _containerViewModel.SaveToFile(saveDlg.FileName);
        }

        private void OnLoadButtonClick(object sender, RoutedEventArgs e)
        {
            var openDlg = new OpenFileDialog {Filter = "Composition files | *.cmps"};
            if (openDlg.ShowDialog().Value)
            {
                if(_containerViewModel == null)
                    _containerViewModel = new SequenceContainerViewModel();

                _containerViewModel.LoadFromFile(openDlg.FileName);
                DataContext = _containerViewModel;
            }
        }
    }
}
