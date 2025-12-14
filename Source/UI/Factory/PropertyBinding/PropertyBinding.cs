using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.UI.Factory.PropertyBinding
{
    public class PropertyBinding<TSender> : IPropertyBinding<TSender>
    {
        public required Action<TSender> ModelHanlder { get; set; }

        public required Action<TSender> DependHanlder { get; set; }

        public PropertyBindingMode BindingMode { get; set; }

        public bool IsModelToDepend => BindingMode.HasFlag(PropertyBindingMode.OneWay);

        public bool IsDependToModel => BindingMode.HasFlag(PropertyBindingMode.OneWayToSource);

        public bool DependToModel(TSender sender)
        {
            return PropertyBinding<TSender>.PropertyChange(sender, IsDependToModel, DependHanlder);
        }

        public bool ModelToDepend(TSender sender)
        {
            return PropertyBinding<TSender>.PropertyChange(sender, IsModelToDepend, ModelHanlder);
        }

        private static bool PropertyChange(TSender sender, bool isBinding, Action<TSender> handler)
        {
            return isBinding && true.Configure(handler, sender);
        }
    }
}