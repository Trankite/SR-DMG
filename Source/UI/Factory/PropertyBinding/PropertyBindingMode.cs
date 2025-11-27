namespace StarRailDamage.Source.UI.Factory.PropertyBinding
{
    [Flags]
    public enum PropertyBindingMode
    {
        OneWay = 1 << 0,
        OneWayToSource = 1 << 1,
        TwoWay = OneWay | OneWayToSource
    }
}