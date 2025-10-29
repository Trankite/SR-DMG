using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Factory.PropertyExpression;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;

namespace StarRailDamage.Source.UI.Factory.PropertyBinding
{
    public class PropertyBindingFactory<TSender> : IPropertyBindingFactory where TSender : DependencyObject
    {
        private readonly Dictionary<string, IPropertyBinding<TSender>> ModelHandlers = [];

        private readonly Dictionary<string, IPropertyBinding<TSender>> DependHandlers = [];

        private readonly Dictionary<DependencyObject, PropertyChangedEventHandler> NotifyHandlers = [];

        public DependencyProperty DependBinding<TProperty>(Expression<Func<TSender, TProperty?>> modelExpression, Expression<Func<TSender, TProperty?>> dependExpression, PropertyBindingMode? bindingMode = null, object? defaultValue = null, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null)
        {
            string ModelProperty = string.Empty.FullName(modelExpression);
            string DependProperty = string.Empty.FullName(dependExpression);
            AddBinding<TProperty>(ModelProperty, DependProperty, bindingMode);
            return DependBinding<TProperty>(DependProperty, defaultValue, propertyChangedCallback, coerceValueCallback, validateValueCallback);
        }

        public void AddBinding<TValue>(string modelProperty, string dependProperty, PropertyBindingMode? bindingMode = null)
        {
            PropertyInfo[] ModelPropertyInfo = PropertyExpressionFactory.FindProperty(typeof(TSender), modelProperty);
            PropertyExpression<TSender, TValue> ModelExpression = PropertyExpressionFactory.GetPropertyExpression<TSender, TValue>(ModelPropertyInfo);
            PropertyInfo[] DependPropertyInfo = PropertyExpressionFactory.FindProperty(typeof(TSender), dependProperty);
            PropertyExpression<TSender, TValue> DependExpression = PropertyExpressionFactory.GetPropertyExpression<TSender, TValue>(DependPropertyInfo);
            PropertyBinding<TSender> PropertyBinding = new()
            {
                BindingMode = bindingMode ?? PropertyBindingMode.OneWay,
                DependHanlder = (sender) => ModelExpression.SetValue(sender, DependExpression.GetValue(sender)),
                ModelHanlder = (sender) => DependExpression.SetValue(sender, ModelExpression.GetValue(sender))
            };
            ModelHandlers[modelProperty.FirstSplit('.').Last()] = PropertyBinding;
            DependHandlers[dependProperty] = PropertyBinding;
        }

        public DependencyProperty DependBinding<TProperty>(string name, object? defaultValue = null, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null)
        {
            propertyChangedCallback += DependChangedCallback;
            return DependProperty<TProperty>(name, defaultValue, propertyChangedCallback, coerceValueCallback, validateValueCallback);
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
                ArgumentNullException.ThrowIfNull(e.PropertyName);
                ModelHandlers.GetValueOrDefault(e.PropertyName)?.ModelToDepend(Sender);
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