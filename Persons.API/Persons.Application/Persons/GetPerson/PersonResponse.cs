namespace Persons.Application.Persons.GetPerson
{
    public record PersonResponse(
        string Id,
        string GivenName,
        string Surname,
        string Gender,
        DateOnly BirthDate,
        string BirthLocation,
        DateOnly DeathDate,
        string DeathLocation
    );
}
