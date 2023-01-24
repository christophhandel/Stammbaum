using FamilyTreeMongoApp.Model.Person;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public sealed class PersonRepository : RepositoryBase<Person>, IPersonRepository
{
    public PersonRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : 
        base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Person>();

    public async Task<Person> AddPerson(Person request)
    {
        return await InsertOneAsync(request);
    }

    public async Task<IReadOnlyCollection<Person>> GetPeopleBySex(string? sex)
    {
        var query = Query();
        if(sex != null) {
            query = query.Where(p=>p.Sex == sex);
        }
        return await query.ToListAsync();
    }

    public async Task<Person> GetPersonById(ObjectId objectId)
    {
        return await Query().FirstOrDefaultAsync(c=>c.Id==objectId);
    }
}