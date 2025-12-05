using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;

namespace StarRailDamage.Source.UI.Factory.PropertyBinding
{
    public interface IPropertyBindingFactory<TSender> where TSender : DependencyObject
    {
        string AddBinding<TProperty>(Expression<Func<TSender, TProperty>> modelProperty, Expression<Func<TSender, TProperty>> dependProperty, PropertyBindingMode bindingMode = PropertyBindingMode.OneWay);

        DependencyProperty ModelBinding<TProperty>(string name, TProperty? defaultValue = default, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null) where TProperty : INotifyPropertyChanged;

        DependencyProperty DependBinding<TProperty>(string name, TProperty? defaultValue = default, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null);

        void ClearModelBinding<TProperty>(TProperty? model) where TProperty : INotifyPropertyChanged;
    }
}