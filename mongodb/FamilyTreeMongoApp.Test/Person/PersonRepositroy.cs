
using FamilyTreeMongoApp.Core.Workloads.Person;
using FluentAssertions;
using Xunit;

namespace FamilyTreeMongoApp.Test.Person;

public class PersonRepositroy
{
    private readonly IPersonRepository _personRepository;

    public PersonRepositroy(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    [Fact]
    public async Task testCreate()
    {
       Core.Workloads.Person.Person p = await _personRepository.AddPerson(new Core.Workloads.Person.Person()
        {
            Firstname = "Bob",
            Lastname = "Marley"
        });

       Core.Workloads.Person.Person? testP = await _personRepository.GetPersonById(p.Id);

       testP.Should().NotBe(null);
       testP.Firstname.Should().Be("Bob");
       testP.Lastname.Should().Be("Marley");

       await _personRepository.DeletePerson(testP.Id);
       
       testP = await _personRepository.GetPersonById(p.Id);

       testP.Should().Be(null);
    }
}