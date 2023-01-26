using LeoMongo.Database;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public class Accomplishment : EntityBase
{
    public DateTime Time { get; set; } = default!;
    public string Description { get; set; } = default!;
}