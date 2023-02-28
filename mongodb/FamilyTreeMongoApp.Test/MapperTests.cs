using AutoMapper;
using FamilyTreeMongoApp.Core.Util;
using Xunit;

namespace FamilyTreeMongoApp.Test;


public sealed class MapperTests
{
    [Fact]
    public void TestMappingProfile()
    {
        var profile = new MapperProfile();
        var cfg = new MapperConfiguration(c => c.AddProfile(profile));
        cfg.AssertConfigurationIsValid();
    }
}