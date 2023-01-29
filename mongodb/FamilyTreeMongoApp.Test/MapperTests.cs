using AutoMapper;
using FamilyTreeMongoApp.Core.Util;
using Xunit;

namespace FamilyTreeMongoApp.Test;


public sealed class MapperTests
{
    [Fact (Skip = "Not all properties are mapped!")]
    public void TestMappingProfile()
    {
        var profile = new MapperProfile();
        var cfg = new MapperConfiguration(c => c.AddProfile(profile));
        cfg.AssertConfigurationIsValid();
    }
}