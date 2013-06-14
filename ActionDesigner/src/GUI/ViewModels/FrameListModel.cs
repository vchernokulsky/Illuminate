using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Intems.LightPlayer.BL;

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

        private void OnSequenceChanged(object sender, EventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(_frames);
            view.Refresh();
        }
    }
}
