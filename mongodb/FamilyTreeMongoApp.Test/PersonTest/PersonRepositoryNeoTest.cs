
using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FluentAssertions;
using LeoMongo.Database;
using LeoMongo.Transaction;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using Xunit;


namespace FamilyTreeMongoApp.Test.PersonTest;

public class PersonRepositoryNeoTest : PersonRepositoryBaseTest
{
    public PersonRepositoryNeoTest(IDriver driver, ILogger<IPersonRepository> logger,
        IMapper mapper) :base(new PersonRepositoryNeo(driver, logger, mapper))
    {
    }

    
}