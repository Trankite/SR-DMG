namespace StarRailDamage.Source.UI.Factory.PropertyBinding
{
    public interface IPropertyBinding<TSender>
    {
        Action<TSender> ModelHanlder { get; set; }

        Action<TSender> DependHanlder { get; set; }

        bool ModelToDepend(TSender sender);

        bool IsModelToDepend { get; }

        bool DependToModel(TSender sender);

        bool IsDependToModel { get; }
    }
}