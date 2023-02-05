using AutoMapper;
using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Model.PersonDetails;
using FamilyTreeMongoApp.Model.Statistics;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;

public sealed class CompanyService : ICompanyService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;
    

    public CompanyService(IDateTimeProvider dateTimeProvider, ICompanyRepository repository, IMapper mapper)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Company?> GetCompanyById(ObjectId parse)
    {
        return await _repository.GetCompanyById(parse);
    }

    public async Task<Company?> AddCompany(CompanyDto request)
    {
        return await _repository.AddCompany(new Company
        {
            Name = request.Name,
            BusinessActivity = request.BusinessActivity
        });
    }

    public async Task<IEnumerable<Company>> GetAllCompanies()
    {
        return await _repository.GetAllCompanies();
    }

    public async Task<Company> UpdateCompany(ObjectId objectId, string companyName, string companyBusinessActivity)
    {
        return await _repository.UpdateCompany(objectId,companyName,companyBusinessActivity);
    }

    public async Task DeleteCompany(ObjectId objectId)
    {
        await _repository.DeleteCompany(objectId);
    }

    public async Task DeleteCollection()
    {
        await _repository.DeleteColletion();
    }

    public async Task<IEnumerable<CompanyStatDto>> GetCompanyStats()
    {
        return await _repository.GetCompanyStats();
    }
}