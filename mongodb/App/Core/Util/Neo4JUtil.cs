using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using MongoDB.Bson;
using Neo4j.Driver;

namespace FamilyTreeMongoApp.Core.Util;

public class Neo4JUtil
{
    public static string personReturnAllFieldsQuery = "RETURN p.firstname AS firstname, p.lastname AS lastname, p.sex AS sex, p.id AS id, p.jobId AS jobId, p.companyId AS companyId, p.motherId AS motherId, p.fatherId as fatherId";

    public static Person convertIRecordToPerson(IRecord p)
    {
        return new Person() {
            Firstname = p["firstname"].As<string>(), 
            Lastname = p["lastname"].As<string>(), 
            Sex = p["sex"].As<string>(), 
            Id=ObjectId.Parse(p["id"].As<string>()),
            Job=p["jobId"] == null ? null :ObjectId.Parse(p["jobId"].As<string>()),
            Company= p["companyId"] == null ? null : ObjectId.Parse(p["companyId"].As<string>()),
            Father= p["fatherId"] == null ? null : ObjectId.Parse(p["fatherId"].As<string>()),
            Mother= p["motherId"] == null ? null : ObjectId.Parse(p["motherId"].As<string>()),
        };
    }
    
    
}