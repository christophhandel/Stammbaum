using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;

namespace FamilyTreeMongoApp.Core.Workloads.FamilyTree;

public sealed class FamilyTreeRepository : RepositoryBase<FamilyTree>, IFamilyTreeRepository
{
    public FamilyTreeRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : 
        base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<FamilyTree>();
}