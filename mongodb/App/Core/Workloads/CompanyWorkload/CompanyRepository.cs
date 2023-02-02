using FamilyTreeMongoApp.Model.PersonDetails;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Runtime.CompilerServices;


namespace FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;

public sealed class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) :
        base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Company>();
    public async Task<Company?> GetCompanyById(ObjectId parse)
    {
        return await Query().FirstOrDefaultAsync(c => c.Id == parse);

    }

    public async Task<Company?> AddCompany(Company company)
    {
        return await InsertOneAsync(company);
    }

    public async Task<IEnumerable<Company>> GetAllCompanies()
    {
        return await Query().OrderBy(s => s.BusinessActivity).ToListAsync();
    }

    public async Task<Company> UpdateCompany(ObjectId objectId, string companyName, string companyBusinessActivity)
    {
        var updateDef = UpdateDefBuilder
            .Set(p => p.Name, companyName)
            .Set(p => p.BusinessActivity, companyBusinessActivity);

        await UpdateOneAsync(objectId, updateDef);

        return (await GetCompanyById(objectId))!;
    }

    public async Task DeleteCompany(ObjectId objectId)
    {
        await DeleteOneAsync(objectId);
    }

    public async Task DeleteColletion()
    {
        await DeleteManyAsync(c => 1 == 1);
    }
}