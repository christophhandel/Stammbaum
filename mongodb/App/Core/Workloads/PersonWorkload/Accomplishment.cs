using LeoMongo.Database;

namespace FamilyTreeMongoApp.Core.Workloads.PersonWorkload;

public class Accomplishment : EntityBase
{
    public DateTime Time { get; set; } = default!;
    public string Description { get; set; } = default!;
}