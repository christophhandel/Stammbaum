using FamilyTreeMongoApp.Core.Workloads.Person;
using LeoMongo.Database;
using MongoDB.Bson;

namespace FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;

public interface IAccomplishmentRepository : IRepositoryBase
{

    Task<Accomplishment?> GetAccomplishmentById(ObjectId parse);
    Task<Accomplishment?> AddAccomplishment(Accomplishment accomplishment);
    Task<IEnumerable<Accomplishment>> GetAllAccomplishments();
    Task<Accomplishment> UpdateAccomplishment(ObjectId objectId, string description, DateTime time);
    Task DeleteAccomplishment(ObjectId objectId);
    Task DeleteCollection();
    Task<IEnumerable<Accomplishment>> GetAccomplishmentByPersonId(ObjectId objectId);
}
