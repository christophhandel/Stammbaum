using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Model.PersonDetails;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;

public interface ICompanyService
{
    Task<Company?> GetCompanyById(ObjectId parse);
    Task<Company?> AddCompany(CompanyDto request);
    Task<IEnumerable<Company>> GetAllCompanies();
    Task<Company> UpdateCompany(ObjectId objectId, string companyName, string companyBusinessActivity);
    Task DeleteCompany(ObjectId objectId);
}
