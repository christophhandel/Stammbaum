using FamilyTreeMongoApp.Model.Person;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Runtime.CompilerServices;
using FamilyTreeMongoApp.Core.Workloads.Person;

namespace FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;

public sealed class AccomplishmentRepository : RepositoryBase<Accomplishment>, IAccomplishmentRepository
{
    public AccomplishmentRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : 
        base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Accomplishment>();
}