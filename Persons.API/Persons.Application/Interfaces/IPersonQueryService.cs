using ErrorOr;
using Persons.Domain.Entities;

namespace Persons.Application.Interfaces
{
    public interface IPersonQueryService
    {
        Task<ErrorOr<Person>> GetPersonById(PersonId id);

        Task<ErrorOr<IList<PersonId>>> GetPersonId(string givenName, string surname);

        Task<ErrorOr<IList<Person>>> GetAllPersons();
    }
}
