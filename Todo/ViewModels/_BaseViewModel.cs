using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.Services;


namespace Todo.ViewModels
{
    /// <summary>
    /// Base view model implements INotifyPropertyChanged on the mutator of all ViewModel Properties
    /// </summary>
    public abstract class _BaseViewModel : INotifyPropertyChanged
    {
       
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        
        public _BaseViewModel()
        {
           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}

