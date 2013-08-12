using System;
using System.Timers;
using Intems.LightPlayer.Transport;
using NAudio.Wave;

namespace Intems.LightPlayer.BL
{
    public class FrameProcessor
    {
        private readonly object _locker = new object();

        private readonly Timer _timer;

        private const double TimeInterval = 100;

        private readonly IWavePlayer _player;
        private readonly IPackageSender _sender;
        private FrameSequence _sequence;

        public FrameProcessor(IPackageSender sender, FrameSequence sequence)
        {
            _timer = new Timer(TimeInterval);
            _timer.Elapsed += OnTimerElapsed;

            _sender = sender;
            _sequence = sequence;
        }

        public FrameProcessor(IPackageSender sender, IWavePlayer player, FrameSequence sequence) : this(sender, sequence)
        {
            _player = player;
        }

        private TimeSpan prevTime = TimeSpan.FromSeconds(0);
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            lock (_locker)
            {
                var time = prevTime;

                if (AudioReader != null)
                    time = AudioReader.CurrentTime;
                else
                    time += TimeSpan.FromMilliseconds(TimeInterval);

                SetTime(time);
            }
        }


        public AudioFileReader AudioReader { get; set; }

        public void SetTime(TimeSpan time)
        {
            var frame = _sequence.FrameByTime(time);
            if (frame != null)
            {
                Console.WriteLine("{0}", (time - prevTime).TotalSeconds);
                //---
                var pkg = new Package(frame.Command.GetBytes());
                _sender.SendPackage(pkg);
            }
        }

        public void Start()
        {
            _timer.Start();
            if(_player != null)
                _player.Play();
        }

        public void Start(FrameSequence sequence)
        {
            _sequence = sequence;
            Start();
        }

        public void Stop()
        {
            _timer.Stop();
            if(_player != null)
                _player.Stop();
        }
    }
}
