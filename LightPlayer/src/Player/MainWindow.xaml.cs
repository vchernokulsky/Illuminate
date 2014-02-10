using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.GUI.ViewModels;
using Microsoft.Win32;
using NAudio.Wave;
using Radario.Common;

namespace Intems.LightPlayer.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FrameSequenceCollection _sequenceCollection;
        private FrameProcessor _processor;
        private IEnumerable<Device> _devices;

        private readonly IWavePlayer _player = new WaveOutEvent();
        private const int Port = 15000;

        public MainWindow()
        {
            InitializeComponent();

            //_processor = new FrameProcessor(_player);
            DataContext = new MainViewModel();
        }

        private void OnDeviceFindButtonClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;

            if (viewModel != null)
            {
                var discoverer = new DeviceDiscoverer(Port, viewModel.DeviceCount);
                var devices = discoverer.Discover();
                _processor = new FrameProcessor(devices, _player);
                viewModel.UpdateDevices(devices);
            }
        }

        private void OnBtnAudioChooseClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog {Filter = "Аудио файлы(*.mp3, *.wav)|*.mp3;*.wav"};
            if (dlg.ShowDialog().Value)
            {
                var viewModel = (MainViewModel) DataContext;
                viewModel.AudioFileName = dlg.FileName;
            }
        }

        private void OnBtnFrameChooseClick(object sender, RoutedEventArgs e)
        {
            var deviceViewModel = (DeviceViewModel)((Button) sender).Tag;

            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog().Value)
            {
                deviceViewModel.CompositionFile = dlg.FileName;
                try
                {
                    using (Stream stream = new FileStream(deviceViewModel.CompositionFile, FileMode.Open))
                    {
                        var bf = new BinaryFormatter();
                        var collection = (FrameSequenceCollection)bf.Deserialize(stream);
                        deviceViewModel.SetSequenceCollection(collection);
                        _processor.AddDevice(deviceViewModel.Device);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке композиции", "Ошибка");
                    SimpleLog.Log.Error(ex.Message, ex);
                }
            }
        }

        private void OnButtonStartClick(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            if (!String.IsNullOrEmpty(vm.AudioFileName))
            {
                var provider = new AudioFileReader(vm.AudioFileName);
                _player.Init(provider);

                //start processing composition
                _processor.AudioReader = provider;
                _processor.Start(null);
            }
        }

        private void OnButtonStopClick(object sender, RoutedEventArgs e)
        {
            _processor.Stop();
        }

        private void OnBtnLoadConfigClick(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewModel) DataContext;
            vm.IsLoadDeviceConfig = Visibility;
        }
    }
}