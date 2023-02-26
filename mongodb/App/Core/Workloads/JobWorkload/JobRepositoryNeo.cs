using AutoMapper;
using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Model.PersonDetails;
using FamilyTreeMongoApp.Model.Statistics;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using Neo4j.Driver;

namespace FamilyTreeMongoApp.Core.Workloads.JobWorkload;

public class JobRepositoryNeo: RepositoryBase<Job>, IJobRepository
{
    private readonly IDriver _driver;
    private readonly IMapper _mapper;

    public JobRepositoryNeo(IDriver driver, ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
        IMapper _mapper) 
        : base(transactionProvider, databaseProvider)
    {
        _driver = driver;
        this._mapper = _mapper;
    }
    
    public override string CollectionName { get; }  = MongoUtil.GetCollectionName<Job>();
    
    public async Task<Job?> GetJobById(ObjectId parse)
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (j:Job {id:$Id}) " + Neo4JUtil.jobReturnAllFieldsQuery + ";",
                new {Id = parse.ToString()});
            return await result.SingleAsync(Neo4JUtil.convertIRecordToJob);
        });
    }

    public async Task<Job> AddJob(Job request)
    {
        // Generate ID for Neo4J
        var toAdd = _mapper.Map<JobDto>(request);
        toAdd.Id = ObjectId.GenerateNewId().ToString();

        await using var session = _driver.AsyncSession();
        return await session.ExecuteWriteAsync(async tx =>
        {
            var result = await tx.RunAsync("MERGE (j:Job {" +
                                           "id: $Id," +
                                           "name: $Name," +
                                           "jobType: $JobType" +
                                           "}) " + Neo4JUtil.jobReturnAllFieldsQuery +";", toAdd);
            return await result.SingleAsync(Neo4JUtil.convertIRecordToJob);
        });
    }

    public async Task<IEnumerable<Job>> GetAllJobs()
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (j:Job) "
                                           + Neo4JUtil.jobReturnAllFieldsQuery + ";");
            return await result.ToListAsync(Neo4JUtil.convertIRecordToJob);
        });
    }

    public async Task<Job> UpdateJob(ObjectId objectId, string jobName, string jobDescription)
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteWriteAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (j:Job {id:$Id}) SET " +
                                           "j.name= $name," +
                                           "j.jobType = $jobType " 
                                           + Neo4JUtil.jobReturnAllFieldsQuery + ";",
                new
                {
                    Id= objectId.ToString(),
                    name = jobName,
                    jobType=jobDescription
                });
            return await result.SingleAsync(Neo4JUtil.convertIRecordToJob);
        });
    }

    public async Task DeleteJob(ObjectId objectId)
    {
        await using var session = _driver.AsyncSession();
        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync("MATCH (j:Job {id: $Id}) DETACH DELETE j;",
                new {Id = objectId.ToString()});
        });
    }

    public async Task DeleteCollection()
    {
        await using var session = _driver.AsyncSession();
        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync("MATCH (j:Job) DETACH DELETE j;");
        });
    }

    public Task<IEnumerable<JobStatDto>> GetJobsStats()
    {
        throw new NotImplementedException();
    }
}