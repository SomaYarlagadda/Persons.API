using Persons.Domain.Entities;

namespace Persons.Domain
{
    public interface IEntity
    {
        PersonId Id { get; }
    }
}