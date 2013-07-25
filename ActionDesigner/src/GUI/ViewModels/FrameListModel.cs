using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using Intems.LightPlayer.BL;
using ServiceStack.Text;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameListModel : BaseViewModel
    {
        private readonly FrameSequence _frameSequence;
        private readonly ObservableCollection<FrameModel> _frameModels;

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

        public void SaveToFile(string fileName)
        {
            try
            {
                var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read);
                using (stream)
                {
                    var s = TypeSerializer.Dump(_frameSequence);
                    //JsonSerializer.SerializeToStream(_sequence, _sequence.GetType(), stream);
                    TextWriter tw = new StreamWriter(stream);
                    tw.Write(s);
                    tw.Flush();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void ConvertFrame(FrameModel model, Frame frame)
        {
            int idx = _frameModels.IndexOf(model);
            if(idx >= 0)
            {
                _frameSequence.ChangeFrame(model.Frame, frame);
                _frameModels[idx] = new FrameModel(frame);
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
            var lastFrame = _frameModels.Last();
            var startTime = lastFrame.FrameBegin + lastFrame.FrameLength;
            return startTime;
        }
    }
}
