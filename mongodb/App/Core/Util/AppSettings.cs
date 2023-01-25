namespace FamilyTreeMongoApp.Core.Util;

public sealed class AppSettings
{
    public const string Key = "AppSettings";
    public string DatabaseType {get;set;} = default!;
    public string ConnectionString { get; set; } = default!;
    public string Database { get; set; } = default!;

    public Uri Neo4jConnection { get; set; } = default!;
    public Uri Neo4jClientConnection { get; set; } = default!;

    public string Neo4jUser { get; set; } = default!;

    public string Neo4jPassword { get; set; } = default!;

    public string Neo4jDatabase { get; set; } = default!;
}