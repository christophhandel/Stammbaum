using AutoMapper;
using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Model.Person;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public sealed class PersonService : IPersonService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;
    

    public PersonService(IDateTimeProvider dateTimeProvider, IPersonRepository repository, IMapper mapper)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Person> AddPerson(PersonDto request)
    {
        return await _repository.AddPerson(new Person {
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Sex = request.Sex,
            Father = string.IsNullOrWhiteSpace(request.Father)?null: ObjectId.Parse(request.Father),
            Mother = string.IsNullOrWhiteSpace(request.Mother)?null: ObjectId.Parse(request.Mother),
            BirthPlace = request.BirthLocation == null ? null : _mapper.Map<Location>(request.BirthLocation!)!
        });
    }

    public async Task<IEnumerable<Person>> GetPeopleBySex(string? sex)
    {
        return await _repository.GetPeopleBySex(sex);
    }

    public Task<Person?> GetPersonById(ObjectId objectId)
    {
        return _repository.GetPersonById(objectId);
    }

    
    public Task<IReadOnlyCollection<Person>> GetPeopleByParents(ObjectId? motherId, ObjectId? fatherId)
    {
        if (motherId is null && fatherId is null)
        {
            return _repository.GetPeopleWithNoParents();
        }

        return _repository.GetPeopleByParents(motherId, fatherId);
    }

    public async Task<Person> UpdatePerson(ObjectId id, 
        string firstname, 
        string lastname, 
        ObjectId? motherId, 
        ObjectId? fatherId, 
        string personSex,
        Location? birthLocation,
        ObjectId? Job,
        ObjectId? Company)
    {
        return await _repository.UpdatePerson(id, 
            firstname,
            lastname,
            motherId,
            fatherId,
            personSex,
            birthLocation,
            Job,
            Company);
    }

    public async Task DeletePerson(ObjectId objectId)
    {
        await _repository.DeletePerson(objectId);
    }
}