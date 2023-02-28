using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using FamilyTreeMongoApp.Core.Workloads.JobWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.Statistics;
using FluentAssertions;
using Xunit;

namespace FamilyTreeMongoApp.Test.CompanyTest;

public abstract class CompanyRepositoryBaseTest : IDisposable
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IPersonRepository _personRepository;


    protected CompanyRepositoryBaseTest(ICompanyRepository companyRepository, IPersonRepository personRepository)
    {
        _companyRepository = companyRepository;
        _personRepository = personRepository;
    }
    
    [Fact]
    public async Task TestJobStats()
    {
        Company firstCompany = await _companyRepository.AddCompany(new Company()
        {
            BusinessActivity= "Pfuschn",
            Name = "Aichinger Co."
        });
        Company secondCompany = await _companyRepository.AddCompany(new Company()
        {
            BusinessActivity = "Informatics",
            Name = "Darius Co."
        });
        Person p1 = await _personRepository.AddPerson(new Person()
        {
            Firstname = "Foo",
            Lastname = "Bar",
            Sex = "m",
            Company = firstCompany.Id
        });
        Person p2 = await _personRepository.AddPerson(new Person()
        {
            Firstname = "Baz",
            Lastname = "Bar",
            Sex = "f",
            Company = secondCompany.Id
        });
        Person p3 = await _personRepository.AddPerson(new Person()
        {
            Firstname = "Bert",
            Lastname = "Bar",
            Sex = "m",
            Company = firstCompany.Id
        });

        var stats =  await _companyRepository.GetCompanyStats();

        stats.Count().Should().Be(2);
        stats.Should().Contain(new CompanyStatDto("Aichinger Co.", 0, 2));
        stats.Should().Contain(new CompanyStatDto("Darius Co.", 1, 0));

        await _companyRepository.DeleteCompany(firstCompany.Id);
        await _companyRepository.DeleteCompany(secondCompany.Id);
        await _personRepository.DeletePerson(p1.Id);
        await _personRepository.DeletePerson(p2.Id);
        await _personRepository.DeletePerson(p3.Id);
    }
    
    [Fact]
    public async Task TestJobStatsExistingCompanyWithNoEmpl()
    {
        Company firstCompany = await _companyRepository.AddCompany(new Company()
        {
            BusinessActivity= "Pfuschn",
            Name = "Aichinger Co."
        });
        Company secondCompany = await _companyRepository.AddCompany(new Company()
        {
            BusinessActivity = "Informatics",
            Name = "Darius Co."
        });

        var stats =  await _companyRepository.GetCompanyStats();

        stats.Count().Should().Be(0);

        await _companyRepository.DeleteCompany(firstCompany.Id);
        await _companyRepository.DeleteCompany(secondCompany.Id);
    }


    public void Dispose()
    {
        
    }
}