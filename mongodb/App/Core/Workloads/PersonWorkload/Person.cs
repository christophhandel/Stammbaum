using LeoMongo.Database;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.PersonWorkload;

public class Person : EntityBase,IComparable<Person>
{
    public string Firstname { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public string Sex { get; set; } = default!; // m, f
    public DateTime BirthDate { get; set; } = default!;
    public DateTime? DeathTime { get; set; } = default!;

    public Location? BirthPlace { get; set; } = default!;
    
    public ObjectId? Job { get; set; } = null;

    public ObjectId? Company { get; set; } = null;

    public List<ObjectId> Accomplishments { get; set; } = default!;
     
    public ObjectId? Mother { get; set; } = null;
    public ObjectId? Father { get; set; } = null;
    public int CompareTo(Person? other)
    {
        return this.CompareTo(other);
    }
}