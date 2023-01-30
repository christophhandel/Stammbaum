using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Model.PersonDetails;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController: ControllerBase
{
    private readonly ILogger<CompanyController> _logger;
    private readonly IMapper _mapper;
    private readonly ITransactionProvider _transactionProvider;
    private readonly ICompanyService _companyService;

    public CompanyController(IMapper mapper, 
        ICompanyService service, 
        ITransactionProvider transactionProvider, 
        ILogger<CompanyController> logger)
    {
        _mapper = mapper;
        _transactionProvider = transactionProvider;
        _companyService = service;
        _logger = logger;
    }
    
    
    /// <summary>
    ///     Creates a new Company.
    /// </summary>
    /// <param name="request">Data for the new comment</param>
    /// <returns>the created comment if successful</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CompanyDto request)
    {
                // for a real app it would be a good idea to configure model validation to remove long ifs like this
        if (string.IsNullOrWhiteSpace(request.Name)
            || string.IsNullOrWhiteSpace(request.BusinessActivity))
        {
            return BadRequest();
        }

        //using var transaction = await _transactionProvider.BeginTransaction();
        //check if dto contains parents ids
        //      - if yes: check if ids are correct
        //      - if no: one (or more) parents are unknown 
        if(!string.IsNullOrWhiteSpace(request.Id)) {
            var company = await _companyService.GetCompanyById(MongoDB.Bson.ObjectId.Parse(request.Id));
            if(company == null) {
                return BadRequest();
            }
        }

        var newCompany = await _companyService.AddCompany(request);
        //await transaction.CommitAsync();
        return new OkResult();
    }
    
    /// <summary>
    ///     Returns all companies
    /// </summary>
    /// <returns>List of comments, may be empty</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAllCompanies()
    {
        IEnumerable<Company> companies = await _companyService.GetAllCompanies();
        return Ok(_mapper.Map<List<CompanyDto>>(companies));
    }

    /// <summary>
    ///     Returns company with id
    /// </summary>
    /// <returns>Get by ID</returns>
    [HttpGet]
    [Route("{companyId}")]
    public async Task<ActionResult<IReadOnlyCollection<CompanyDto>>> GetCompanyById(string companyId)
    {
        Company? company = await _companyService.GetCompanyById(new ObjectId(companyId));
        
        if(companyId is null)
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map<CompanyDto>(company));
    }
    
    
    /// <summary>
    ///     Update Company
    /// </summary>
    /// <param name="companyId">ID of Company to update</param>
    /// <param name="Company">New Company</param>
    /// <returns>Get by ID</returns>
    [HttpPut]
    [Route("{companyId}")]
    public async Task<ActionResult<IReadOnlyCollection<CompanyDto>>> UpdateCompany(string companyId, CompanyDto company)
    {

        Company? p = await _companyService.GetCompanyById(new ObjectId(companyId));
        if(p is null)
        {
            return NotFound();
        }

        Company newCompany = await _companyService.UpdateCompany(new ObjectId(companyId),
            company.Name,
            company.BusinessActivity
        );
        
        return Ok(_mapper.Map<CompanyDto>(newCompany));
    }
    
    /// <summary>
    ///     Delete Company
    /// </summary>
    /// <param name="companyId">ID of Company to delete</param>
    /// <returns>Get by ID</returns>
    [HttpDelete]
    [Route("{companyId}")]
    public async Task<ActionResult<IReadOnlyCollection<CompanyDto>>> DeleteCompany(string companyId)
    {
        Company? company = await _companyService.GetCompanyById(new ObjectId(companyId));
        if(company is null)
        {
            return NotFound();
        }

        await _companyService.DeleteCompany(new ObjectId(companyId));
        
        return Ok(_mapper.Map<CompanyDto>(company));
    }
}