using AutoMapper;
using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using FamilyTreeMongoApp.Core.Workloads.Person;
using FamilyTreeMongoApp.Model.PersonDetails;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using Neo4j.Driver;

namespace FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;

public class AccomplishmentRepositoryNeo: RepositoryBase<Accomplishment>, IAccomplishmentRepository
{
    private readonly IMapper _mapper;
    private readonly IDriver _driver;

    public AccomplishmentRepositoryNeo(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
        IMapper mapper, IDriver driver) 
        : base(transactionProvider, databaseProvider)
    {
        _mapper = mapper;
        _driver = driver;
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Accomplishment>();
    public async Task<Accomplishment?> GetAccomplishmentById(ObjectId parse)
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (a:Accomplishment {id:$Id}) " + Neo4JUtil.accomplishmentReturnAllFieldsQuery + ";",
                new {Id = parse.ToString()});
            return await result.SingleAsync(Neo4JUtil.convertIRecordToAccomplishment);
        });
    }

    public async Task<Accomplishment?> AddAccomplishment(Accomplishment accomplishment)
    {
        // Generate ID for Neo4J
        var toAdd = _mapper.Map<AccomplishmentDto>(accomplishment);
        toAdd.Id = ObjectId.GenerateNewId().ToString();

        await using var session = _driver.AsyncSession();
        return await session.ExecuteWriteAsync(async tx =>
        {
            var result = await tx.RunAsync("MERGE (a:Accomplishment {" +
                                           "id: $Id," +
                                           "time: $Time," +
                                           "description: $Description" +
                                           "}) " + Neo4JUtil.accomplishmentReturnAllFieldsQuery +";", toAdd);
            return await result.SingleAsync(Neo4JUtil.convertIRecordToAccomplishment);
        });
    }

    public async Task<IEnumerable<Accomplishment>> GetAllAccomplishments()
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (a:Accomplishment) "
                                           + Neo4JUtil.accomplishmentReturnAllFieldsQuery + ";");
            return await result.ToListAsync(Neo4JUtil.convertIRecordToAccomplishment);
        });
    }

    public async Task<Accomplishment> UpdateAccomplishment(ObjectId objectId, string description, DateTime time)
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteWriteAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (a:Accomplishment {id:$Id}) SET " +
                                           "a.time = $time," +
                                           "a.description = $description " 
                                           + Neo4JUtil.accomplishmentReturnAllFieldsQuery + ";",
                new
                {
                    Id= objectId.ToString(),
                    time=time.ToString("dd.MM.yyyy"),
                    description
                });
            return await result.SingleAsync(Neo4JUtil.convertIRecordToAccomplishment);
        });
    }

    public async Task DeleteAccomplishment(ObjectId objectId)
    {
        await using var session = _driver.AsyncSession();
        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync("MATCH (a:Accomplishment {id: $Id}) DETACH DELETE a;",
                new {Id = objectId.ToString()});
        });
    }

    public async Task DeleteCollection()
    {
        await using var session = _driver.AsyncSession();
        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync("MATCH (a:Accomplishment) DETACH DELETE a;");
        });
    }
}