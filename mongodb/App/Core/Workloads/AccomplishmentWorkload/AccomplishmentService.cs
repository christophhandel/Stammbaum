using AutoMapper;
using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Core.Workloads.Person;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.PersonDetails;
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

    public async Task<Accomplishment?> AddAccomplishment(AccomplishmentDto request)
    {
        return await _repository.AddAccomplishment(_mapper.Map<Accomplishment>(request));
    }

    public async Task DeleteAccomplishment(ObjectId objectId)
    {
        await _repository.DeleteAccomplishment(objectId);
    }

    public async Task<Accomplishment?> GetAccomplishmentById(ObjectId parse)
    {
        return await _repository.GetAccomplishmentById(parse);
    }

    public async Task<IEnumerable<Accomplishment>> GetAllAccomplishments()
    {
        return await _repository.GetAllAccomplishments();
    }

    public async Task<Accomplishment> UpdateAccomplishment(ObjectId objectId, string description, DateTime time)
    {
        return await _repository.UpdateAccomplishment(objectId,description,time);
    }
}