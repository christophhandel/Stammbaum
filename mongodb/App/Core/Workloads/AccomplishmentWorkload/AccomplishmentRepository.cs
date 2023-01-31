using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using FamilyTreeMongoApp.Core.Workloads.Person;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Runtime.CompilerServices;

namespace FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;

public sealed class AccomplishmentRepository : RepositoryBase<Accomplishment>, IAccomplishmentRepository
{
    public AccomplishmentRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : 
        base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Accomplishment>();

    public async Task<Accomplishment?> AddAccomplishment(Accomplishment accomplishment)
    {
        return await InsertOneAsync(accomplishment);
    }

    public async Task DeleteAccomplishment(ObjectId objectId)
    {
        await DeleteOneAsync(objectId);
    }

    public async Task<Accomplishment?> GetAccomplishmentById(ObjectId parse)
    {
        return await Query().FirstOrDefaultAsync(p=>p.Id == parse);
    }

    public async Task<IEnumerable<Accomplishment>> GetAllAccomplishments()
    {
        return await Query().OrderBy(s => s.Time).ToListAsync();
    }

    public async Task<Accomplishment> UpdateAccomplishment(ObjectId objectId, string description, DateTime time)
    {
        var updateDef = UpdateDefBuilder
                        .Set(p => p.Description, description)
                        .Set(p => p.Time, time);

        await UpdateOneAsync(objectId, updateDef);

        return (await GetAccomplishmentById(objectId))!;
    }
}