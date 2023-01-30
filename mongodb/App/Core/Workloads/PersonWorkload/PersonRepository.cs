using FamilyTreeMongoApp.Model.Person;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Runtime.CompilerServices;

namespace FamilyTreeMongoApp.Core.Workloads.PersonWorkload;

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

    public async Task<IEnumerable<Person>> GetPeopleBySex(string? sex)
    {
        var query = Query();
        if(sex != null) {
            query = query.Where(p=>p.Sex == sex);
        }
        return await query.ToListAsync();
    }

    public async Task<Person?> GetPersonById(ObjectId objectId)
    {
        return await Query().FirstOrDefaultAsync(c=>c.Id==objectId);
    }

    public async Task<IReadOnlyCollection<Person>> GetPeopleWithNoParents()
    {
        return await Query()
            .Where(c=>c.Mother == null && c.Father == null)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Person>> GetPeopleByParents(ObjectId? motherId, ObjectId? fatherId)
    {
        var query = Query();
        if(motherId != null) {
            query = query.Where(p=>p.Mother == motherId);
        }
        
        if(fatherId != null) {
            query = query.Where(p=>p.Father == fatherId);
        }
        
        return await query.ToListAsync();
    }

    public async Task<Person> UpdatePerson(
        ObjectId id,
        string firstname,
        string lastname,
        ObjectId? motherId,
        ObjectId? fatherId,
        string personSex,
        Location? birthplace,
        ObjectId? job,
        ObjectId? company)
    {
        var updateDef = UpdateDefBuilder
            .Set(p => p.Firstname, firstname)
            .Set(p => p.Lastname, lastname)
            .Set(p => p.Sex, personSex)
            .Set(p => p.Mother, motherId)
            .Set(p => p.Father, fatherId);

        if(birthplace is not null)
        {
            updateDef = updateDef.Set(p => p.BirthPlace, birthplace);
        }

        if(job is not null)
        {
            updateDef = updateDef.Set(p => p.Job, job);
        }

        if(company is not null) { 
            updateDef = updateDef.Set(p => p.Company, company);
        }

        await UpdateOneAsync(id, updateDef);
        
        return (await GetPersonById(id))!;
    }

    public async Task DeletePerson(ObjectId objectId)
    {
        await DeleteOneAsync(objectId);
    }
}