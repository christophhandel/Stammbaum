using FamilyTreeMongoApp.Core.Util;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public sealed class FamilyTreeService : IFamilyTreeService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPersonRepository _repository;

    public FamilyTreeService(IDateTimeProvider dateTimeProvider, IPersonRepository repository)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
    }
}