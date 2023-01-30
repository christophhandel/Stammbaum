using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Core.Workloads.Person;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AchievementController : ControllerBase
{
    private readonly ILogger<AchievementController> _logger;
    private readonly IMapper _mapper;
    private readonly IPersonService _personService;
    private readonly IAccomplishmentService _accomplishmentService;
    private readonly ITransactionProvider _transactionProvider;

    public AchievementController(IMapper mapper, 
        IPersonService service, 
        IAccomplishmentService accomplishmentService, 
        ITransactionProvider transactionProvider, 
        ILogger<AchievementController> logger)
    {
        _mapper = mapper;
        _transactionProvider = transactionProvider;
        _personService = service;
        _accomplishmentService = accomplishmentService;
        _logger = logger;
    }

    /// <summary>
    ///     Get Achievement Count
    ///
    /// </summary>
    /// <param name="personId">ID of Person to get achievement count of</param>
    /// <returns>amount of achievements</returns>
    [HttpDelete]
    [Route("count/{personId}")]
    public async Task<ActionResult<IReadOnlyCollection<PersonDto>>> GetAchievementCount(string personId)
    {
        Person? p = await _personService.GetPersonById(new ObjectId(personId));
        if(p is null)
        {
            return NotFound();
        }

        int accomplishmentsCount = await _personService.GetAccomplishmentsCount(new ObjectId(personId));
        
        return Ok(_mapper.Map<PersonDto>(accomplishmentsCount));
    }
    
    
}