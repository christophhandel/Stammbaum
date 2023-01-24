using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Core.Workloads.Person;

namespace FamilyTreeMongoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly IMapper _mapper;
    private readonly IPersonService _personService;
    private readonly ITransactionProvider _transactionProvider;

    public PersonController(IMapper mapper, 
        IPersonService service, 
        ITransactionProvider transactionProvider, 
        ILogger<PersonController> logger)
    {
        _mapper = mapper;
        _transactionProvider = transactionProvider;
        _personService = service;
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
        if (string.IsNullOrWhiteSpace(request.FirstName)
            || string.IsNullOrWhiteSpace(request.LastName)
            || request.MotherId == null
            || request.FatherId == null)
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        var father = await _personService.GetPersonById(MongoDB.Bson.ObjectId.Parse(request.FatherId));
        if(father == null) {
            return BadRequest();
        }
        var mother = await _personService.GetPersonById(MongoDB.Bson.ObjectId.Parse(request.MotherId));
        if(mother == null) {
            return BadRequest();
        }
        var person = await _personService.AddPerson(request);
        await transaction.CommitAsync();
        return CreatedAtAction(nameof(Person), new {id = person.Id.ToString()}, person);
    }
    
    
}