using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Media;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using Intems.LightPlayer.Transport;
using Intems.LightPlayer.Transport.Stubs;
using Microsoft.Win32;
using NAudio.Wave;
using ServiceStack.Text;
using Frame = Intems.LightPlayer.BL.Frame;

namespace Intems.LightPlayer.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FrameSequence _sequence;
        private IWavePlayer   _player = new WaveOutEvent();
        private IPackageSender _sender = new FakePackageSender();

        private FrameProcessor _processor;


        private static void InitSequence(ref FrameSequence seq)
        {
            for (int i = 0; i < 100; i++)
            {
                var frame = new Frame(TimeSpan.FromSeconds(i * 0.5), TimeSpan.FromSeconds(0.5))
                {
                    Command = new SetColor(Color.FromRgb(128, 128, 128)){Channel = 1}
                };
                seq.Push(frame);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            //InitSequence(ref _sequence);

            DataContext = new MainViewModel();
            _processor = new FrameProcessor(_sender, _player, _sequence);
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
                    //var sr = new StreamReader(vm.FramesFileName);
                    //_sequence = JsonSerializer.DeserializeFromReader<FrameSequence>(sr);
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
