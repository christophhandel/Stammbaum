using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;

namespace FamilyTreeMongoApp.Core.Workloads.Person;

public sealed class PersonRepository : RepositoryBase<Person>, IPersonRepository
{
    public PersonRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : 
        base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Person>();
}