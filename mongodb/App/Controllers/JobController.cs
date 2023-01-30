using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.JobWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.PersonDetails;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController: ControllerBase
{
    private readonly ILogger<JobController> _logger;
    private readonly IMapper _mapper;
    private readonly ITransactionProvider _transactionProvider;
    private readonly IJobService _jobService;

    public JobController(IMapper mapper, 
        IJobService service, 
        ITransactionProvider transactionProvider, 
        ILogger<JobController> logger)
    {
        _mapper = mapper;
        _transactionProvider = transactionProvider;
        _jobService = service;
        _logger = logger;
    }
    
    
    /// <summary>
    ///     Creates a new job.
    /// </summary>
    /// <param name="request">Data for the new comment</param>
    /// <returns>the created comment if successful</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] JobDto request)
    {
                // for a real app it would be a good idea to configure model validation to remove long ifs like this
        if (string.IsNullOrWhiteSpace(request.Name)
            || string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest();
        }

        //using var transaction = await _transactionProvider.BeginTransaction();
        //check if dto contains parents ids
        //      - if yes: check if ids are correct
        //      - if no: one (or more) parents are unknown 
        if(!string.IsNullOrWhiteSpace(request.Id)) {
            var job = await _jobService.GetJobById(MongoDB.Bson.ObjectId.Parse(request.Id));
            if(job == null) {
                return BadRequest();
            }
        }

        var newJob = await _jobService.AddJob(request);
        return new OkResult();
    }
    
    /// <summary>
    ///     Returns all jobs
    /// </summary>
    /// <returns>List of comments, may be empty</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobDto>>> GetAllCompanies()
    {
        IEnumerable<Job> jobs = await _jobService.GetAllJobs();
        return Ok(_mapper.Map<List<JobDto>>(jobs));
    }

    /// <summary>
    ///     Returns job with id
    /// </summary>
    /// <returns>Get by ID</returns>
    [HttpGet]
    [Route("{jobId}")]
    public async Task<ActionResult<IReadOnlyCollection<JobDto>>> GetCompanyById(string jobId)
    {
        Job? job = await _jobService.GetJobById(new ObjectId(jobId));
        
        if(jobId is null)
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map<JobDto>(job));
    }
    
    
    /// <summary>
    ///     Update Job
    /// </summary>
    /// <param name="jobId">ID of Company to update</param>
    /// <param name="Job">New Company</param>
    /// <returns>Get by ID</returns>
    [HttpPut]
    [Route("{jobId}")]
    public async Task<ActionResult<IReadOnlyCollection<CompanyDto>>> UpdateCompany(string jobId, JobDto job)
    {

        Job? searchJob = await _jobService.GetJobById(new ObjectId(jobId));
        if(job is null)
        {
            return NotFound();
        }

        Job newJob = await _jobService.UpdateJob(new ObjectId(jobId),
            job.Name,
            job.JobType
        );
        
        return Ok(_mapper.Map<JobDto>(newJob));
    }
    
    /// <summary>
    ///     Delete Job
    /// </summary>
    /// <param name="jobId">ID of Job to delete</param>
    /// <returns>Get by ID</returns>
    [HttpDelete]
    [Route("{jobId}")]
    public async Task<ActionResult<IReadOnlyCollection<JobDto>>> DeleteJob(string jobId)
    {
        Job? job = await _jobService.GetJobById(new ObjectId(jobId));
        if(job is null)
        {
            return NotFound();
        }

        await _jobService.DeleteJob(new ObjectId(jobId));
        
        return Ok(_mapper.Map<JobDto>(job));
    }
}