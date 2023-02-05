using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Model.Statistics;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.PersonWorkload;

public interface IPersonService
{
    Task<Person> AddPerson(PersonDto request);
    Task<IEnumerable<Person>> GetPeopleBySex(string? sex);
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

    Task<Person> UpdatePerson(
        ObjectId id,
        string firstname,
        string lastname,
        ObjectId? motherId,
        ObjectId? fatherId,
        string personSex,
        Location? birthplace,
        ObjectId? job,
        ObjectId? company);
        Task DeletePerson(ObjectId objectId);
        Task<int> GetAccomplishmentsCount(ObjectId objectId);
        Task<IEnumerable<Person>> GetDescendantsInCompany(Person objectId, Company company);
        Task<IEnumerable<Person>> GetDescendants(Person objectId);
        Task DeleteCollection();
        Task<IEnumerable<Person>> GetAncestors(Person person);
}
