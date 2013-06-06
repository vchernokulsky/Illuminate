using Intems.LightPlayer.BL;

namespace Intems.LightDesigner.GUI.ViewModels
{
    public class FrameListModel : BaseViewModel
    {
        private FrameSequence _frames;

        public FrameSequence Frames
        {
            get { return _frames; }
            set
            {
                _frames = value;
                RaisePropertyChanged("FrameListModel");
            }
        }
    }
}
