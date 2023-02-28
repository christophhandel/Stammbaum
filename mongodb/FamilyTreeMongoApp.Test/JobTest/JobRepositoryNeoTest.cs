
using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;
using FamilyTreeMongoApp.Core.Workloads.JobWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FluentAssertions;
using LeoMongo.Database;
using LeoMongo.Transaction;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using Xunit;


namespace FamilyTreeMongoApp.Test.JobTest;

public class JobRepositoryNeoTest : JobRepositoryBaseTest
{
    public JobRepositoryNeoTest(IDriver driver, ILogger<IPersonRepository> logger,
        IMapper mapper) :base(new JobRepositoryNeo(driver, mapper), new PersonRepositoryNeo(driver, logger, mapper))
    {
    }

    
}