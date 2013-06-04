using System;
using System.Windows.Media;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using Intems.LightPlayer.Transport;
using NAudio.Wave;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests
{
    [TestFixture]
    class FrameProcessorTest
    {
        private IPackageSender _sender;
        private FrameSequence  _sequence;

        private readonly MockRepository _repository = new MockRepository();

        [SetUp]
        public void Initialization()
        {
            _sequence = new FrameSequence();
            _sender = _repository.StrictMock<IPackageSender>();
        }

        private static Frame CreateFrame(double time1, double time2)
        {
            var span1 = TimeSpan.FromSeconds(time1);
            var span2 = TimeSpan.FromSeconds(time2);
            var frame = new Frame(span1, span2) { Command = new SetColor(1, Color.FromRgb(128, 128, 128)) };
            return frame;
        }

        [Test]
        public void ProcessCurrentFrame()
        {
            var frame = CreateFrame(0, 2.5);
            _sequence.Push(frame);

            var processor = new FrameProcessor(_sender, _sequence);
            using (_repository.Record())
            {
                var pkg  = new Package(frame.Command.GetBytes());
                _sender.Expect(x => x.SendPackage(pkg)).Repeat.Once();
            }
            using (_repository.Playback())
            {
                processor.SetTime(TimeSpan.FromSeconds(0.1));
            }
        }

        [Test]
        public void ProcessTwoFrames()
        {
            var frame1 = CreateFrame(0, 2.5);
            var frame2 = CreateFrame(2.5, 2);
            _sequence.Push(frame1);
            _sequence.Push(frame2);

            var process = new FrameProcessor(_sender, _sequence);
            using (_repository.Record())
            {
                var pkg1 = new Package(frame1.Command.GetBytes());
                var pkg2 = new Package(frame2.Command.GetBytes());
                _sender.Expect(x => x.SendPackage(pkg1)).Repeat.Times(2);
            }
            using (_repository.Playback())
            {
                process.SetTime(TimeSpan.FromSeconds(0.1));
                process.SetTime(TimeSpan.FromSeconds(2.6)); 
            }
        }

        [Test]
        public void ProcessOneFrameOnShortTimeInterval()
        {
            var frame = CreateFrame(0, 3);
            _sequence.Push(frame);

            var process = new FrameProcessor(_sender, _sequence);
            using (_repository.Record())
            {
                var pkg1 = new Package(frame.Command.GetBytes());
                _sender.Expect(x => x.SendPackage(pkg1)).Repeat.Once();
            }
            using (_repository.Playback())
            {
                process.SetTime(TimeSpan.FromSeconds(0.1));
                process.SetTime(TimeSpan.FromSeconds(0.5));
                process.SetTime(TimeSpan.FromSeconds(0.8));
            }
        }

        [Test]
        public void StartFeameProcessingTest()
        {
            var player = _repository.DynamicMock<IWavePlayer>();
            var process = new FrameProcessor(_sender, player, _sequence);

            using (_repository.Record())
            {
                player.Expect(x => x.Play()).Repeat.Once();
            }
            using (_repository.Playback())
            {
                process.Start();
            }
        }
    }
}
