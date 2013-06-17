using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameListModel : BaseViewModel
    {
        private readonly ObservableCollection<FrameModel> _frames;
        private readonly FrameSequence _sequence;

        public FrameListModel()
        {
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

        public void PushBack(Command cmd)
        {
            var lastFrame = _frames.Last();
            var startTime = lastFrame.FrameBegin + lastFrame.FrameLength;
            var length = TimeSpan.FromSeconds(cmd.Length);

            var frame = new Frame(startTime, length){Command = cmd};
            _sequence.Push(frame);
            _frames.Add(new FrameModel(frame));
        }

        private void OnSequenceChanged(object sender, EventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(_frames);
            view.Refresh();
        }
    }
}
