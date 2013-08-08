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
    public class FrameListModel : BaseViewModel
    {
        private FrameSequence _frameSequence;

        [NonSerialized]
        private ObservableCollection<FrameModel> _frameModels;
        [NonSerialized]
        private readonly FrameBuilder _builder;

        public FrameListModel()
        {
            _builder = new FrameBuilder();
            _frameSequence = new FrameSequence();
            _frameModels = new ObservableCollection<FrameModel>();
            _frameSequence.SequenceChanged += OnFrameSequenceChanged;
        }

        public IList<FrameModel> FrameModels
        {
            get { return _frameModels; }
        }

        public void Add(Frame frame)
        {
            _frameSequence.Push(frame);

            //добавляем модель для отображения
            var frameModel = new FrameModel(frame);
            frameModel.ModelChanged += OnFrameSequenceChanged;
            _frameModels.Add(frameModel);
        }

        public void PushBack(LightPlayer.BL.Commands.CmdEnum cmd)
        {
            var startTime = CalculateFrameStartTime();
            var frame = _builder.CreateFrameByCmdEnum(cmd, startTime);
            _frameSequence.Push(frame);

            //добавляем модель для отображения
            var frameModel = new FrameModel(frame);
            frameModel.ModelChanged += OnFrameSequenceChanged;
            _frameModels.Add(frameModel);
        }

        public void ConvertFrame(FrameModel model, Frame frame)
        {
            int idx = _frameModels.IndexOf(model);
            if (idx >= 0)
            {
                _frameSequence.ChangeFrame(model.Frame, frame);
                _frameModels[idx] = new FrameModel(frame);
            }
        }

        public void SaveToFile(string fileName)
        {
            try
            {
                using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    //var s = TypeSerializer.Dump(_frameSequence);
                    //JsonSerializer.SerializeToStream(_frameSequence, _frameSequence.GetType(), stream);
                    //TextWriter tw = new StreamWriter(stream);
                    _frameSequence.SequenceChanged -= OnFrameSequenceChanged;
                    var bf = new BinaryFormatter();
                    bf.Serialize(stream, _frameSequence);
                    _frameSequence.SequenceChanged += OnFrameSequenceChanged;
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

                _frameSequence = obj as FrameSequence;
                if (_frameSequence != null)
                {
                    _frameModels.Clear();
                    foreach (var frame in _frameSequence.Frames)
                        _frameModels.Add(new FrameModel(frame));
                }
            }
        }

        //PRIVATE METHODS
        private void OnFrameSequenceChanged(object sender, EventArgs eventArgs)
        {
            var view = CollectionViewSource.GetDefaultView(_frameModels);
            view.Refresh();
        }

        private TimeSpan CalculateFrameStartTime()
        {
            if (_frameModels.Count <= 0) return TimeSpan.FromSeconds(0);

            var lastFrame = _frameModels.Last();
            var startTime = lastFrame.FrameBegin + lastFrame.FrameLength;
            return startTime;
        }
    }
}
