
using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;
using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using FamilyTreeMongoApp.Core.Workloads.JobWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Test.CompanyTest;
using FluentAssertions;
using LeoMongo.Database;
using LeoMongo.Transaction;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using Xunit;


namespace FamilyTreeMongoApp.Test.CompanyTest;

public class CompanyRepositoryMongoTest : CompanyRepositoryBaseTest
{

    public CompanyRepositoryMongoTest(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
        IPersonRepository personRepository, IAccomplishmentRepository accomplishmentRepository, IMapper mapper) 
        : base(new CompanyRepository(transactionProvider, databaseProvider, personRepository), 
            new PersonRepository(transactionProvider, databaseProvider, accomplishmentRepository, mapper))
    {
        
    }
}