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
            FirstName = request.FirstName,
            LastName = request.LastName,
            Father = ObjectId.Parse(request.FatherId),
            Mother = ObjectId.Parse(request.MotherId),
        });
    }

    public Task<Person> GetPersonById(ObjectId objectId)
    {
        return _repository.GetPersonById(objectId);
    }
}