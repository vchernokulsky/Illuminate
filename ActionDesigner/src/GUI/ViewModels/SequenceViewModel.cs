using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Data;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class SequenceViewModel : BaseViewModel
    {
        [NonSerialized]
        private readonly ObservableCollection<FrameViewModel> _frameViewModels;

        //последовательность управляющих фреймов
        private readonly FrameSequence _sequence;

        public SequenceViewModel()
        {
            _frameViewModels = new ObservableCollection<FrameViewModel>();

            _sequence = new FrameSequence();
            _sequence.SequenceChanged += OnSequenceChanged;
        }

        private readonly FrameBuilder _frameBuilder;
        public SequenceViewModel(FrameBuilder builder) : this()
        {
            _frameBuilder = builder;
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
            var frameModel = new FrameViewModel(frame);
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

            foreach (var frameView in frames.Select(frame => new FrameViewModel(frame)))
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

        public void CopySelected()
        {
//            _buffer.Clear();
//            foreach (var model in FrameViewModels.Where(model => model.IsSelected))
//            {
//                var frameClone = (Frame)model.Frame.Clone();
//                _buffer.Add(frameClone);
//            }
        }

        public void ConvertFrame(FrameViewModel view, Frame frame)
        {
            int idx = _frameViewModels.IndexOf(view);
            if (idx >= 0)
            {
                _sequence.ChangeFrame(view.Frame, frame);
                _frameViewModels[idx] = new FrameViewModel(frame);
            }
        }

        //загрузка/сохранение последовательности фреймов
        public void SaveToFile(string fileName)
        {
            try
            {
                using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    _sequence.SequenceChanged -= OnSequenceChanged;
                    var bf = new BinaryFormatter();
                    bf.Serialize(stream, _sequence);
                    _sequence.SequenceChanged += OnSequenceChanged;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void LoadFromFile(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var bf = new BinaryFormatter();
                var obj = bf.Deserialize(stream);

                var sequence = obj as FrameSequence;
                if (sequence != null)
                {
                    _frameViewModels.Clear();
                    _sequence.Clear();

                    foreach (var frame in sequence.Frames) Add(frame);
                }
            }
        }

        //PRIVATE METHODS
        private void OnSequenceChanged(object sender, EventArgs eventArgs)
        {
            var view = CollectionViewSource.GetDefaultView(_frameViewModels);
            view.Refresh();
        }
    }
}
