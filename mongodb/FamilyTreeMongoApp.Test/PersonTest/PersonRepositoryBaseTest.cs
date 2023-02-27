using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FluentAssertions;
using Xunit;

namespace FamilyTreeMongoApp.Test.PersonTest;

public abstract class PersonRepositoryBaseTest
{
    private readonly IPersonRepository _personRepository;

    protected PersonRepositoryBaseTest(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
        [Fact]
    public async Task TestCreate()
    {
       Person p = await _personRepository.AddPerson(new Person()
        {
            Firstname = "Bob",
            Lastname = "Marley",
            Sex = "m"
        });

       Person? testP = await _personRepository.GetPersonById(p.Id);

       testP.Should().NotBe(null);
       testP.Firstname.Should().Be("Bob");
       testP.Lastname.Should().Be("Marley");

       await _personRepository.DeletePerson(testP.Id);
       
       
       testP = await _personRepository.GetPersonById(p.Id);

       testP.Should().Be(null);
    }

    [Fact]
    public async Task TestGetPeopleBySex()
    {
        int cntMale = (await _personRepository.GetPeopleBySex("m")).Count();

        Person male = await _personRepository.AddPerson(new Person()
        {
            Firstname = "male",
            Lastname = "person",
            Sex = "m"
        });

        (await _personRepository.GetPeopleBySex("m")).Count().Should().Be(cntMale+1);

    }

    [Fact]
    public async Task TestGetByParents()
    {
        Person father = await _personRepository.AddPerson(new Person()
        {
            Firstname = "parent",
            Lastname = "father",
            Sex = "m"
        });

        Person mother = await _personRepository.AddPerson(new Person()
        {
            Firstname = "parent",
            Lastname = "mother",
            Sex = "m"
        });

        Person childFather = await _personRepository.AddPerson(new Person()
        {
            Firstname = "child",
            Lastname = "father",
            Father = father.Id,
            Sex = "m"
        });

        Person childBoth = await _personRepository.AddPerson(new Person()
        {
            Firstname = "child",
            Lastname = "both",
            Mother = mother.Id,
            Father = father.Id,
            Sex = "m"
        });

        IReadOnlyCollection<Person> childsFather = await _personRepository.GetPeopleByParents(null, father.Id);

        childsFather.Count().Should().Be(2);

        IReadOnlyCollection<Person> childsBoth = await _personRepository.GetPeopleByParents(mother.Id, father.Id);

        childsBoth.Count().Should().Be(1);
    }


    [Fact]
    public async Task TestUpdatePerson()
    {
        Person personToUpdate = await _personRepository.AddPerson(new Person()
        {
            Firstname = "initial",
            Lastname = "name",
            Sex = "m",
            BirthDate = DateTime.Now,
        });


        personToUpdate.Firstname = "updated";

        await _personRepository.UpdatePerson(personToUpdate.Id, personToUpdate.Firstname, personToUpdate.Lastname, null, null, personToUpdate.Sex, null, null, null);


        Person? updatedPerson = await _personRepository.GetPersonById(personToUpdate.Id);

        updatedPerson.Should().NotBeNull();
        updatedPerson!.Firstname.Should().Be(personToUpdate.Firstname);

    }


    [Fact]
    public async Task TestDeletePerson()
    {
        Person personToDelete = await _personRepository.AddPerson(new Person()
        {
            Firstname = "delete",
            Lastname = "person",
            Sex = "m"
        });


        await _personRepository.DeletePerson(personToDelete.Id);

        Person? personToUpdate = await _personRepository.GetPersonById(personToDelete.Id);

        personToUpdate.Should().BeNull();
    }
}