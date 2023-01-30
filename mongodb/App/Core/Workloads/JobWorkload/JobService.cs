using AutoMapper;
using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.PersonDetails;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.JobWorkload;

public sealed class JobService : IJobService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IJobRepository _repository;
    private readonly IMapper _mapper;
    

    public JobService(IDateTimeProvider dateTimeProvider, IJobRepository repository, IMapper mapper)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<Job?> GetJobById(ObjectId parse)
    {
        return await _repository.GetJobById(parse);
    }
    
    public async Task<Job?> AddJob(JobDto request)
    {
        return await _repository.AddJob(new Job
        {
            Name = request.Name,
            JobType = request.JobType
        });
    }

    public Task<IEnumerable<Job>> GetAllJobs()
    {
        return _repository.GetAllJobs();
    }

    public async Task<Job> UpdateJob(ObjectId objectId, string jobName, string jobDescription)
    {
        return await _repository.UpdateJob(objectId, jobName, jobDescription);
    }

    public async Task DeleteJob(ObjectId objectId)
    {
        await _repository.DeleteJob(objectId);
    }
}