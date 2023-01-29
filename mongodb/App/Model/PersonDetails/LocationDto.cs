namespace FamilyTreeMongoApp.Model.PersonDetails
{
    public class LocationDto
    {
        public string? Id { get; set; }

        public string Country { get; set; } = default!;

        public string City { get; set; } = default!;
    }
}
