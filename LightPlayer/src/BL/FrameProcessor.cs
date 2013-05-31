using System;

namespace Intems.LightPlayer.BL
{
    public class FrameProcessor
    {
        private FrameSequence _sequence;

        public FrameProcessor(FrameSequence sequence)
        {
            _sequence = sequence;
        }

        public void SetTime(TimeSpan time)
        {
        }
    }
}
