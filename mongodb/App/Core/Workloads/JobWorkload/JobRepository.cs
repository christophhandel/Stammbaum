using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using FamilyTreeMongoApp.Core.Workloads.JobWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.PersonDetails;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FamilyTreeMongoApp.Core.Workloads.JobWorkload;

public sealed class JobRepository : RepositoryBase<Job>, IJobRepository
{
    public JobRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) :
        base(transactionProvider, databaseProvider)
    {
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
}