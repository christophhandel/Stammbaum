using LeoMongo.Database;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public class Company : EntityBase
{
    public string Name { get; set; } = default!;
    public string BusinessActivity { get; set; } = default!;
}