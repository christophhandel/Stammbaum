using System.Text.Json.Serialization;
using FamilyTreeMongoApp.Model.PersonDetails;
using Newtonsoft.Json;

namespace FamilyTreeMongoApp.Model.Person;

public sealed class PersonDto
{
    public string? Id { get; set; } = default!;

    public string Firstname { get; set; } = default!;

    public string Lastname { get; set; } = default!;

    public string Sex { get; set; } = default!;

    [JsonPropertyName("jobId")]
    public string? Job { get; set; }

    [JsonPropertyName("companyId")]
    public string? Company { get; set; }

    [JsonPropertyName("birthLocation")]
    public LocationDto? BirthLocation { get; set; }

    [JsonPropertyName("motherId")]
    public string? Mother { get; set; } = default!;

    [JsonPropertyName("fatherId")]
    public string? Father { get; set; } = default!;


}
