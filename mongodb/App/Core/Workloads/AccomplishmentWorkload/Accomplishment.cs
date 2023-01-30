using LeoMongo.Database;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public class Accomplishment : EntityBase
{
    public string Name { get; set; } = default!;
    public DateTime Time { get; set; } = default!;
}