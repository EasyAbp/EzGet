namespace EasyAbp.EzGet
{
    public static class EzGetDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EzGet";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EzGet";
    }
}
