using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Model.Person;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public sealed class PersonService : IPersonService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPersonRepository _repository;

    public PersonService(IDateTimeProvider dateTimeProvider, IPersonRepository repository)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
    }

    public async Task<Person> AddPerson(PersonDto request)
    {
        return await _repository.AddPerson(new Person {
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Sex = request.Sex,
            Father = string.IsNullOrWhiteSpace(request.FatherId)?null: ObjectId.Parse(request.FatherId),
            Mother = string.IsNullOrWhiteSpace(request.MotherId)?null: ObjectId.Parse(request.MotherId) 
        });
    }

    public async Task<IEnumerable<Person>> GetPeopleBySex(string? sex)
    {
        return await _repository.GetPeopleBySex(sex);
    }

    public Task<Person> GetPersonById(ObjectId objectId)
    {
        return _repository.GetPersonById(objectId);
    }
}