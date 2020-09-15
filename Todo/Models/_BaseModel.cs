using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Todo.Models
{
    /// <summary>
    /// Base model implements INotifyPropertyChanged on the mutator of Model Properties
    /// </summary>
    public abstract class _BaseModel : INotifyPropertyChanged, IModel
    {
        public _BaseModel()
        {
            Id = Guid.NewGuid();
        }

        [PrimaryKey]
        public Guid Id { get; }

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
