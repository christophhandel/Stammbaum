using LeoMongo.Database;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public class Location : EntityBase
{
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
}