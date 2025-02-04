﻿using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly IMapper _mapper;
    private readonly IPersonService _personService;
    private readonly ICompanyService _companyService;
    private readonly ITransactionProvider _transactionProvider;

    public PersonController(IMapper mapper, 
        IPersonService service,
        ICompanyService companyService,
        ITransactionProvider transactionProvider, 
        ILogger<PersonController> logger)
    {
        _mapper = mapper;
        _transactionProvider = transactionProvider;
        _personService = service;
        _companyService = companyService;
        _logger = logger;
    }

    /// <summary>
    ///     Creates a new Person.
    /// </summary>
    /// <param name="request">Data for the new comment</param>
    /// <returns>the created comment if successful</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PersonDto request)
    {
                // for a real app it would be a good idea to configure model validation to remove long ifs like this
        if (string.IsNullOrWhiteSpace(request.Firstname)
            || string.IsNullOrWhiteSpace(request.Lastname))
        {
            return BadRequest();
        }

        //using var transaction = await _transactionProvider.BeginTransaction();
        //check if dto contains parents ids
        //      - if yes: check if ids are correct
        //      - if no: one (or more) parents are unknown 
        if(!string.IsNullOrWhiteSpace(request.Father)) {
            var father = await _personService.GetPersonById(MongoDB.Bson.ObjectId.Parse(request.Father));
            if(father == null) {
                return BadRequest();
            }
        }

        if(!string.IsNullOrWhiteSpace(request.Mother)) {
            var mother = await _personService.GetPersonById(MongoDB.Bson.ObjectId.Parse(request.Mother));
            if(mother == null) {
                return BadRequest();
            }
        }
        
        var person = await _personService.AddPerson(request);
        //await transaction.CommitAsync();
        return new OkResult();
        //return CreatedAtAction(nameof(Person), new {id = person.Id.ToString()}, person);
    }
    
    /// <summary>
    ///     Returns all people
    /// </summary>
    /// <param name="sex">filter by sex</param>
    /// <returns>List of comments, may be empty</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPeopleBySex(string? sex)
    {
        IEnumerable<Person> people = await _personService.GetPeopleBySex(sex);
        return Ok(_mapper.Map<List<PersonDto>>(people));
    }

    /// <summary>
    ///     Returns person with id
    /// </summary>
    /// <param name="personId">ID of person</param>
    /// <returns>Get by ID</returns>
    [HttpGet]
    [Route("{personId}")]
    public async Task<ActionResult<IReadOnlyCollection<PersonDto>>> GetPersonById(string personId)
    {
        Person? person = await _personService.GetPersonById(new ObjectId(personId));
        
        if(person is null)
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map<PersonDto>(person));
    }

    /// <summary>
    ///     Get by parent ID
    ///
    /// </summary>
    /// <param name="motherId">ID of first Parent</param>
    /// <param name="fatherId">ID of second Parent</param>
    /// <returns>Get by ID</returns>
    [HttpGet]
    [Route("/parents")]
    public async Task<ActionResult<IReadOnlyCollection<PersonDto>>> GetPeopleByParentId(string? motherId, string? fatherId)
    {
        IReadOnlyCollection<Person> person = await _personService.GetPeopleByParents(
            motherId == null ? null : new ObjectId(motherId), 
            fatherId == null ? null : new ObjectId(fatherId)
            );
        return Ok(_mapper.Map<IReadOnlyCollection<PersonDto>>(person));
    }
    
    /// <summary>
    ///     Update Person
    ///
    /// </summary>
    /// <param name="personId">ID of Person to update</param>
    /// <param name="person">New person</param>
    /// <returns>Get by ID</returns>
    [HttpPut]
    [Route("{personId}")]
    public async Task<ActionResult<IReadOnlyCollection<PersonDto>>> UpdatePerson(string personId, PersonDto person)
    {

        Person? p = await _personService.GetPersonById(new ObjectId(personId));
        if(p is null)
        {
            return NotFound();
        }

        Person newPerson = await _personService.UpdatePerson(new ObjectId(personId),
            person.Firstname,
            person.Lastname,
            person.Mother == null ? null : new ObjectId(person.Mother),
            person.Father == null ? null : new ObjectId(person.Father),
            person.Sex,
            person.BirthLocation == null ? null : _mapper.Map<Location>(person.BirthLocation),
            person.Job == null ? null : new ObjectId(person.Job),
            person.Company == null ? null : new ObjectId(person.Company)
        );
        
        return Ok(_mapper.Map<PersonDto>(newPerson));
    }
    
    /// <summary>
    ///     Delete Person
    ///
    /// </summary>
    /// <param name="personId">ID of Person to delete</param>
    /// <returns>Get by ID</returns>
    [HttpDelete]
    [Route("{personId}")]
    public async Task<ActionResult<IReadOnlyCollection<PersonDto>>> DeletePerson(string personId)
    {
        Person? p = await _personService.GetPersonById(new ObjectId(personId));
        if(p is null)
        {
            return NotFound();
        }

        await _personService.DeletePerson(new ObjectId(personId));
        
        return Ok(_mapper.Map<PersonDto>(p));
    }
    
    /// <summary>
    ///    Return descendants who worked in same company
    ///
    /// </summary>
    /// <param name="personId">ID of Person to delete</param>
    /// <returns>Get by ID</returns>
    [HttpGet]
    [Route("descendants-in-same-company/{personId}")]
    public async Task<ActionResult<IReadOnlyCollection<PersonDto>>> GetDescendantsInSameCompany(string personId)
    {
        Person? p = await _personService.GetPersonById(new ObjectId(personId));
        if(p is null)
        {
            return NotFound();
        }

        if (p.Company is null)
        {
            return BadRequest();
        }

        Company? company = await _companyService.GetCompanyById(p.Company.Value);

        if (company is null)
        {
            return BadRequest();
        }

        var descendants = await _personService.GetDescendantsInCompany(p, company);
        
        return Ok(_mapper.Map<IEnumerable<PersonDto>>(descendants));
    }
    
    /// <summary>
    ///    Return descendants
    /// </summary>
    /// <param name="personId">ID of Person to delete</param>
    /// <returns>Get by ID</returns>
    [HttpGet]
    [Route("descendants/{personId}")]
    public async Task<ActionResult<IReadOnlyCollection<PersonDto>>> GetDescendants(string personId)
    {
        Person? p = await _personService.GetPersonById(new ObjectId(personId));
        if(p is null)
        {
            return NotFound();
        }

        var descendants = await _personService.GetDescendants(p);
        
        return Ok(_mapper.Map<IEnumerable<PersonDto>>(descendants.DistinctBy(p => p.Id)));
    }
    
    /// <summary>
    ///    Return ancestors
    /// </summary>
    [HttpGet]
    [Route("ancestors/{personId}")]
    public async Task<ActionResult<IReadOnlyCollection<PersonDto>>> GetAncestors(string personId)
    {
        Person? p = await _personService.GetPersonById(new ObjectId(personId));
        if(p is null)
        {
            return NotFound();
        }

        var ancestors = await _personService.GetAncestors(p);
        
        return Ok(_mapper.Map<IEnumerable<PersonDto>>(ancestors.DistinctBy(p => p.Id)));
    }
}