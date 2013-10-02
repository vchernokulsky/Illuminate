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

namespace Intems.LightPlayer.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FrameSequence _sequence;
        private readonly FrameProcessor _processor;
        private IEnumerable<Device> _devices;

        private readonly IWavePlayer _player = new WaveOutEvent();
        private const int Port = 15000;

        public MainWindow()
        {
            InitializeComponent();

            _processor = new FrameProcessor(_player);
            DataContext = new MainViewModel();
        }

        private void OnDeviceFindButtonClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;

            if (viewModel != null)
            {
                var discoverer = new DeviceDiscoverer(Port, viewModel.DeviceCount);
                var devices = discoverer.Discover();

                viewModel.UpdateDevices(devices);
            }
        }

        private void OnBtnAudioChooseClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog {Filter = "Аудио файлы(*.mp3, *.wav)|*.mp3;*.wav"};
            if (dlg.ShowDialog().Value)
                ((MainViewModel)DataContext).AudioFileName = dlg.FileName;
        }

        private void OnBtnFrameChooseClick(object sender, RoutedEventArgs e)
        {
            var dev = (DeviceViewModel)((Button) sender).Tag;

            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog().Value)
            {
                dev.CompositionFile = dlg.FileName;
                try
                {
                    using (Stream stream = new FileStream(dev.CompositionFile, FileMode.Open))
                    {
                        var bf = new BinaryFormatter();
                        _sequence = (FrameSequence)bf.Deserialize(stream);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
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
                _processor.Start(_sequence);
            }
        }

        private void OnButtonStopClick(object sender, RoutedEventArgs e)
        {
            _processor.Stop();
        }
    }
}