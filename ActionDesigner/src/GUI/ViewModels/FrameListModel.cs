using System.Collections.Generic;

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
    }
}
