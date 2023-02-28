
using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;
using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using FamilyTreeMongoApp.Core.Workloads.JobWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FluentAssertions;
using LeoMongo.Database;
using LeoMongo.Transaction;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using Xunit;


namespace FamilyTreeMongoApp.Test.CompanyTest;

public class CompanyRepositoryNeoTest : CompanyRepositoryBaseTest
{
    public CompanyRepositoryNeoTest(IDriver driver, ILogger<IPersonRepository> logger,
        IMapper mapper) :base(new CompanyRepositoryNeo(mapper, driver), new PersonRepositoryNeo(driver, logger, mapper))
    {
    }

    
}