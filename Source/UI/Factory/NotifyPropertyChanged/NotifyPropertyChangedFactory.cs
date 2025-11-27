using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StarRailDamage.Source.UI.Factory.NotifyPropertyChanged
{
    public abstract partial class NotifyPropertyChangedFactory : INotifyPropertyChangedFactory
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly Dictionary<string, PropertyChangedEventHandler> Handlers = [];

        public bool OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (PropertyChanged == null) return false;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        public bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            return OnPropertyChanged(propertyName);
        }

        public bool SetField<T>(ref T field, T value, Predicate<T> predicate, [CallerMemberName] string? propertyName = null)
        {
            return predicate.Invoke(value) && SetField(ref field, value, propertyName);
        }

        public bool SetNotifyField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) where T : INotifyPropertyChanged
        {
            if (Equals(field, value)) return false;
            ArgumentNullException.ThrowIfNull(propertyName);
            if (field != null)
            {
                field.PropertyChanged -= Handlers.GetValueOrDefault(propertyName);
            }
            if (!Equals(null, field = value))
            {
                void PropertyChanged(object? sender, PropertyChangedEventArgs e)
                {
                    OnPropertyChanged($"{propertyName}.{e.PropertyName}");
                }
                Handlers[propertyName] = PropertyChanged;
                field.PropertyChanged += PropertyChanged;
            }
            return true;
        }

        public bool SetNotifyField<T>(ref T field, T value, Predicate<T> predicate, [CallerMemberName] string? propertyName = null) where T : INotifyPropertyChanged
        {
            return predicate.Invoke(value) && SetNotifyField(ref field, value, propertyName);
        }
    }
}