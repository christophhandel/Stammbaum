using System.Runtime.InteropServices.JavaScript;
using AutoMapper.QueryableExtensions;
using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using FamilyTreeMongoApp.Core.Workloads.JobWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.PersonDetails;
using FamilyTreeMongoApp.Model.Statistics;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FamilyTreeMongoApp.Core.Workloads.JobWorkload;

public sealed class JobRepository : RepositoryBase<Job>, IJobRepository
{
    private IPersonRepository _personRepository;
    
    public JobRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
        IPersonRepository personRepository) :
        base(transactionProvider, databaseProvider)
    {
        _personRepository = personRepository;
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Job>();
    public async Task<Job?> GetJobById(ObjectId parse)
    {
        return await Query().FirstOrDefaultAsync(c=>c.Id==parse);
    }

    public async Task<Job> AddJob(Job request)
    {
        return await InsertOneAsync(request);
    }

    public async Task<IEnumerable<Job>> GetAllJobs()
    {
        return await Query().OrderBy(s => s.Name).ToListAsync();
    }

    public async Task<Job> UpdateJob(ObjectId objectId, string jobName, string jobDescription)
    {
        var updateDef = UpdateDefBuilder
            .Set(p => p.Name, jobName)
            .Set(p => p.JobType, jobDescription);
        
        await UpdateOneAsync(objectId, updateDef);
        
        return (await GetJobById(objectId))!;
    }

    public async Task DeleteJob(ObjectId objectId)
    {
        await DeleteOneAsync(objectId);
    }

    public async Task DeleteCollection()
    {
        await DeleteManyAsync(j => 1 == 1);
    }

    public async Task<IEnumerable<JobStatDto>> GetJobsStats()
    {
        return (await QueryIncludeDetail<PersonWorkload.Person, String, String>(_personRepository,
                    p => p.Job!.Value,
                    (p, l) => new MasterDetails<String, String>()
                    {
                        Master = p.Name,
                        Details = l.Select(pers => pers.Sex)
                    })
                .ToListAsync())
            .Select(mD => new JobStatDto(
                mD.Master,
                mD.Details == null ? 0 : mD.Details!.Count(p => p == "f"),
                mD.Details == null ? 0 : mD.Details!.Count(p => p == "m")));
    }
}