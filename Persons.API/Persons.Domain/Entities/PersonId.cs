using ErrorOr;
using Persons.Domain.Common;

namespace Persons.Domain.Entities
{
    public class PersonId : ValueObject<PersonId>
    {
        public string Value { get; }

        private PersonId(string value)
        {
            Value = value;
        }

        public static ErrorOr<PersonId> GetPersonId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return PersonErrors.IdCanNotBeNullOrEmpty;
            }

            if (!Guid.TryParse(value, out var id))
            {
                return PersonErrors.InvalidId;
            }

            if (Guid.Empty.Equals(id))
            {
                return PersonErrors.InvalidId;
            }

            return new PersonId(id.ToString());
        }

        public static PersonId UniquePersonId()
        {
            return new PersonId(Guid.NewGuid().ToString());
        }

        protected override bool EqualsCore(PersonId other)
        {
            return other.Value == Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator PersonId(string personId)
        {
            return GetPersonId(personId).Value;
        }

        public static implicit operator string(PersonId personId)
        {
            return personId.Value;
        }
    }
}
