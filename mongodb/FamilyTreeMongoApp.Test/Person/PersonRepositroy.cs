
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

    [Fact]
    public async Task testGetPeopleBySex()
    {
        int cntMale = (await _personRepository.GetPeopleBySex("m")).Count();

        Core.Workloads.Person.Person male = await _personRepository.AddPerson(new Core.Workloads.Person.Person()
        {
            Firstname = "male",
            Lastname = "person",
            Sex = "m"
        });

        (await _personRepository.GetPeopleBySex("m")).Count().Should().Be(cntMale+1);

    }

    [Fact]
    public async Task testGetByParents()
    {
        Core.Workloads.Person.Person father = await _personRepository.AddPerson(new Core.Workloads.Person.Person()
        {
            Firstname = "parent",
            Lastname = "father"
        });

        Core.Workloads.Person.Person mother = await _personRepository.AddPerson(new Core.Workloads.Person.Person()
        {
            Firstname = "parent",
            Lastname = "mother"
        });

        Core.Workloads.Person.Person childFather = await _personRepository.AddPerson(new Core.Workloads.Person.Person()
        {
            Firstname = "child",
            Lastname = "father",
            Father = father.Id
        });

        Core.Workloads.Person.Person childBoth = await _personRepository.AddPerson(new Core.Workloads.Person.Person()
        {
            Firstname = "child",
            Lastname = "both",
            Mother = mother.Id,
            Father = father.Id
        });

        IReadOnlyCollection<Core.Workloads.Person.Person> childsFather = await _personRepository.GetPeopleByParents(null, father.Id);

        childsFather.Count().Should().Be(2);

        IReadOnlyCollection<Core.Workloads.Person.Person> childsBoth = await _personRepository.GetPeopleByParents(mother.Id, father.Id);

        childsBoth.Count().Should().Be(1);


    }


    [Fact]
    public async Task testUpdatePerson()
    {
        Core.Workloads.Person.Person personToUpdate = await _personRepository.AddPerson(new Core.Workloads.Person.Person()
        {
            Firstname = "initial",
            Lastname = "name",
            Sex = "m",
            BirthDate = DateTime.Now,
        });


        personToUpdate.Firstname = "updated";

        await _personRepository.UpdatePerson(personToUpdate.Id, personToUpdate.Firstname, personToUpdate.Lastname, null, null, personToUpdate.Sex, null, null, null);


        Core.Workloads.Person.Person? updatedPerson = await _personRepository.GetPersonById(personToUpdate.Id);

        updatedPerson.Should().NotBeNull();
        updatedPerson!.Firstname.Should().Be(personToUpdate.Firstname);

    }


    [Fact]
    public async Task testDeletePerson()
    {
        Core.Workloads.Person.Person personToDelete = await _personRepository.AddPerson(new Core.Workloads.Person.Person()
        {
            Firstname = "delete",
            Lastname = "person",
        });


        await _personRepository.DeletePerson(personToDelete.Id);

        Core.Workloads.Person.Person? personToUpdate = await _personRepository.GetPersonById(personToDelete.Id);

        personToUpdate.Should().BeNull();
    }
}