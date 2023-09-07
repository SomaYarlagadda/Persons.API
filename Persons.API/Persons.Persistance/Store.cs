using Persons.Domain.Entities;

namespace Persons.Persistence
{
    internal static class Store
    {
        internal static readonly Dictionary<string, Person> PersonsDictionary = new();
    }
}
