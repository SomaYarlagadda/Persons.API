using ErrorOr;
using Persons.Application.Interfaces;
using Persons.Domain.Common;
using Persons.Domain.Entities;

namespace Persons.Persistence.Services
{
    public class PersonCommandService : IPersonCommandService
    {
        public async Task<ErrorOr<Created>> AddPerson(Person person)
        {
            await Task.CompletedTask;

            Store.PersonsDictionary.Add(person.Id, person);
            return Result.Created;
        }

        public async Task<ErrorOr<Updated>> RecordBirth(Person person)
        {
            await Task.CompletedTask;

            if (!Store.PersonsDictionary.ContainsKey(person.Id))
            {
                return PersonErrors.NotFound;
            }

            Store.PersonsDictionary[person.Id] = person;
            return Result.Updated;
        }
    }
}