using System;
using System.Collections.Generic;
using System.Linq;

namespace Intems.LightPlayer.BL
{
    [Serializable]
    public class SequenceCollection
    {
        private readonly IList<FrameSequence> _sequences;

        public SequenceCollection()
        {
            _sequences = new List<FrameSequence>();
        }

        public SequenceCollection(IList<FrameSequence> sequences)
        {
            _sequences = sequences;
        }

        public ICollection<FrameSequence> Sequences
        {
            get { return _sequences; }
        }

        public FrameSequence GetSequenceByChannelId(int id)
        {
            FrameSequence result = null;
            if (id < _sequences.Count)
                result = _sequences[id];
            return result;
        }

        public Frame[] FramesByTime(TimeSpan time)
        {
            return _sequences.Select(sequence => sequence.FrameByTime(time)).Where(frame => frame != null).ToArray();
        }

        public void RewindAll()
        {
            foreach (var sequence in _sequences)
                sequence.Rewind();
        }

        public void Clear()
        {
            _sequences.Clear();
        }
    }
}
