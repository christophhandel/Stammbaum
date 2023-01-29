using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Model.Person;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Neo4j.Driver;
using Neo4jClient;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public class PersonRepositoryNeo : IPersonRepository
{
    private IDriver _driver;
    private ILogger<PersonRepository> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonRepository"/> class.
    /// </summary>
    public PersonRepositoryNeo(IDriver driver, ILogger<PersonRepository> logger)
    {
        _driver = driver;
        _logger = logger;
    }

    public string CollectionName => "person";

    public Task<Person> AddPerson(Person request)
    {
        throw new NotImplementedException();
    }

    public Task DeletePerson(ObjectId objectId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Person>> GetPeopleByParents(ObjectId? motherId, ObjectId? fatherId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Person>> GetPeopleBySex(string? sex)
    {
        using (var session = _driver.AsyncSession())
        {
            return await session.ExecuteReadAsync(async tx => {
                var result = await tx.RunAsync("MATCH (p:Person) RETURN p.firstname AS firstname, p.lastname AS lastname, p.sex AS sex;");
                return await result.ToListAsync(p=> {
                    return new Person() {
                    Firstname = p["firstname"].As<string>(), 
                    Lastname = p["lastname"].As<string>(), 
                    Sex = p["sex"].As<string>(), 
                };});
            });
        }
    }

    public Task<IReadOnlyCollection<Person>> GetPeopleWithNoParents()
    {
        throw new NotImplementedException();
    }

    public Task<Person?> GetPersonById(ObjectId objectId)
    {
        throw new NotImplementedException();
    }

    public Task<Person> UpdatePerson(ObjectId id, string firstname, string lastname, ObjectId? motherId, ObjectId? fatherId, string personSex)
    {
        throw new NotImplementedException();
    }
}