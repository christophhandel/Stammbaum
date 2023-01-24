using FamilyTreeMongoApp.Model.Person;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public interface IPersonService
{
    Task<Person> AddPerson(PersonDto request);
    Task<Person> GetPersonById(ObjectId objectId);
}