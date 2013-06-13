using System.Collections.Generic;
using Intems.LightPlayer.BL;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameListModel : BaseViewModel
    {
        private IList<FrameModel> _frames = new List<FrameModel>();
        public IList<FrameModel> FrameViews
        {
            get { return _frames; }
            set
            {
                _frames = value;
                RaisePropertyChanged("FrameListModel");
            }
        }

        private readonly FrameSequence _sequence = new FrameSequence();
        public void Add(Frame frame)
        {
            _sequence.Push(frame);
            _frames.Add(new FrameModel(frame));
        }
    }
}
