namespace StarRailDamage.Source.UI.Factory.PropertyBinding
{
    public class PropertyBinding<TSender> : IPropertyBinding<TSender>
    {
        public required Action<TSender> ModelHanlder { get; set; }

        public required Action<TSender> DependHanlder { get; set; }

        public PropertyBindingMode BindingMode { get; set; }

        public bool IsModelToDepend => BindingMode is PropertyBindingMode.OneWay or PropertyBindingMode.TwoWay;

        public bool IsDependToModel => BindingMode is PropertyBindingMode.OneWayToSource or PropertyBindingMode.TwoWay;

        public bool DependToModel(TSender sender)
        {
            return PropertyBinding<TSender>.PropertyCharge(sender, IsDependToModel, DependHanlder);
        }

        public bool ModelToDepend(TSender sender)
        {
            return PropertyBinding<TSender>.PropertyCharge(sender, IsModelToDepend, ModelHanlder);
        }

        private static bool PropertyCharge(TSender sender, bool isBinding, Action<TSender> handler)
        {
            if (isBinding)
            {
                handler(sender);
                return true;
            }
            return false;
        }
    }
}