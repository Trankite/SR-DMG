namespace StarRailDamage.Source.UI.Factory.PropertyBinding
{
    [Flags]
    public enum PropertyBindingMode
    {
        OneWay = 0x01,
        OneWayToSource = 0x02,
        TwoWay = OneWay | OneWayToSource
    }
}