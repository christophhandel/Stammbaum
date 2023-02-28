
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

public class JobRepositoryMongoTest : JobRepositoryBaseTest
{

    public JobRepositoryMongoTest(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
        IPersonRepository personRepository, IAccomplishmentRepository accomplishmentRepository, IMapper mapper) 
        : base(new JobRepository(transactionProvider, databaseProvider, personRepository), 
            new PersonRepository(transactionProvider, databaseProvider, accomplishmentRepository, mapper))
    {
        
    }
}