using ErrorOr;
using MediatR;
using Persons.Domain.Entities;

namespace Persons.Application.Persons.GetAppPeople
{
    public record GetAllPeopleQuery : IRequest<ErrorOr<IList<Person>>>;
}
