using System.Collections.Generic;
using Intems.LightPlayer.BL;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class SequenceContainerViewModel
    {
        private readonly SequenceCollection _sequenceCollection;

        private readonly FrameBuilder _frameBuilder;
        private readonly Dictionary<int, SequenceViewModel> _sequenceDict;


        public SequenceContainerViewModel() : this(1)
        {
        }

        public SequenceContainerViewModel(int channelCount)
        {
            _sequenceCollection = new SequenceCollection();

            _frameBuilder = new FrameBuilder();
            _sequenceDict = new Dictionary<int, SequenceViewModel>();

            for (int i = 0; i < channelCount; i++)
            {
                var sequenceViewModel = new SequenceViewModel(_frameBuilder);
                _frameBuilder.RegisterSequence(i, sequenceViewModel);
                _sequenceDict.Add(i, sequenceViewModel);
                //формируем коллекцию последовательностей фреймов
                _sequenceCollection.Sequences.Add(sequenceViewModel.Sequence);
            }
        }

        public IEnumerable<int> Channels
        {
            get { return _sequenceDict.Keys; }
        }

        public IEnumerable<SequenceViewModel> Sequences
        {
            get { return _sequenceDict.Values; }
        }
    }
}
