using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Model.PersonDetails;

public class PersonWithParents : Core.Workloads.PersonWorkload.Person
{
    public List<Core.Workloads.PersonWorkload.Person> Parents = default!;
}