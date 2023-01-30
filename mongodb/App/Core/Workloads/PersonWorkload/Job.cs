using LeoMongo.Database;

namespace FamilyTreeMongoApp.Core.Workloads.PersonWorkload;

public class Job : EntityBase
{
    public string Name { get; set; } = default!;
    public string JobType { get; set; } = default!;
}