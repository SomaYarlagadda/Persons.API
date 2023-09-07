using ErrorOr;
using Persons.Application.Interfaces;
using Persons.Domain.Common;
using Persons.Domain.Entities;

namespace Persons.Persistence.Services
{
    public class PersonQueryService : IPersonQueryService
    {
        public async Task<ErrorOr<IList<Person>>> GetAllPersons()
        {
            await Task.CompletedTask;

            var persons = Store.PersonsDictionary.Values.ToList();
            return persons;
        }

        public async Task<ErrorOr<Person>> GetPersonById(PersonId id)
        {
            await Task.CompletedTask;

            if (Store.PersonsDictionary.ContainsKey(id))
            {
                return Store.PersonsDictionary.Values.First(x => x.Id == id);
            }

            return PersonErrors.NotFound;
        }

        public async Task<ErrorOr<IList<PersonId>>> GetPersonId(string givenName, string surname)
        {
            await Task.CompletedTask;

            var persons = Store.PersonsDictionary.Values
                .Where(x => x.GivenName.Equals(givenName, StringComparison.OrdinalIgnoreCase) &&
                            x.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Id)
                .ToList();

            return persons;
        }
    }
}
