﻿namespace FamilyTreeMongoApp.Model.Person;

public sealed class PersonDto
{
    public string Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public string MotherId { get; set; } = default!;
    public string FatherId { get; set; } = default!;
}