using AutoMapper;
using FamilyTreeMongoApp.Core.Workloads.Person;
using FamilyTreeMongoApp.Model.Person;

namespace FamilyTreeMongoApp.Core.Util;

public sealed class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
    }
}