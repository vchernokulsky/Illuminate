using System;

namespace Intems.LightPlayer.BL
{
    public class FrameProcessor
    {
        private IPackageSender _sender;
        private FrameSequence _sequence;

        public FrameProcessor(IPackageSender sender, FrameSequence sequence)
        {
            _sender = sender;
            _sequence = sequence;
        }

        public void SetTime(TimeSpan time)
        {
            var frame = _sequence.FrameByTime(time);
            if (frame != null)
            {
                var pkg = new Package(frame.Command.GetBytes());
                _sender.SendPackage(pkg);
            }
        }
    }
}
