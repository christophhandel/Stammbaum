using AutoMapper;
using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;
using FamilyTreeMongoApp.Model.Person;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;

public sealed class AccomplishmentService : IAccomplishmentService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAccomplishmentRepository _repository;
    private readonly IMapper _mapper;
    

    public AccomplishmentService(IDateTimeProvider dateTimeProvider, 
        IAccomplishmentRepository repository,
        IMapper mapper)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
        _mapper = mapper;
    }
    
}