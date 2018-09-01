using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sales.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingfield, T value, [CallerMemberName] string propertyName = null)
        {
            if(EqualityComparer<T>.Default.Equals(backingfield, value))
            {
                return;
            }

            backingfield = value;
            this.OnPropertyChanged(propertyName);
        }
    }
}
