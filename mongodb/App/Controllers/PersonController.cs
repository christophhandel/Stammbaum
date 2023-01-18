using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
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
    
    
}