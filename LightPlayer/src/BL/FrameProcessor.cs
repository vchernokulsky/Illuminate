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

        private IWavePlayer _player;
        private readonly IPackageSender _sender;
        private readonly FrameSequence _sequence;

        public FrameProcessor(IPackageSender sender, FrameSequence sequence)
        {
            _timer = new Timer();
            _timer.Elapsed += OnTimerElapsed;

            _sender = sender;
            _sequence = sequence;
        }

        public FrameProcessor(IPackageSender sender, IWavePlayer player, FrameSequence sequence) : this(sender, sequence)
        {
            _player = player;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            
        }

        public void SetTime(TimeSpan time)
        {
            lock (_locker)
            {
                var frame = _sequence.FrameByTime(time);
                if (frame == null) return;

                var pkg = new Package(frame.Command.GetBytes());
                _sender.SendPackage(pkg);
            }
        }

        public void Start()
        {
            _player.Play();
            _timer.Start();
        }
    }
}
