using System.ComponentModel;

namespace Intems.LightPlayer.GUI
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propName)
        {
            var pc = PropertyChanged;
            if (pc != null)
                pc(this, new PropertyChangedEventArgs(propName));
        }
    }

    internal class MainViewModel : BaseViewModel
    {
        private string _audioFileName;
        public string AudioFileName
        {
            get { return _audioFileName; }
            set { _audioFileName = value; RaisePropertyChanged("AudioFileName"); }
        }

        private string _framesFileName;
        public string FramesFileName
        {
            get { return _framesFileName; }
            set { _framesFileName = value; RaisePropertyChanged("FramesFileName"); }
        }
    }
}
