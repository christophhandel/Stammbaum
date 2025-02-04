﻿using System.Text.Json.Serialization;

namespace FamilyTreeMongoApp.Model.PersonDetails
{
    public class AccomplishmentDto
    {
        [JsonPropertyName("personId")]
        public string? Person { get; set; } = default!;
        public string? Id { get; set; } = default!;

        public string Time { get; set; } = default!;

        public string Description { get; set; } = default!;
    
        public void SetTimeFromDateTime(DateTime time)
        {
            Time = time.ToString("dd.MM.yyyy");
        }
    }
}
