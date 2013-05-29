using System;
using Intems.LightPlayer.BL;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class FrameProcessorTest
    {
        private FrameSequence _sequence;

        [SetUp]
        public void Initialization()
        {
            _sequence = new FrameSequence();
        }

        [Test]
        public void ProcessCurrentFrame()
        {
            var span1 = TimeSpan.FromSeconds(0);
            var span2 = TimeSpan.FromSeconds(2.5);
            var frame = new Frame(span1, span2);
            _sequence.Push(frame);


        }
    }
}
