using System.Text.Json.Serialization;

namespace FamilyTreeMongoApp.Model.PersonDetails
{
    public class JobDto
    {
        public string? Id { get; set; }

        public string Name { get; set; } = default!;

        [JsonPropertyName("description")]
        public string JobType { get; set; } = default!;

    }
}
