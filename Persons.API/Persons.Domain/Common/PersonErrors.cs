using ErrorOr;

namespace Persons.Domain.Common
{
    public static class PersonErrors
    {
        public static Error NotFound => Error.NotFound(
            code: "Person.NotFound",
            description: "Person not found");

        public static Error IdCanNotBeNullOrEmpty => Error.Validation(
            code: "Person.Id",
            description: "Person Id can not be null or empty"
        );

        public static Error InvalidId => Error.Validation(
            code: "Person.InvalidId",
            description: "Invalid id provided");
    }
}