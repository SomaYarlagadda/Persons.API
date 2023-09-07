using ErrorOr;
using Persons.Domain.Entities;

namespace Persons.Application.Interfaces
{
    public interface IPersonCommandService
    {
        Task<ErrorOr<Created>> AddPerson(Person person);

        Task<ErrorOr<Updated>> RecordBirth(Person person);
    }
}
