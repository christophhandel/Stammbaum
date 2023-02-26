using System.Globalization;
using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Core.Workloads.Person;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using MongoDB.Bson;
using FamilyTreeMongoApp.Model.PersonDetails;

namespace FamilyTreeMongoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AccomplishmentController : ControllerBase
{
    private readonly ILogger<AccomplishmentController> _logger;
    private readonly IMapper _mapper;
    private readonly IPersonService _personService;
    private readonly IAccomplishmentService _accomplishmentService;
    private readonly ITransactionProvider _transactionProvider;

    public AccomplishmentController(IMapper mapper, 
        IPersonService service, 
        IAccomplishmentService accomplishmentService, 
        ITransactionProvider transactionProvider, 
        ILogger<AccomplishmentController> logger)
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
    /// <param name="id"></param>
    /// <returns>amount of achievements</returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAchievement(string id)
    {
        Accomplishment? p = await _accomplishmentService.GetAccomplishmentById(new ObjectId(id));
        if(p is null)
        {
            return NotFound();
        }
        await _accomplishmentService.DeleteAccomplishment(p.Id);
        return Ok();
    }


    /// <summary>
    ///     Creates a new Accomplishment.
    /// </summary>
    /// <param name="request">Data for the new comment</param>
    /// <returns>the created comment if successful</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AccomplishmentDto request)
    {
        // for a real app it would be a good idea to configure model validation to remove long ifs like this
        if (string.IsNullOrWhiteSpace(request.Description)
            || string.IsNullOrWhiteSpace(request.Time))
        {
            return BadRequest();
        }

        //using var transaction = await _transactionProvider.BeginTransaction();
        //check if dto contains parents ids
        //      - if yes: check if ids are correct
        //      - if no: one (or more) parents are unknown 
        if (!string.IsNullOrWhiteSpace(request.Id))
        {
            var accomplishment = await _accomplishmentService.GetAccomplishmentById(MongoDB.Bson.ObjectId.Parse(request.Id));
            if (accomplishment == null)
            {
                return BadRequest();
            }
        }

        var newAccomplishment = await _accomplishmentService.AddAccomplishment(request);
        //await transaction.CommitAsync();
        return new OkResult();
    }

    /// <summary>
    ///     Returns all companies
    /// </summary>
    /// <returns>List of comments, may be empty</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccomplishmentDto>>> GetAllCompanies()
    {
        IEnumerable<Accomplishment> companies = await _accomplishmentService.GetAllAccomplishments();
        return Ok(_mapper.Map<List<AccomplishmentDto>>(companies));
    }

    /// <summary>
    ///     Returns accomplishment with id
    /// </summary>
    /// <returns>Get by ID</returns>
    [HttpGet]
    [Route("{accomplishmentId}")]
    public async Task<ActionResult<IReadOnlyCollection<AccomplishmentDto>>> GetAccomplishmentById(string accomplishmentId)
    {
        Accomplishment? accomplishment = await _accomplishmentService.GetAccomplishmentById(new ObjectId(accomplishmentId));

        if (accomplishmentId is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<AccomplishmentDto>(accomplishment));
    }

    /// <summary>
    ///     Update Accomplishment
    /// </summary>
    /// <param name="accomplishmentId">ID of Accomplishment to update</param>
    /// <param name="Accomplishment">New Accomplishment</param>
    /// <returns>Get by ID</returns>
    [HttpPut]
    [Route("{accomplishmentId}")]
    public async Task<ActionResult<IReadOnlyCollection<AccomplishmentDto>>> UpdateAccomplishment(string accomplishmentId, AccomplishmentDto accomplishmentDto)
    {

        Accomplishment? p = await _accomplishmentService.GetAccomplishmentById(new ObjectId(accomplishmentId));
        if (p is null)
        {
            return NotFound();
        }

        Accomplishment newAccomplishment = await _accomplishmentService.UpdateAccomplishment(new ObjectId(accomplishmentId),
            accomplishmentDto.Description,
            DateTime.ParseExact(accomplishmentDto.Time, "dd.MM.yyyy", CultureInfo.InvariantCulture)
            );

        return Ok(_mapper.Map<AccomplishmentDto>(newAccomplishment));
    }

}