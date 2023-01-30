using FamilyTreeMongoApp.Core.Workloads.Person;
using FamilyTreeMongoApp.Model.PersonDetails;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;

public interface IAccomplishmentService
{
    Task<Accomplishment?> GetAccomplishmentById(ObjectId parse);
    Task<Accomplishment?> AddAccomplishment(AccomplishmentDto request);
    Task<IEnumerable<Accomplishment>> GetAllAccomplishments();
    Task<Accomplishment> UpdateAccomplishment(ObjectId objectId, string description, DateTime time);
    Task DeleteAccomplishment(ObjectId objectId);

}
