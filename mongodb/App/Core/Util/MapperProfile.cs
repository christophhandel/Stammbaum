using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.CompanyWorkload;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using FamilyTreeMongoApp.Model.Person;
using FamilyTreeMongoApp.Model.PersonDetails;

namespace FamilyTreeMongoApp.Core.Util;

public sealed class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(p => p.BirthLocation, opt => opt.MapFrom(src => src.BirthPlace))
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
        CreateMap<Job,JobDto>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
        CreateMap<Location,LocationDto>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
        CreateMap<LocationDto,Location>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id!.ToString()));
        CreateMap<CompanyDto,Company>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id!.ToString()));
        CreateMap<Company,CompanyDto>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id!.ToString()));
        CreateMap<JobDto,JobDto>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id!.ToString()));
        CreateMap<Job,JobDto>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id!.ToString()));
    }
}