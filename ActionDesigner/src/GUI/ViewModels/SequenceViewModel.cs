using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Data;
using Intems.LightPlayer.BL;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class SequenceViewModel : BaseViewModel, IEnumerable<FrameViewModel>
    {
        [NonSerialized]
        private readonly ObservableCollection<FrameViewModel> _frameViewModels;

        private readonly List<Frame> _buffer;

        //последовательность управляющих фреймов
        private readonly FrameSequence _sequence;

        public SequenceViewModel()
        {
            _buffer = new List<Frame>();
            _frameViewModels = new ObservableCollection<FrameViewModel>();

            _sequence = new FrameSequence();
            _sequence.SequenceChanged += OnSequenceChanged;
        }

        public IEnumerator<FrameViewModel> GetEnumerator()
        {
            return _frameViewModels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _frameViewModels.GetEnumerator();
        }

        public FrameViewModel CurrentView { get; set; }

        public IList<FrameViewModel> FrameViewModels
        {
            get { return _frameViewModels; }
        }

        public void Add(Frame frame)
        {
            //добавляем вьюху
            var frameModel = new FrameViewModel(frame);
            frameModel.ModelChanged += OnSequenceChanged;
            _frameViewModels.Add(frameModel);
            //добавляем фрейм в последовательность
            _sequence.Push(frame);
            _sequence.UpdateFrom(frame);
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

        //работа с фреймами и буфером фреймов
        public void ClearSelection()
        {
            foreach (var view in FrameViewModels)
                view.IsSelected = false;
        }

        public void SelectGroup(FrameViewModel frameView)
        {
            var firstIdx = FrameViewModels.TakeWhile(view => !view.IsSelected).Count();
            var secondIdx = FrameViewModels.IndexOf(frameView);

            if (firstIdx < secondIdx)
            {
                for (int i = firstIdx; i <= secondIdx; i++)
                    FrameViewModels[i].IsSelected = true;
            }
            else
            {
                for (int i = secondIdx; i < firstIdx; i++)
                    FrameViewModels[i].IsSelected = true;
            }
        }

        public void CopySelected()
        {
            _buffer.Clear();
            foreach (var model in FrameViewModels.Where(model => model.IsSelected))
            {
                var frameClone = (Frame)model.Frame.Clone();
                _buffer.Add(frameClone);
            }
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

//        private TimeSpan CalculateFrameStartTime()
//        {
//            if (_frameViewModels.Count <= 0) return TimeSpan.FromSeconds(0);
//
//            var lastFrame = _frameViewModels.Last();
//            var startTime = lastFrame.FrameBegin + lastFrame.FrameLength;
//            return startTime;
//        }
    }
}
