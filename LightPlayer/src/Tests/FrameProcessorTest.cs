using System;
using System.Windows.Media;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests
{
    [TestFixture]
    class FrameProcessorTest
    {
        private IPackageSender _sender;
        private FrameSequence  _sequence;

        private MockRepository _repository = new MockRepository();

        [SetUp]
        public void Initialization()
        {
            _sequence = new FrameSequence();
            _sender = _repository.StrictMock<IPackageSender>();
        }

        [Test]
        public void ProcessCurrentFrame()
        {
            var span1 = TimeSpan.FromSeconds(0);
            var span2 = TimeSpan.FromSeconds(2.5);
            var frame = new Frame(span1, span2) {Command = new SetColor(1, Color.FromRgb(128, 128, 128))};
            _sequence.Push(frame);

            var processor = new FrameProcessor(_sender, _sequence);
            using (_repository.Record())
            {
                var pkg  = new Package(frame.Command.GetBytes());
                _sender.Expect(x => x.SendPackage(pkg)).Repeat.Once();
            }
            using (_repository.Playback())
            {
                processor.SetTime(TimeSpan.FromSeconds(1.5));
            }
        }
    }
}
