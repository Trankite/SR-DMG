using StarRailDamage.Source.Extension;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Note
{
    public static class NoteRequestBuilderFactoryExtenison
    {
        public static NoteRequestBuilderFactory SetUid(this NoteRequestBuilderFactory builder, string uid)
        {
            return builder.Configure(builder.Uid = uid);
        }

        public static NoteRequestBuilderFactory SetServer(this NoteRequestBuilderFactory builder, string server)
        {
            return builder.Configure(builder.Server = server);
        }
    }
}