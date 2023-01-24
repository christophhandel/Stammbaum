namespace FamilyTreeMongoApp.Model.Person;

public sealed class PersonDto
{
    public string? Id { get; set; } = default!;
    public string Firstname { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public string Sex { get; set; } = default!;
    public string? MotherId { get; set; } = default!;
    public string? FatherId { get; set; } = default!;
}