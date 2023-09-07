namespace Persons.Application.Persons.RecordBirth
{
    public record RecordBirthRequest(
        DateOnly? BirthDate, 
        string? BirthLocation
        );
}
