using AutoMapper;
using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Model.Statistics;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Neo4j.Driver;
using Neo4jClient;

namespace FamilyTreeMongoApp.Core.Workloads.PersonWorkload;

public class PersonRepositoryNeo : IPersonRepository
{
    private IDriver _driver;
    private ILogger<PersonRepository> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonRepository"/> class.
    /// </summary>
    public PersonRepositoryNeo(IDriver driver, ILogger<PersonRepository> logger, IMapper mapper)
    {
        _driver = driver;
        _logger = logger;
        _mapper = mapper;
    }

    public string CollectionName => "person";

    public async Task<Person> AddPerson(Person request)
    {
        // Generate ID for Neo4J
        var toAdd = _mapper.Map<PersonDto>(request);
        toAdd.Id = ObjectId.GenerateNewId().ToString();

        await using var session = _driver.AsyncSession();
        return await session.ExecuteWriteAsync(async tx =>
        {
            var result = await tx.RunAsync("MERGE (p:Person {" +
                                           "id: $Id," +
                                           "firstname: $Firstname," +
                                           "lastname: $Lastname," +
                                           "sex: $Sex" +
                                           ((toAdd.Job != null) ? ",jobId: $Job" : "") +
                                           ((toAdd.Company != null) ? ",companyId: $Company" : "") +
                                           ((toAdd.Mother != null) ? ",motherId: $Mother" : "") +
                                           ((toAdd.Father != null) ? ",fatherId: $Father" : "") +
                                           "}) " +
                                           Neo4JUtil.personReturnAllFieldsQuery + ";", toAdd);
            return await result.SingleAsync(Neo4JUtil.convertIRecordToPerson);
        });
    }

    public async Task DeletePerson(ObjectId objectId)
    {
        await using var session = _driver.AsyncSession();
        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync("MATCH (p:Person {id: $Id}) DETACH DELETE p;",
                new {Id = objectId.ToString()});
        });
    }

    public Task<int> GetAccomplishmentsCount(ObjectId objectId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Person>> GetDescendants(Person objectId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Person>> GetDescendantsInCompany(Person objectId, Company company)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<Person>> GetPeopleByParents(ObjectId? motherId, ObjectId? fatherId)
    {
        string filter = "";
        
        if (motherId is null && fatherId is null)
        {
            return await GetPeopleWithNoParents();
        }
        
        if (motherId is null)
        {
            filter = "{fatherId:$fatherId}";
        }
        else if (fatherId is null)
        {
            filter = "{motherId:$motherId}";
        }
        else
        {
            filter = "{motherId:$motherId,fatherId:$fatherId}";
        }
        
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (p:Person "+ filter +") " +
                                           Neo4JUtil.personReturnAllFieldsQuery + ";", 
                new {motherId=motherId.ToString(), fatherId=fatherId.ToString()});
            return await result.ToListAsync(Neo4JUtil.convertIRecordToPerson);
        });
    }

    public async Task<IEnumerable<Person>> GetPeopleBySex(string? sex)
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (p:Person) " + Neo4JUtil.personReturnAllFieldsQuery + ";");
            return await result.ToListAsync(Neo4JUtil.convertIRecordToPerson);
        });
    }

    public async Task<IReadOnlyCollection<Person>> GetPeopleWithNoParents()
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (p:Person) where p.motherId is null and p.fatherId is null " +
                                           Neo4JUtil.personReturnAllFieldsQuery + ";");
            return await result.ToListAsync(Neo4JUtil.convertIRecordToPerson);
        });
    }

    public async Task<Person?> GetPersonById(ObjectId objectId)
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (p:Person {id:$Id}) " + Neo4JUtil.personReturnAllFieldsQuery + ";",
                new {Id = objectId.ToString()});
            return await result.SingleAsync(Neo4JUtil.convertIRecordToPerson);
        });
    }

    public Task<Person> UpdatePerson(
        ObjectId id,
        string firstname,
        string lastname,
        ObjectId? motherId,
        ObjectId? fatherId,
        string personSex,
        Location? BirthPlace,
        ObjectId? Job,
        ObjectId? Company)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCollection()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Person>> GetAncestors(Person person)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<JobStatDto>> GetJobsStats()
    {
        throw new NotImplementedException();
    }
}