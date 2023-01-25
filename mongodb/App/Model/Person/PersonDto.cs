using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace FamilyTreeMongoApp.Model.Person;

public sealed class PersonDto
{
    public string? Id { get; set; } = default!;
    public string Firstname { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public string Sex { get; set; } = default!;
    [JsonPropertyName("MotherId")]
    public string? Mother { get; set; } = default!;
    [JsonPropertyName("FatherId")]
    public string? Father { get; set; } = default!;
}