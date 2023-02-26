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
            await createRelations(tx, request.Id, request.Mother, request.Father, request.Job, request.Company);
            return await result.SingleAsync(Neo4JUtil.convertIRecordToPerson);
        });
    }

    private async Task createRelations(IAsyncQueryRunner tx,
        ObjectId id,
        ObjectId? motherId, 
        ObjectId? fatherId, 
        ObjectId? jobId, 
        ObjectId? companyId)
    {
        if (motherId is not null)
        {
            await tx.RunAsync("MATCH (p:Person),(m:Person) WHERE p.id=$id AND m.id=$motherId " +
                              "CREATE (m)-[:PARENT_OF]->(p);",
                new
                {
                    id= id.ToString(),motherId=motherId.ToString()
                });
        }
        if (fatherId is not null)
        {
            await tx.RunAsync("MATCH (p:Person),(f:Person) WHERE p.id=$id AND f.id=$fatherId " +
                              "CREATE (f)-[:PARENT_OF]->(p);",
                new
                {
                    id= id.ToString(),fatherId=fatherId.ToString()
                });
        }
        if (jobId is not null)
        {
            await tx.RunAsync("MATCH (p:Person),(j:Job) WHERE p.id=$id AND j.id=$jobId " +
                              "CREATE (p)-[:WORKS_AS]->(j);",
                new
                {
                    id= id.ToString(),jobId=jobId.ToString()
                });
        }

        if (companyId is not null)
        {
            await tx.RunAsync("MATCH (p:Person),(c:Company) WHERE p.id=$id AND c.id=$companyId " +
                              "CREATE (p)-[:WORKS_AT]->(c);",
                new
                {
                    id= id.ToString(),companyId=companyId.ToString()
                });
        }
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

    public async Task<int> GetAccomplishmentsCount(ObjectId objectId)
    {
        // TODO: ADD ACCOMPLISHMENT REPO
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (p:Person {id:$id})-[:ACCOMPLISHED]->(a:Accomplishment) RETURN a;"
                , new {id=objectId.ToString()});
            return (await result.ToListAsync(p => p["a"] != null)).Count;
        });
    }

    public async Task<IEnumerable<Person>> GetDescendants(Person objectId)
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (p:Person)-[:PARENT_OF *0..]->(act:Person {id:$id}) " 
                                           + Neo4JUtil.personReturnAllFieldsQuery + ";"
                , new {id=objectId.Id.ToString()});
            return await result.ToListAsync(Neo4JUtil.convertIRecordToPerson);
        });
    }

    public async Task<IEnumerable<Person>> GetDescendantsInCompany(Person objectId, Company company)
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (p:Person)-[:PARENT_OF *0..]->(act:Person {id:$id}) " +
                                           "WHERE p.companyId = $companyId " 
                                           + Neo4JUtil.personReturnAllFieldsQuery + ";"
                , new {id=objectId.Id.ToString(), companyId = company.Id.ToString()});
            return await result.ToListAsync(Neo4JUtil.convertIRecordToPerson);
        });
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
            var result = await tx.RunAsync("MATCH (p:Person" +
                                           (sex is null ? "" : " {sex:$sex}")
                                           + ") "
                                           + Neo4JUtil.personReturnAllFieldsQuery + ";", new {sex});
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

    public async Task<Person> UpdatePerson(
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
        await using var session = _driver.AsyncSession();
        return await session.ExecuteWriteAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (p:Person {id:$Id}) SET " +
                                           "p.firstname= $firstname," +
                                           "p.lastname= $lastname," +
                                           "p.sex= $sex" +
                                           ((motherId != null) ? ",p.motherId= $motherId" : "") +
                                           ((fatherId != null) ? ",p.fatherId= $fatherId" : "") + " "
                                           + Neo4JUtil.personReturnAllFieldsQuery + ";",
                new
                {
                    Id= id.ToString(),firstname, lastname, motherId = motherId.ToString(), fatherId = fatherId.ToString(),
                    sex = personSex, jobId=Job.ToString(), companyId =Company.ToString()
                });
            await createRelations(tx, id, motherId, fatherId, Job, Company);
            return await result.SingleAsync(Neo4JUtil.convertIRecordToPerson);
        });
    }

    public async Task DeleteCollection()
    {
        await using var session = _driver.AsyncSession();
        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync("MATCH (p:Person) DETACH DELETE p;");
        });
    }

    public async Task<IEnumerable<Person>> GetAncestors(Person person)
    {
        await using var session = _driver.AsyncSession();
        return await session.ExecuteReadAsync(async tx =>
        {
            var result = await tx.RunAsync("MATCH (act:Person {id:$id})-[:PARENT_OF *0..]->(p:Person) " 
                                           + Neo4JUtil.personReturnAllFieldsQuery + ";"
                                           , new {id=person.Id.ToString()});
            return await result.ToListAsync(Neo4JUtil.convertIRecordToPerson);
        });
    }

    public Task<IEnumerable<JobStatDto>> GetJobsStats()
    {
        // TODO: IMPLEMENT JOBS
        throw new NotImplementedException();
    }
}