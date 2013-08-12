using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.Transport;
using Intems.LightPlayer.Transport.Stubs;
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
        private IPackageSender _sender;

        private readonly IWavePlayer _player = new WaveOutEvent();

        private readonly FrameProcessor _processor;

        public MainWindow()
        {
            InitializeComponent();
            InitializePackageSender();
            DataContext = new MainViewModel();
            _processor = new FrameProcessor(_sender, _player, _sequence);
        }

        private void InitializePackageSender()
        {
            if (SerialPort.GetPortNames().Contains("COM5"))
            {
                var port = new SerialPort("COM5", 115200, Parity.None, 8, StopBits.One);
                port.Open();
                _sender = new SerialportSender(port);
            }
            else
            {
                _sender = new FakePackageSender();
                Console.WriteLine("Sender stub was create because can't find target COM port");
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
            var vm = (MainViewModel)DataContext;

            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog().Value)
            {
                vm.FramesFileName = dlg.FileName;
                try
                {
                    using (Stream stream = new FileStream(vm.FramesFileName, FileMode.Open))
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

        private void OnBtnStart_Click(object sender, RoutedEventArgs e)
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

        private void OnBtnStop_Click(object sender, RoutedEventArgs e)
        {
            _processor.Stop();
        }
    }
}
