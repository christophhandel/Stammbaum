using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Model.Statistics;
using LeoMongo.Database;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;

public interface ICompanyRepository : IRepositoryBase
{
    Task<Company?> GetCompanyById(ObjectId parse);
    Task<Company> AddCompany(Company company);
    Task<IEnumerable<Company>> GetAllCompanies();
    Task<Company> UpdateCompany(ObjectId objectId, string companyName, string companyBusinessActivity);
    Task DeleteCompany(ObjectId objectId);
    Task DeleteColletion();
    Task<IEnumerable<CompanyStatDto>> GetCompanyStats();
}
