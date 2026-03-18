namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public struct ECCodeGroup
    {
        public int ECCodePerBytes;

        public int BlocksInGroup1;

        public int CodewordsInGroup1;

        public int BlocksInGroup2;

        public int CodewordsInGroup2;

        public ECCodeGroup(int eCCodePerBytes, int blocksInGroup1, int codewordsInGroup1, int blocksInGroup2, int codewordsInGroup2)
        {
            ECCodePerBytes = eCCodePerBytes;
            BlocksInGroup1 = blocksInGroup1;
            CodewordsInGroup1 = codewordsInGroup1;
            BlocksInGroup2 = blocksInGroup2;
            CodewordsInGroup2 = codewordsInGroup2;
        }
    }
}