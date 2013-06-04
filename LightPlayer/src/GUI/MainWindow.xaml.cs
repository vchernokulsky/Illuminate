using System.ComponentModel;
using System.Windows;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.Transport;
using Microsoft.Win32;
using NAudio.Wave;

namespace Intems.LightPlayer.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FrameSequence _sequence = new FrameSequence();
        private IWavePlayer   _player = new WaveOutEvent();
        private IPackageSender _sender = new SerialPortSender(null);

        private FrameProcessor _processor;


        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
            _processor = new FrameProcessor(_sender, _player, _sequence);
        }

        private void OnBtnStart_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewModel) DataContext;

            IWaveProvider provider = new AudioFileReader(vm.AudioFileName);
            _player.Init(provider);

            _processor.Start();
        }

        private void OnBtnChoose_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog().Value)
                ((MainViewModel)DataContext).AudioFileName = dlg.FileName;
        }

        private void OnBtnStop_Click(object sender, RoutedEventArgs e)
        {
            _player.Stop();
        }
    }
}
