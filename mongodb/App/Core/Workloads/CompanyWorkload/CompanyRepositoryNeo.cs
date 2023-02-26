using AutoMapper;
using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Model.PersonDetails;
using FamilyTreeMongoApp.Model.Statistics;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using Neo4j.Driver;

namespace FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;

public class CompanyRepositoryNeo : RepositoryBase<Company>, ICompanyRepository
{
    private readonly IMapper _mapper;
    private readonly IDriver _driver;

    public CompanyRepositoryNeo(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
        IMapper mapper, IDriver driver) 
        : base(transactionProvider, databaseProvider)
    {
        this._mapper = mapper;
        _driver = driver;
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Company>();
    
    public async Task<Company?> GetCompanyById(ObjectId parse)
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (c:Company {id:$Id}) " + Neo4JUtil.companyReturnAllFieldsQuery + ";",
                new {Id = parse.ToString()});
            return await result.SingleAsync(Neo4JUtil.convertIRecordToCompany);
        });
    }

    public async Task<Company?> AddCompany(Company company)
    {
        // Generate ID for Neo4J
        var toAdd = _mapper.Map<CompanyDto>(company);
        toAdd.Id = ObjectId.GenerateNewId().ToString();

        await using var session = _driver.AsyncSession();
        return await session.ExecuteWriteAsync(async tx =>
        {
            var result = await tx.RunAsync("MERGE (c:Company {" +
                                           "id: $Id," +
                                           "name: $Name," +
                                           "businessActivity: $BusinessActivity" +
                                           "}) " + Neo4JUtil.companyReturnAllFieldsQuery +";", toAdd);
            return await result.SingleAsync(Neo4JUtil.convertIRecordToCompany);
        });
    }

    public async Task<IEnumerable<Company>> GetAllCompanies()
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (c:Company) "
                                           + Neo4JUtil.companyReturnAllFieldsQuery + ";");
            return await result.ToListAsync(Neo4JUtil.convertIRecordToCompany);
        });
    }

    public async Task<Company> UpdateCompany(ObjectId objectId, string companyName, string companyBusinessActivity)
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteWriteAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (c:Company {id:$Id}) SET " +
                                           "c.name= $name," +
                                           "c.businessActivity = $businessActivity " 
                                           + Neo4JUtil.companyReturnAllFieldsQuery + ";",
                new
                {
                    Id= objectId.ToString(),
                    name = companyName,
                    businessActivity = companyBusinessActivity
                });
            return await result.SingleAsync(Neo4JUtil.convertIRecordToCompany);
        });
    }

    public async Task DeleteCompany(ObjectId objectId)
    {
        await using var session = _driver.AsyncSession();
        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync("MATCH (c:Company {id: $Id}) DETACH DELETE c;",
                new {Id = objectId.ToString()});
        });
    }

    public async Task DeleteColletion()
    {
        await using var session = _driver.AsyncSession();
        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync("MATCH (c:Company) DETACH DELETE j;");
        });
    }

    public Task<IEnumerable<CompanyStatDto>> GetCompanyStats()
    {
        throw new NotImplementedException();
    }
}