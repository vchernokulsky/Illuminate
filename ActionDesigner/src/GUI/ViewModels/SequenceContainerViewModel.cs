﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Intems.LightDesigner.GUI.Common;
using Intems.LightPlayer.BL;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class SequenceContainerViewModel
    {
        private SequenceCollection _sequenceCollection;

        private readonly FrameBuilder _frameBuilder;
        private readonly Dictionary<int, SequenceViewModel> _sequenceDictionary;


        public SequenceContainerViewModel() 
        {
            _frameBuilder = new FrameBuilder();
            _sequenceCollection = new SequenceCollection();
            _sequenceDictionary = new Dictionary<int, SequenceViewModel>();
        }

        public SequenceContainerViewModel(int channelCount) : this()
        {
            for (int i = 0; i < channelCount; i++)
            {
                var sequenceViewModel = new SequenceViewModel(_frameBuilder);
                _frameBuilder.RegisterSequence(i, sequenceViewModel);
                _sequenceDictionary.Add(i, sequenceViewModel);
                //формируем коллекцию последовательностей фреймов
                _sequenceCollection.Sequences.Add(sequenceViewModel.Sequence);
            }
        }

        public IEnumerable<int> Channels
        {
            get { return _sequenceDictionary.Keys; }
        }

        public IEnumerable<SequenceViewModel> Sequences
        {
            get { return _sequenceDictionary.Values; }
        }

        public void SaveToFile(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                foreach (var viewModel in _sequenceDictionary.Values)
                    viewModel.Unsubscribe();

                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, _sequenceCollection);
                fs.Flush();

                foreach (var viewModel in _sequenceDictionary.Values)
                    viewModel.Subscribe();
            }
        }

        public void LoadFromFile(string fileName)
        {
            //очищаем коллекции
            _frameBuilder.Clear();
            _sequenceDictionary.Clear();
            _sequenceCollection.Clear();

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var formatter = new BinaryFormatter();
                _sequenceCollection = (SequenceCollection) formatter.Deserialize(fs);
                fs.Flush();

                int i = 0;
                foreach (var sequence in _sequenceCollection.Sequences)
                {
                    var sequenceViewModel = new SequenceViewModel(sequence, _frameBuilder);
                    _frameBuilder.RegisterSequence(i, sequenceViewModel);
                    _sequenceDictionary.Add(i, sequenceViewModel);
                    i++;
                }
            }
        }
    }
}
