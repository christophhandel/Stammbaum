
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

public class PersonRepositoryMongoTest : PersonRepositoryBaseTest
{

    public PersonRepositoryMongoTest(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
        IAccomplishmentRepository accomplishmentRepository,
        IMapper mapper) : base(new PersonRepository(transactionProvider, databaseProvider, accomplishmentRepository, mapper))
    {
        
    }
}