using System.Collections.Generic;
using Intems.LightPlayer.BL;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class SequenceContainerViewModel
    {
        private readonly SequenceCollection _sequenceCollection;

        private int _sequenceCount;
        private readonly Dictionary<int, SequenceViewModel> _sequenceDict;

        public SequenceContainerViewModel()
        {
            _sequenceDict = new Dictionary<int, SequenceViewModel>
            {
                {1, new SequenceViewModel()},
                {2, new SequenceViewModel()}
            };
        }

        public IEnumerable<int> Channels
        {
            get { return _sequenceDict.Keys; }
        }

        public IEnumerable<SequenceViewModel> Sequences
        {
            get { return _sequenceDict.Values; }
        }

        public void CreateNewSequence()
        {
            var newSequence = new FrameSequence();
            _sequenceCollection.Sequences.Add(newSequence);

            var sequenceViewModel = new SequenceViewModel();
            _sequenceDict.Add(_sequenceCount, sequenceViewModel);
            _sequenceCount++;
        }
    }
}
