using LeoMongo.Database;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public class Accomplishment : EntityBase
{
    public string Description { get; set; } = default!;
    public DateTime Time { get; set; } = default!;
}