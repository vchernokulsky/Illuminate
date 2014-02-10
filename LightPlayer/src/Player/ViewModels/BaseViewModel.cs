using System.ComponentModel;

namespace Intems.LightPlayer.GUI.ViewModels
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
}