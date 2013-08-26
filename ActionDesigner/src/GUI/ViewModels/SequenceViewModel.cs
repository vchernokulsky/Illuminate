﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using Intems.LightDesigner.GUI.ActionCommands;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class SequenceViewModel : BaseViewModel
    {
        [NonSerialized]
        private readonly ObservableCollection<FrameViewModel> _frameViewModels;
        [NonSerialized]
        private readonly ActionGroup _actionGroup;

        //последовательность управляющих фреймов
        private readonly FrameSequence _sequence;

        public SequenceViewModel()
        {
            _frameViewModels = new ObservableCollection<FrameViewModel>();
            _actionGroup = new ActionGroup(this);

            _sequence = new FrameSequence();
            _sequence.SequenceChanged += OnSequenceChanged;
        }

        private readonly FrameBuilder _frameBuilder;
        public SequenceViewModel(FrameBuilder builder) : this()
        {
            _frameBuilder = builder;
        }

        public SequenceViewModel(FrameSequence sequence, FrameBuilder builder) : this(builder)
        {
            foreach (var frame in sequence.Frames) 
                Add(frame);
        }

        public FrameSequence Sequence
        {
            get { return _sequence; }
        }

        public IList<FrameViewModel> FrameViewModels
        {
            get { return _frameViewModels; }
        }

        public void NewFrame(string buttonTag)
        {
            CmdEnum cmdEnum;
            CmdEnum.TryParse(buttonTag, out cmdEnum);

            if (_frameBuilder != null)
            {
                var frame = _frameBuilder.CreateFrame(cmdEnum, this);
                if(frame != null)
                    Add(frame);
            }
        }

        public void Add(Frame frame)
        {
            //добавляем вьюху
            var frameModel = CreateFrameViewModel(frame);
            frameModel.ModelChanged += OnSequenceChanged;
            _frameViewModels.Add(frameModel);
            //добавляем фрейм в последовательность
            _sequence.Push(frame);
            _sequence.UpdateAll();
        }

        public void InsertAfter(Frame currentFrame, IEnumerable<Frame> frames)
        {
            var idx = _sequence.Frames.IndexOf(currentFrame);
            if (idx >= 0 && frames.Any())
                _sequence.Frames.InsertRange(idx + 1, frames);

            foreach (var frameView in frames.Select(CreateFrameViewModel))
            {
                frameView.ModelChanged += OnSequenceChanged;
                _frameViewModels.Insert(++idx, frameView);
            }
            _sequence.UpdateFrom(currentFrame);
        }

        public void ClearSelection()
        {
            foreach (var viewModel in FrameViewModels)
                viewModel.IsSelected = false;
        }

        public IEnumerable<Frame> SelectGroup(FrameViewModel frameView)
        {
            var selectedFrames = new List<Frame>();

            var firstIdx = FrameViewModels.TakeWhile(view => !view.IsSelected).Count();
            var secondIdx = FrameViewModels.IndexOf(frameView);

            if (firstIdx < secondIdx)
            {
                for (int i = firstIdx; i <= secondIdx; i++)
                {
                    FrameViewModels[i].IsSelected = true;
                    selectedFrames.Add(FrameViewModels[i].Frame);
                }
            }
            else
            {
                var queue = new Queue<Frame>();
                for (int i = secondIdx; i <= firstIdx; i++)
                {
                    FrameViewModels[i].IsSelected = true;
                    queue.Enqueue(FrameViewModels[i].Frame);
                }
                selectedFrames = queue.ToList();
            }
            return selectedFrames;
        }

        // подписка/отписка на все события для сохранения
        public void Unsubscribe()
        {
            foreach (var viewModel in FrameViewModels)
                viewModel.ModelChanged -= OnSequenceChanged;
            _sequence.SequenceChanged -= OnSequenceChanged;
        }

        public void Subscribe()
        {
            foreach (var viewModel in FrameViewModels)
                viewModel.ModelChanged += OnSequenceChanged;
            _sequence.SequenceChanged += OnSequenceChanged;
        }

        //PRIVATE METHODS
        private FrameViewModel CreateFrameViewModel(Frame frame)
        {
            var viewModel = new FrameViewModel(frame, _actionGroup);
            return viewModel;
        }

        private void OnSequenceChanged(object sender, EventArgs eventArgs)
        {
            var view = CollectionViewSource.GetDefaultView(_frameViewModels);
            view.Refresh();
        }
    }
}
