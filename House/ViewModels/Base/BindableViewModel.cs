using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace House.ViewModels.Base
{
    public abstract class BindableViewModel<TModel> : INotifyPropertyChanged
    {
        public abstract TModel ToModel();
        public abstract void LoadFromModel(TModel model);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(name);
            return true;
        }
    }
}
