using System.Windows;

namespace StarRailDamage.Source.UI.Factory.PropertyBinding
{
    public interface IPropertyBindingFactory
    {
        void AddBinding<TValue>(string modelProperty, string dependProperty, PropertyBindingMode? bindingMode = null);

        DependencyProperty ModelBinding<TProperty>(string name, object? defaultValue = null, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null);

        DependencyProperty DependBinding<TProperty>(string name, object? defaultValue = null, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null);
    }
}