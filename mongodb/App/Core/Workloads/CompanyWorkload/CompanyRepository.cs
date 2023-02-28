using FamilyTreeMongoApp.Model.PersonDetails;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Runtime.CompilerServices;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.Statistics;


namespace FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;

public sealed class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    private IPersonRepository _personRepository;

    public CompanyRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
        IPersonRepository personRepository) :
        base(transactionProvider, databaseProvider)
    {
        _personRepository = personRepository;
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Company>();
    public async Task<Company?> GetCompanyById(ObjectId parse)
    {
        return await Query().FirstOrDefaultAsync(c => c.Id == parse);

    }

    public async Task<Company> AddCompany(Company company)
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

    public async Task<IEnumerable<CompanyStatDto>> GetCompanyStats()
    {
         return (await QueryIncludeDetail<PersonWorkload.Person, String, String>(_personRepository,
                            p => p.Company!.Value,
                            (p, l) => new MasterDetails<String, String>()
                            {
                                Master = p.Name,
                                Details = l.Select(pers => pers.Sex)
                            })
                        .ToListAsync())
                    .Select(mD => new CompanyStatDto(
                        mD.Master,
                        mD.Details == null ? 0 : mD.Details!.Count(p => p == "f"),
                        mD.Details == null ? 0 : mD.Details!.Count(p => p == "m")));
    }
}