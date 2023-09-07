using ErrorOr;
using MediatR;
using Persons.Domain.Entities;

namespace Persons.Application.Persons.GetPerson
{
    public record GetPersonQuery(
        string Id) : IRequest<ErrorOr<Person>>;
}
