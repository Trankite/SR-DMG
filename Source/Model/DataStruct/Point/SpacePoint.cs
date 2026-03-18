namespace StarRailDamage.Source.Model.DataStruct.Point
{
    public readonly struct SpacePoint
    {
        public readonly int X;

        public readonly int Y;

        public readonly int Z;

        public SpacePoint(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}