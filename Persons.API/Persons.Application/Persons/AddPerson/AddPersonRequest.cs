namespace Persons.Application.Persons.AddPerson
{
    public record AddPersonRequest(
        string? GivenName, 
        string? Surname, 
        string Gender, 
        DateOnly? BirthDate, 
        string? BirthLocation, 
        DateOnly? DeathDate,
        string? DeathLocation
    );
}
