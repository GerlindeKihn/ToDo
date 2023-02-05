
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDo.App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected void SetProperty<T>(
            ref T field,
            T value,
            [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;

            field = value;
            RaisePropertyChangedEvent(propertyName);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChangedEvent(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}