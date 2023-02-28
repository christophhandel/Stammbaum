using FamilyTreeMongoApp.Core.Workloads.JobWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.Statistics;
using FluentAssertions;
using Xunit;

namespace FamilyTreeMongoApp.Test.JobTest;

public abstract class JobRepositoryBaseTest : IDisposable
{
    private readonly IJobRepository _jobRepository;
    private readonly IPersonRepository _personRepository;


    protected JobRepositoryBaseTest(IJobRepository jobRepository, IPersonRepository personRepository)
    {
        _jobRepository = jobRepository;
        _personRepository = personRepository;
    }

    [Fact]
    public async Task TestJobStats()
    {
        Job firstJob = await _jobRepository.AddJob(new Job()
        {
            JobType = "Software Engineering",
            Name = "Project Owner"
        });
        Job secondJob = await _jobRepository.AddJob(new Job()
        {
            JobType = "Facility Management",
            Name = "Communications"
        });
        Person p1 = await _personRepository.AddPerson(new Person()
        {
            Firstname = "Foo",
            Lastname = "Bar",
            Sex = "m",
            Job = firstJob.Id
        });
        Person p2 = await _personRepository.AddPerson(new Person()
        {
            Firstname = "Baz",
            Lastname = "Bar",
            Sex = "f",
            Job = firstJob.Id
        });
        Person p3 = await _personRepository.AddPerson(new Person()
        {
            Firstname = "Bert",
            Lastname = "Bar",
            Sex = "m",
            Job = secondJob.Id
        });

        var stats =  await _jobRepository.GetJobsStats();

        stats.Count().Should().Be(2);
        stats.Should().Contain(new JobStatDto("Project Owner", 1, 1));
        stats.Should().Contain(new JobStatDto("Communications", 0, 1));

        await _jobRepository.DeleteJob(firstJob.Id);
        await _jobRepository.DeleteJob(secondJob.Id);
        await _personRepository.DeletePerson(p1.Id);
        await _personRepository.DeletePerson(p2.Id);
        await _personRepository.DeletePerson(p3.Id);
    }


    public void Dispose()
    {
        
    }
}