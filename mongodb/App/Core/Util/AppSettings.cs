namespace FamilyTreeMongoApp.Core.Util;

public sealed class AppSettings
{
    public const string Key = "AppSettings";

    public string ConnectionString { get; set; } = default!;
    public string Database { get; set; } = default!;
}