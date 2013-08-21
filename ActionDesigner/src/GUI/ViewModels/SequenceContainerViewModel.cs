using System.Collections.Generic;
using Intems.LightPlayer.BL;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class SequenceContainerViewModel
    {
        private readonly SequenceCollection _sequenceCollection;

        private int _sequenceCount;
        private Dictionary<int, SequenceViewModel> _dictSequence;

        public SequenceContainerViewModel()
        {
        }

        public void CreateNewSequence()
        {
            var newSequence = new FrameSequence();
            _sequenceCollection.Sequences.Add(newSequence);

            var sequenceViewModel = new SequenceViewModel();
            _dictSequence.Add(_sequenceCount++, sequenceViewModel);
        }
    }
}
