using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using FamilyTreeMongoApp.Core.Workloads.JobWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using MongoDB.Bson;
using Neo4j.Driver;

namespace FamilyTreeMongoApp.Core.Util;

public class Neo4JUtil
{
    public static string personReturnAllFieldsQuery = "RETURN p.firstname AS firstname, p.lastname AS lastname, p.sex AS sex, p.id AS id, p.jobId AS jobId, p.companyId AS companyId, p.motherId AS motherId, p.fatherId as fatherId";
    public static string jobReturnAllFieldsQuery = "RETURN j.id as id, j.name as name, j.jobType as jobType";
    public static object companyReturnAllFieldsQuery = "RETURN c.id as id, c.name as name, c.businessActivity as businessActivity";

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


    public static Job convertIRecordToJob(IRecord p)
    {
        return new Job() {
            Id = ObjectId.Parse(p["id"].As<string>()),
            Name = p["name"].As<string>(),
            JobType = p["jobType"].As<string>(),
        };
    }

    public static Company convertIRecordToCompany(IRecord p)
    {
        return new Company()
        {
            Id = ObjectId.Parse(p["id"].As<string>()),
            Name = p["name"].As<string>(),
            BusinessActivity = p["businessActivity"].As<string>(),
        };
    }
}