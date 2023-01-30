using LeoMongo.Database;

namespace FamilyTreeMongoApp.Core.Workloads.JobWorkload;

public class Company : EntityBase
{
    public string Name { get; set; } = default!;
    public string JobType { get; set; } = default!;
}