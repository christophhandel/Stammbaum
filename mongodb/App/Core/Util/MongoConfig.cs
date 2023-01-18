using LeoMongo;
using Microsoft.Extensions.Options;

namespace FamilyTreeMongoApp.Core.Util;

public sealed class MongoConfig : IMongoConfig
{
    public MongoConfig(IOptions<AppSettings> options)
    {
        var settings = options.Value;
        ConnectionString = settings.ConnectionString;
        DatabaseName = settings.Database;
    }

    public string ConnectionString { get; }
    public string DatabaseName { get; }
}