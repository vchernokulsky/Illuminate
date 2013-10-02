using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using NAudio.Wave;

namespace Intems.LightPlayer.BL
{
    public class FrameProcessor
    {
        private const double TimeInterval = 100;
        private readonly Timer _timer;

        private readonly object _locker = new object();

        private readonly IWavePlayer _player;

        public FrameProcessor(IWavePlayer player)
        {
            _timer = new Timer(TimeInterval);
            _timer.Elapsed += OnTimerElapsed;

            _player = player;
            _devices = new Collection<Device>();
        }

        private readonly ICollection<Device> _devices;
        public FrameProcessor(ICollection<Device> devices, IWavePlayer player) : this(player)
        {
            _devices = devices;
        }

        public AudioFileReader AudioReader { get; set; }

        public void AddDevice(Device device)
        {
            _devices.Add(device);
        }

        public void Start()
        {
            _timer.Start();
            if(_player != null)
                _player.Play();
        }

        public void Stop()
        {
            _timer.Stop();
            if (_player != null)
                _player.Stop();
        }

        public void Start(FrameSequence sequence)
        {
            Start();
        }

        private readonly TimeSpan _prevTime = TimeSpan.FromSeconds(0.0);
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            lock (_locker)
            {
                var time = _prevTime;
                if (AudioReader != null)
                    time = AudioReader.CurrentTime;
                else
                    time += TimeSpan.FromMilliseconds(TimeInterval);

                //set current time for all devices
                foreach (var device in _devices) device.SetTime(time);
            }
        }
    }
}
