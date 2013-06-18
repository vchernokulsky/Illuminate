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
        private readonly ObservableCollection<FrameModel> _frames;
        private readonly FrameSequence _sequence;

        private FrameBuilder _builder;

        public FrameListModel()
        {
            _builder = new FrameBuilder();
            _frames = new ObservableCollection<FrameModel>();
            _sequence = new FrameSequence();
            _sequence.SequenceChanged += OnSequenceChanged;
        }

        public IList<FrameModel> FrameViews
        {
            get { return _frames; }
        }


        public void Add(Frame frame)
        {
            _sequence.Push(frame);
            _frames.Add(new FrameModel(frame));
        }

        public void PushBack(LightPlayer.BL.Commands.CmdEnum cmd)
        {
            var lastFrame = _frames.Last();
            var startTime = lastFrame.FrameBegin + lastFrame.FrameLength;
            var frame = _builder.CreateFrameByCmdEnum(cmd, startTime);

            _sequence.Push(frame);
            _frames.Add(new FrameModel(frame));
        }

        private void OnSequenceChanged(object sender, EventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(_frames);
            view.Refresh();
        }

        public void SaveToFile()
        {
            try
            {
                var stream = new FileStream("composition.json", FileMode.Create, FileAccess.Write, FileShare.Read);
                using (stream)
                {
                    var s = TypeSerializer.Dump(_sequence);
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
    }
}
