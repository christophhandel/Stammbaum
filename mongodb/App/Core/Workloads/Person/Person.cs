using LeoMongo.Database;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public sealed class Person : EntityBase
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime BirthDate { get; set; } = default!;
    public Location BirthPlace { get; set; } = default!;
    
    public List<ObjectId> Children { get; set; } = default!;
}