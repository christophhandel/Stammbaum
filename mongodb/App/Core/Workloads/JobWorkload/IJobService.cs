using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Model.PersonDetails;
using FamilyTreeMongoApp.Model.Statistics;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.JobWorkload;

public interface IJobService
{
    Task<Job?> GetJobById(ObjectId parse);
    Task<Job?> AddJob(JobDto request);
    Task<IEnumerable<Job>> GetAllJobs();
    Task<Job> UpdateJob(ObjectId objectId, string jobName, string jobDescription);
    Task DeleteJob(ObjectId objectId);
    Task DeleteCollection();
    Task<IEnumerable<JobStatDto>> GetJobsStats();
}
