using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;
using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using FamilyTreeMongoApp.Core.Workloads.JobWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Model.PersonDetails;
using FamilyTreeMongoApp.Model.Statistics;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;


namespace FamilyTreeMongoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;
        private readonly ICompanyService _companyService;
        private readonly IJobService _jobService;
        private readonly IAccomplishmentService _accomplishmentService;
        private readonly ITransactionProvider _transactionProvider;

        public StatsController(
            ILogger<PersonController> logger,
            IMapper mapper,
            IPersonService personService,
            ICompanyService companyService,
            IAccomplishmentService accomplishmentService,
            IJobService jobService,
            ITransactionProvider transactionProvider)
        {
            _logger = logger;
            _mapper = mapper;
            _personService = personService;
            _companyService = companyService;
            _jobService = jobService;
            _accomplishmentService = accomplishmentService;
            _transactionProvider = transactionProvider;
        }

        [HttpGet]
        [Route("/job-stats")]
        public async Task<IEnumerable<JobStatDto>> GetJobStats()
        {
            return await _jobService.GetJobsStats();
        }
    }
}
