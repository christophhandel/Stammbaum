using FamilyTreeMongoApp.Core.Util;

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
}