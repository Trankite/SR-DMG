using System.Linq.Expressions;
using System.Windows;

namespace StarRailDamage.Source.UI.Factory.PropertyBinding
{
    public interface IPropertyBindingFactory<TSender>
    {
        string AddBinding<TValue>(Expression<Func<TSender, TValue>> modelProperty, Expression<Func<TSender, TValue>> dependProperty, PropertyBindingMode bindingMode);

        DependencyProperty ModelBinding<TValue>(string name, object? defaultValue = null, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null);

        DependencyProperty DependBinding<TValue>(string name, object? defaultValue = null, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null);
    }
}