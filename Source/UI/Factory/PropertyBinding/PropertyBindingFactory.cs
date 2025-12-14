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

        private readonly Dictionary<INotifyPropertyChanged, PropertyChangedEventHandler> NotifyHandlers = [];

        public string AddBinding<TProperty>(Expression<Func<TSender, TProperty>> modelProperty, Expression<Func<TSender, TProperty>> dependProperty, PropertyBindingMode bindingMode = PropertyBindingMode.OneWay)
        {
            PropertyExpressionFactory<TSender, TProperty> Factory = new();
            IPropertyExpression<TSender, TProperty> ModelExpression = Factory.GetPropertyExpression(modelProperty);
            IPropertyExpression<TSender, TProperty> DependExpression = Factory.GetPropertyExpression(dependProperty);
            void DependToModel(TSender sender)
            {
                if (DependExpression.TryGetValue(sender, out TProperty? value)) ModelExpression.TrySetValue(sender, value);
            }
            void ModelToDepend(TSender sender)
            {
                if (ModelExpression.TryGetValue(sender, out TProperty? value)) DependExpression.TrySetValue(sender, value);
            }
            PropertyBinding<TSender> PropertyBinding = new()
            {
                BindingMode = bindingMode,
                DependHanlder = DependToModel,
                ModelHanlder = ModelToDepend
            };
            ModelHandlers[modelProperty.FullName().FirstSplit('.').Last()] = PropertyBinding;
            return dependProperty.FullName().Configure(x => DependHandlers[x] = PropertyBinding);
        }

        public DependencyProperty DependBinding<TProperty>(Expression<Func<TSender, TProperty?>> modelExpression, Expression<Func<TSender, TProperty?>> dependExpression, PropertyBindingMode bindingMode = PropertyBindingMode.OneWay, TProperty? defaultValue = default, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null)
        {
            return DependBinding(AddBinding(modelExpression, dependExpression, bindingMode), defaultValue, propertyChangedCallback, coerceValueCallback, validateValueCallback);
        }

        public DependencyProperty DependBinding<TProperty>(string name, TProperty? defaultValue = default, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null)
        {
            return DependProperty(name, defaultValue, propertyChangedCallback += DependChangedCallback, coerceValueCallback, validateValueCallback);
        }

        private void DependChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TSender sender)
            {
                DependHandlers[e.Property.Name].DependToModel(sender);
            }
        }

        public DependencyProperty ModelBinding<TProperty>(Expression<Func<TSender, TProperty?>> modelExpression, TProperty? defaultValue = default, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null) where TProperty : INotifyPropertyChanged
        {
            return ModelBinding(modelExpression.FullName(), defaultValue, propertyChangedCallback, coerceValueCallback, validateValueCallback);
        }

        public DependencyProperty ModelBinding<TProperty>(string name, TProperty? defaultValue = default, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null) where TProperty : INotifyPropertyChanged
        {
            return DependProperty(name, defaultValue, propertyChangedCallback += ModelChangedCallback, coerceValueCallback, validateValueCallback);
        }

        public void ClearModelBinding<TProperty>(TProperty? model) where TProperty : INotifyPropertyChanged
        {
            if (model.IsNull()) return;
            if (NotifyHandlers.Remove(model, out PropertyChangedEventHandler? PropertyChangedEventHandler))
            {
                model.PropertyChanged -= PropertyChangedEventHandler;
            }
        }

        private void ModelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ClearModelBinding(e.OldValue as INotifyPropertyChanged);
            if (d is not TSender Sender) return;
            if (e.NewValue is not INotifyPropertyChanged NotifyProperty) return;
            void PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                ModelHandlers.GetValueOrDefault(e.PropertyName.ThrowIfNull())?.ModelToDepend(Sender);
            }
            NotifyHandlers[NotifyProperty] = PropertyChanged;
            NotifyProperty.PropertyChanged += PropertyChanged;
            foreach (IPropertyBinding<TSender> PropertyBinding in ModelHandlers.Values)
            {
                PropertyBinding.ModelToDepend(Sender);
            }
        }

        public DependencyProperty DependProperty<TProperty>(Expression<Func<TSender, TProperty?>> dependExpression, TProperty? defaultValue = default, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null)
        {
            return DependProperty(dependExpression.FullName(), defaultValue, propertyChangedCallback, coerceValueCallback, validateValueCallback);
        }

        public DependencyProperty DependProperty<TProperty>(string name, TProperty? defaultValue = default, PropertyChangedCallback? propertyChangedCallback = null, CoerceValueCallback? coerceValueCallback = null, ValidateValueCallback? validateValueCallback = null)
        {
            return DependencyProperty.Register(name, typeof(TProperty), typeof(TSender), new PropertyMetadata(defaultValue, propertyChangedCallback, coerceValueCallback), validateValueCallback);
        }
    }
}