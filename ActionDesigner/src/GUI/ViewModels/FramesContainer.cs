using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Data;
using Intems.LightPlayer.BL;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FramesContainer : BaseViewModel
    {
        [NonSerialized]
        private readonly ObservableCollection<FrameView> _frameViews;
        [NonSerialized]
        private readonly FrameBuilder _builder;

        private readonly List<Frame> _buffer;

        //последовательность управляющих фреймов
        private readonly FrameSequence _sequence;

        public FramesContainer()
        {
            _builder = new FrameBuilder();
            _buffer = new List<Frame>();
            _frameViews = new ObservableCollection<FrameView>();

            _sequence = new FrameSequence();
            _sequence.SequenceChanged += OnSequenceChanged;
        }

        public FrameView CurrentView { get; set; }

        public IList<FrameView> FrameViews
        {
            get { return _frameViews; }
        }

        public void Add(Frame frame)
        {
            _sequence.Push(frame);

            //добавляем модель для отображения
            var frameModel = new FrameView(frame);
            frameModel.ModelChanged += OnSequenceChanged;
            _frameViews.Add(frameModel);
        }

        public void PushBack(LightPlayer.BL.Commands.CmdEnum cmd)
        {
            var startTime = CalculateFrameStartTime();
            var frame = _builder.CreateFrameByCmdEnum(cmd, startTime);
            _sequence.Push(frame);

            //добавляем модель для отображения
            var frameModel = new FrameView(frame);
            frameModel.ModelChanged += OnSequenceChanged;
            _frameViews.Add(frameModel);
        }

        public void InsertAfter(FrameView currentView, IEnumerable<Frame> frames)
        {
            var idx = _sequence.Frames.IndexOf(currentView.Frame);
            if(idx >= 0)
                _sequence.Frames.InsertRange(idx + 1, frames);
        }

        //работа с фреймами и буфером фреймов
        public void ClearSelection()
        {
            foreach (var view in FrameViews)
                view.IsSelected = false;
        }

        public void SelectGroup(FrameView frameView)
        {
            var idx = FrameViews.TakeWhile(view => !view.IsSelected).Count();
        }

        public void CopySelected()
        {
            _buffer.Clear();
            foreach (var model in FrameViews.Where(model => model.IsSelected))
                _buffer.Add(model.Frame);
        }

        public void ConvertFrame(FrameView view, Frame frame)
        {
            int idx = _frameViews.IndexOf(view);
            if (idx >= 0)
            {
                _sequence.ChangeFrame(view.Frame, frame);
                _frameViews[idx] = new FrameView(frame);
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
                    _frameViews.Clear();
                    _sequence.Clear();

                    foreach (var frame in sequence.Frames) Add(frame);
                }
            }
        }

        //PRIVATE METHODS
        private void OnSequenceChanged(object sender, EventArgs eventArgs)
        {
            var view = CollectionViewSource.GetDefaultView(_frameViews);
            view.Refresh();
        }

        private TimeSpan CalculateFrameStartTime()
        {
            if (_frameViews.Count <= 0) return TimeSpan.FromSeconds(0);

            var lastFrame = _frameViews.Last();
            var startTime = lastFrame.FrameBegin + lastFrame.FrameLength;
            return startTime;
        }
    }
}
