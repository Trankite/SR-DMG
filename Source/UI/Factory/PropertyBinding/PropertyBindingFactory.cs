using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Factory.PropertyExpression;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;

namespace StarRailDamage.Source.UI.Factory.PropertyBinding
{
    public class PropertyBindingFactory<TSender> : IPropertyBindingFactory<TSender> where TSender : DependencyObject
    {
        private readonly Dictionary<string, IPropertyBinding<TSender>> ModelHandlers = [];

        private readonly Dictionary<string, IPropertyBinding<TSender>> DependHandlers = [];

        private readonly Dictionary<DependencyObject, PropertyChangedEventHandler> NotifyHandlers = [];

        public DependencyProperty DependBinding<TValue>(Expression<Func<TSender, TValue?>> modelExpression, Expression<Func<TSender, TValue?>> dependExpression, PropertyBindingMode bindingMode = PropertyBindingMode.OneWay, object? defaultValue = null, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null)
        {
            return DependBinding<TValue>(AddBinding(modelExpression, dependExpression, bindingMode), defaultValue, propertyChangedCallback, coerceValueCallback, validateValueCallback);
        }

        public string AddBinding<TValue>(Expression<Func<TSender, TValue>> modelProperty, Expression<Func<TSender, TValue>> dependProperty, PropertyBindingMode bindingMode)
        {
            PropertyExpression<TSender, TValue> ModelExpression = PropertyExpressionFactory.GetPropertyExpression(modelProperty);
            PropertyExpression<TSender, TValue> DependExpression = PropertyExpressionFactory.GetPropertyExpression(dependProperty);
            void DependToModel(TSender sender)
            {
                if (DependExpression.TryGetValue(sender, out TValue? value)) ModelExpression.TrySetValue(sender, value);
            }
            void ModelToDepend(TSender sender)
            {
                if (ModelExpression.TryGetValue(sender, out TValue? value)) DependExpression.TrySetValue(sender, value);
            }
            PropertyBinding<TSender> PropertyBinding = new()
            {
                BindingMode = bindingMode,
                DependHanlder = DependToModel,
                ModelHanlder = ModelToDepend
            };
            ModelHandlers[modelProperty.FullName().FirstSplit('.').Last()] = PropertyBinding;
            return dependProperty.FullName().Invoke(x => DependHandlers[x] = PropertyBinding);
        }

        public DependencyProperty DependBinding<TValue>(string name, object? defaultValue = null, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null)
        {
            propertyChangedCallback += DependChangedCallback;
            return DependProperty<TValue>(name, defaultValue, propertyChangedCallback, coerceValueCallback, validateValueCallback);
        }

        private void DependChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TSender sender) DependHandlers[e.Property.Name].DependToModel(sender);
        }

        public DependencyProperty ModelBinding<TProperty>(string name, object? defaultValue = null, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null)
        {
            propertyChangedCallback += ModelChangedCallback;
            return DependProperty<TProperty>(name, defaultValue, propertyChangedCallback, coerceValueCallback, validateValueCallback);
        }

        public DependencyProperty DependProperty<TProperty>(string name, object? defaultValue = null, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null)
        {
            return DependencyProperty.Register(name, typeof(TProperty), typeof(TSender), new PropertyMetadata(defaultValue ?? default(TProperty), propertyChangedCallback, coerceValueCallback), validateValueCallback);
        }

        private void ModelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is INotifyPropertyChanged OldNotifyProperty)
            {
                OldNotifyProperty.PropertyChanged -= NotifyHandlers.GetValueOrDefault(d);
            }
            if (d is not TSender Sender) return;
            if (e.NewValue is not INotifyPropertyChanged NewNotifyProperty) return;
            void PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                ModelHandlers.GetValueOrDefault(e.PropertyName.ThrowIfNull())?.ModelToDepend(Sender);
            }
            NotifyHandlers[d] = PropertyChanged;
            NewNotifyProperty.PropertyChanged += PropertyChanged;
            foreach (IPropertyBinding<TSender> PropertyBinding in ModelHandlers.Values)
            {
                PropertyBinding.ModelToDepend(Sender);
            }
        }
    }
}