using FamilyTreeMongoApp.Model.Person;
using LeoMongo.Database;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public interface IPersonRepository : IRepositoryBase
{
    Task<Person> AddPerson(Person request);
    Task<IEnumerable<Person>> GetPeopleBySex(string? sex);
    Task<Person> GetPersonById(ObjectId objectId);
}