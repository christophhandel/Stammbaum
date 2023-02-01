using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using FamilyTreeMongoApp.Core.Workloads.JobWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Model.PersonDetails;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FamilyTreeMongoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;
        private readonly ICompanyService _companyService;
        private readonly IJobService _jobService;
        private readonly ITransactionProvider _transactionProvider;

        public TestDataController(
            ILogger<PersonController> logger,
            IMapper mapper,
            IPersonService personService,
            ICompanyService companyService,
            IJobService jobService,
            ITransactionProvider transactionProvider)
        {
            _logger = logger;
            _mapper = mapper;
            _personService = personService;
            _companyService = companyService;
            _jobService = jobService;
            _transactionProvider = transactionProvider;
        }




        // POST api/<TestDataController>
        [HttpGet]
        public async Task Get()
        {
            await _jobService.AddJob(new JobDto() { Name = "King", JobType = "The one who sits on the Iron-Throne" });
            await _jobService.AddJob(new JobDto() { Name = "King of the North", JobType = "The one who sits on the throne in winterfell" });
            await _jobService.AddJob(new JobDto() { Name = "Mother of the Dragons", JobType = "A person who raised dragons" });

            await _jobService.AddJob(new JobDto() { Name = "King", JobType = "The one who sits on the Iron-Throne" });
            await _jobService.AddJob(new JobDto() { Name = "King of the North", JobType = "The one who sits on the throne in winterfell" });
            await _jobService.AddJob(new JobDto() { Name = "Mother of the Dragons", JobType = "A person who raised dragons" });

            #region Test Data

            // HOUSE LANNISTER
            Person tywin = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Tywin",
                Lastname = "Lannister",
                BirthLocation = new LocationDto() { City = "Casterly", Country = "Rock" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person joanna = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Joanna",
                Lastname = "Lannister",
                BirthLocation = new LocationDto() { City = "Casterly", Country = "Rock" },
                Sex = "f",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person cersei = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Cersei",
                Lastname = "Lannister",
                BirthLocation = new LocationDto() { City = "Rock", Country = "Casterly" },
                Sex = "f",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person jaime = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Jaime",
                Lastname = "Lannister",
                BirthLocation = new LocationDto() { City = "Rock", Country = "Casterly" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person tyrion = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Tyrion",
                Lastname = "Lannister",
                BirthLocation = new LocationDto() { City = "Rock", Country = "Casterly" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person lancel = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Lancel",
                Lastname = "Lannister",
                BirthLocation = new LocationDto() { City = "Rock", Country = "Casterly" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });

            // HOUSE BARATHEON
            Person robert = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Robert",
                Lastname = "Baratheon",
                BirthLocation = new LocationDto() { City = "Storm's End", Country = "Westeros" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person joffrey = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Joffrey",
                Lastname = "Baratheon",
                BirthLocation = new LocationDto() { City = "Storm's End", Country = "Westeros" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person myrcella = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Myrcella",
                Lastname = "Baratheon",
                BirthLocation = new LocationDto() { City = "Storm's End", Country = "Westeros" },
                Sex = "f",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person tommen = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Tommen",
                Lastname = "Baratheon",
                BirthLocation = new LocationDto() { City = "Storm's End", Country = "Westeros" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });

            // HOUSE TARGARYEN
            Person rhaella = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Rhaella",
                Lastname = "Targaryen",
                BirthLocation = new LocationDto() { City = "Valyria", Country = "Essos" },
                Sex = "f",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person aerys = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Aerys 2.",
                Lastname = "Targaryen",
                BirthLocation = new LocationDto() { City = "Valyria", Country = "Essos" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person rhaegar = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Rhaegar",
                Lastname = "Targaryen",
                BirthLocation = new LocationDto() { City = "Valyria", Country = "Essos" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person viserys = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Viserys",
                Lastname = "Targaryen",
                BirthLocation = new LocationDto() { City = "Valyria", Country = "Essos" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person daenerys = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Daenerys",
                Lastname = "Targaryen",
                BirthLocation = new LocationDto() { City = "Valyria", Country = "Essos" },
                Sex = "f",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person drogon = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Drogon",
                Lastname = "Dragon",
                BirthLocation = new LocationDto() { City = "Valyria", Country = "Essos" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person rhaegal = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Rhaegal",
                Lastname = "Dragon",
                BirthLocation = new LocationDto() { City = "Valyria", Country = "Essos" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });

            // HOUSE STARK 
            Person lyanna = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Lyanna",
                Lastname = "Stark",
                BirthLocation = new LocationDto() { City = "Winterfell", Country = "The North" },
                Sex = "f",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person jon = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Jon",
                Lastname = "Snow",
                BirthLocation = new LocationDto() { City = "Winterfell", Country = "The North" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person sansa = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Sansa",
                Lastname = "Stark",
                BirthLocation = new LocationDto() { City = "Winterfell", Country = "The North" },
                Sex = "f",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person catelyn = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Catelyn",
                Lastname = "Stark",
                BirthLocation = new LocationDto() { City = "Winterfell", Country = "The North" },
                Sex = "f",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person ned = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Ned",
                Lastname = "Stark",
                BirthLocation = new LocationDto() { City = "Winterfell", Country = "The North" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person yara = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Yara",
                Lastname = "Stark",
                BirthLocation = new LocationDto() { City = "Winterfell", Country = "The North" },
                Sex = "f",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person rickard = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Rickard",
                Lastname = "Stark",
                BirthLocation = new LocationDto() { City = "Winterfell", Country = "The North" },
                Sex = "m",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });
            Person arya = await _personService.AddPerson(new PersonDto()
            {
                Firstname = "Arya",
                Lastname = "Stark",
                BirthLocation = new LocationDto() { City = "Winterfell", Country = "The North" },
                Sex = "f",
                Company = null,
                Father = null,
                Mother = null,
                Job = null
            });

            #endregion

            // ADDING RELATIONS 
            await _personService.UpdatePerson(cersei.Id, cersei.Firstname, cersei.Lastname, joanna.Id, tywin.Id, cersei.Sex, cersei.BirthPlace, null, null);
            await _personService.UpdatePerson(jaime.Id, jaime.Firstname, jaime.Lastname, joanna.Id, tywin.Id, jaime.Sex, jaime.BirthPlace, null, null);
            await _personService.UpdatePerson(tyrion.Id, tyrion.Firstname, tyrion.Lastname, joanna.Id, tywin.Id, tyrion.Sex, tyrion.BirthPlace, null, null);

            await _personService.UpdatePerson(joffrey.Id, joffrey.Firstname, joffrey.Lastname, cersei.Id, jaime.Id, joffrey.Sex, joffrey.BirthPlace, null, null);
            await _personService.UpdatePerson(myrcella.Id, myrcella.Firstname, myrcella.Lastname, cersei.Id, jaime.Id, myrcella.Sex, myrcella.BirthPlace, null, null);
            await _personService.UpdatePerson(tommen.Id, tommen.Firstname, tommen.Lastname, cersei.Id, robert.Id, tommen.Sex, tommen.BirthPlace, null, null);

            await _personService.UpdatePerson(rhaegar.Id, rhaegar.Firstname, rhaegar.Lastname, rhaella.Id, aerys.Id, rhaegar.Sex, rhaegar.BirthPlace, null, null);
            await _personService.UpdatePerson(viserys.Id, viserys.Firstname, viserys.Lastname, rhaella.Id, aerys.Id, viserys.Sex, viserys.BirthPlace, null, null);
            await _personService.UpdatePerson(daenerys.Id, daenerys.Firstname, daenerys.Lastname, rhaella.Id, aerys.Id, daenerys.Sex, daenerys.BirthPlace, null, null);

            await _personService.UpdatePerson(jon.Id, jon.Firstname, jon.Lastname, lyanna.Id, rhaegar.Id, jon.Sex, jon.BirthPlace, null, null);
            await _personService.UpdatePerson(drogon.Id, drogon.Firstname, drogon.Lastname, daenerys.Id, jon.Id, drogon.Sex, drogon.BirthPlace, null, null);
            await _personService.UpdatePerson(rhaegal.Id, rhaegal.Firstname, rhaegal.Lastname, daenerys.Id, jon.Id, rhaegal.Sex, rhaegal.BirthPlace, null, null);

            await _personService.UpdatePerson(lancel.Id, lancel.Firstname, lancel.Lastname, sansa.Id, tyrion.Id, lancel.Sex, lancel.BirthPlace, null, null);
            await _personService.UpdatePerson(sansa.Id, sansa.Firstname, sansa.Lastname, catelyn.Id, ned.Id, sansa.Sex, sansa.BirthPlace, null, null);
            await _personService.UpdatePerson(arya.Id, arya.Firstname, arya.Lastname, catelyn.Id, ned.Id, arya.Sex, arya.BirthPlace, null, null);

            await _personService.UpdatePerson(ned.Id, ned.Firstname, ned.Lastname, yara.Id, rickard.Id, ned.Sex, ned.BirthPlace, null, null);
            await _personService.UpdatePerson(lyanna.Id, lyanna.Firstname, lyanna.Lastname, yara.Id, rickard.Id, lyanna.Sex, lyanna.BirthPlace, null, null);

            await _companyService.AddCompany(new CompanyDto() { Name = "Golden Army", BusinessActivity = "Paid army which is said to be super strong but is actually super weak" });
            await _companyService.AddCompany(new CompanyDto() { Name = "White Walkers", BusinessActivity = "Zombie alike white dudes" });
            await _companyService.AddCompany(new CompanyDto() { Name = "Faceless Men", BusinessActivity = "Assassins who can change their face at will" });

            return;
        }
    }
}
