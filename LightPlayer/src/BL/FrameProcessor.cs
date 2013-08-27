using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Intems.LightPlayer.Transport;
using NAudio.Wave;

namespace Intems.LightPlayer.BL
{
    public class FrameProcessor
    {
        private const double TimeInterval = 100;
        private readonly Timer _timer;

        private readonly object _locker = new object();

        private readonly IWavePlayer _player;
        private readonly SequenceCollection _sequenceCollection;
        private IEnumerable<Device> _devices;


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

        private readonly IEnumerable<IPackageSender> _packageSenders;
        public FrameProcessor(IEnumerable<IPackageSender> senders, IWavePlayer player, SequenceCollection sequenceCollection)
        {
            _timer = new Timer(TimeInterval);
            _timer.Elapsed += OnTimerElapsed;

            _packageSenders = senders;
            _sequenceCollection = sequenceCollection;
            _player = player;
        }

        public AudioFileReader AudioReader { get; set; }

        public void SetTime(TimeSpan time)
        {
            if (_packageSenders != null && _packageSenders.Any())
            {
                var frames = _sequenceCollection.FramesByTime(time);
                foreach (var sender in _packageSenders)
                {
                    var packages = frames.Select(frm => frm.Command.GetBytes()).Select(bytes => new Package(bytes)).ToList();
                    sender.SendPackages(packages);
                }
            }
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
            _sequence = sequence;
            Start();
        }

        private readonly TimeSpan _prevTime = TimeSpan.FromSeconds(0);
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            lock (_locker)
            {
                var time = _prevTime;

                if (AudioReader != null)
                    time = AudioReader.CurrentTime;
                else
                    time += TimeSpan.FromMilliseconds(TimeInterval);

                SetTime(time);
            }
        }
    }
}
