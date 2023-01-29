using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.Person;
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
        
    }
}