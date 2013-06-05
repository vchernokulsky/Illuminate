using System;
using System.Diagnostics;
using System.Timers;
using Intems.LightPlayer.Transport;
using NAudio.Wave;

namespace Intems.LightPlayer.BL
{
    public class FrameProcessor
    {
        private readonly object _locker = new object();

        private readonly double TimeInterval = 50;
        private readonly Timer  _timer;

        private IWavePlayer _player;
        private readonly IPackageSender _sender;
        private readonly FrameSequence _sequence;

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
                var time = ((AudioFileReader) AudioReader).CurrentTime;
                SetTime(time);
            }
        }


        public AudioFileReader AudioReader { get; set; }

        public void SetTime(TimeSpan time)
        {
            var frame = _sequence.FrameByTime(time);
            if (frame != null)
            {
                Console.WriteLine("Track time: {0}s", time.TotalSeconds);
                Console.WriteLine("Diff: {0}s", (time - prevTime).TotalSeconds);
                prevTime = time;
                //---

                var pkg = new Package(frame.Command.GetBytes());
                _sender.SendPackage(pkg);
            }
        }

        public void Start()
        {
            _timer.Start();
            _player.Play();
        }

        public void Stop()
        {
            _timer.Stop();
            _player.Stop();
        }
    }
}
