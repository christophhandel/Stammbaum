using LeoMongo.Database;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public sealed class Person : EntityBase
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime BirthDate { get; set; } = default!;
    public DateTime? DeathTime { get; set; } = default!;
    public Location BirthPlace { get; set; } = default!;
    
    public ObjectId Job { get; set; } = default!;

    public ObjectId Company { get; set; } = default!;

    public List<ObjectId> Accomplishments { get; set; } = default!;
     
    public ObjectId Mother { get; set; } = default!;
    public ObjectId Father { get; set; } = default!;
}