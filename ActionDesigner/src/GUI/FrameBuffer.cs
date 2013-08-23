using System.Collections.Generic;
using Intems.LightPlayer.BL;

namespace Intems.LightDesigner.GUI
{
    public class FrameBuffer
    {
        private readonly List<Frame> _frames = new List<Frame>();

        public void Copy(IEnumerable<Frame> frames)
        {
            _frames.Clear();
            _frames.AddRange(frames);
        }

        public IEnumerable<Frame> GetFrames()
        {
            return _frames;
        }
    }
}
