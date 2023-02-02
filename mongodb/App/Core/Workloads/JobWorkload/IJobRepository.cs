using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Model.PersonDetails;
using LeoMongo.Database;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.JobWorkload;

public interface IJobRepository : IRepositoryBase
{
    Task<Job?> GetJobById(ObjectId parse);
    Task<Job> AddJob(Job request);
    Task<IEnumerable<Job>> GetAllJobs();
    Task<Job> UpdateJob(ObjectId objectId, string jobName, string jobDescription);
    Task DeleteJob(ObjectId objectId);
    Task DeleteCollection();
}
