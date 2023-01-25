using FamilyTreeMongoApp.Model.Person;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public interface IPersonService
{
    Task<Person> AddPerson(PersonDto request);
    Task<IReadOnlyCollection<Person>> GetPeopleBySex(string? sex);
    Task<Person?> GetPersonById(ObjectId objectId);
    /// <summary>
    /// Returns the people by motherId or fatherId
    ///
    /// if both motherId and fatherId are null, return people with no parents aka the root of the family tree.
    /// 
    /// </summary>
    /// <param name="motherId">Mother ID</param>
    /// <param name="fatherId">Father ID</param>
    /// <returns></returns>
    Task<IReadOnlyCollection<Person>> GetPeopleByParents(ObjectId? motherId, ObjectId? fatherId);

    Task<Person> UpdatePerson(ObjectId id, 
        string firstname, 
        string lastname, 
        ObjectId? motherId, 
        ObjectId? fatherId, 
        string personSex);
}