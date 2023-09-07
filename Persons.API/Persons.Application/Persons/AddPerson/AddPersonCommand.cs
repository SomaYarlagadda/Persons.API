using ErrorOr;
using MediatR;
using Persons.Domain.Entities;

namespace Persons.Application.Persons.AddPerson
{
    public record AddPersonCommand(
        string GivenName,
        string Surname,
        string Gender,
        DateOnly BirthDate,
        string BirthLocation,
        DateOnly DeathDate,
        string DeathLocation) : IRequest<ErrorOr<Person>>;
}
