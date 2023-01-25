using FamilyTreeMongoApp.Model.Person;
using LeoMongo.Database;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public interface IPersonRepository : IRepositoryBase
{
    Task<Person> AddPerson(Person request);
    Task<IReadOnlyCollection<Person>> GetPeopleBySex(string? sex);
    Task<Person?> GetPersonById(ObjectId objectId);
    Task<IReadOnlyCollection<Person>> GetPeopleWithNoParents();
    Task<IReadOnlyCollection<Person>> GetPeopleByParents(ObjectId? motherId, ObjectId? fatherId);
    Task<Person> UpdatePerson(ObjectId id, string firstname, string lastname, ObjectId? motherId, ObjectId? fatherId, string personSex);
}