using LeoMongo.Database;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.FamilyTree;

public sealed class FamilyTree : EntityBase
{
    public DateTime CreatedAt { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<ObjectId> FirstPeople { get; set; } = default!;
}