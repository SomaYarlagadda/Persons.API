using FluentValidation;
using Persons.Domain.Entities;
using static Persons.Application.Common.DateOnlyHelper;

namespace Persons.Application.Persons.AddPerson
{
    public class AddPersonRequestValidator : AbstractValidator<AddPersonRequest>
    {
        public AddPersonRequestValidator()
        {
            RuleFor(p => p.GivenName)
                .NotEmpty()
                .When(p => string.IsNullOrWhiteSpace(p.Surname))
                .WithMessage("At lease one name, either given name or surname is required");

            RuleFor(p => p.Surname)
                .NotEmpty()
                .When(p => string.IsNullOrWhiteSpace(p.GivenName))
                .WithMessage("At lease one name, either given name or surname is required");

            var allowedValues = Enum.GetNames(typeof(GenderType)).ToList();
            RuleFor(p => p.Gender)
                .Must(p => allowedValues.Contains(p))
                .WithMessage("{PropertyName} outside the allowed values of Woman, Man, and Other");


            RuleFor(p => p.DeathDate)
                .GreaterThanOrEqualTo(p => p.BirthDate)
                .When(p => BeValid(p.DeathDate))
                .WithMessage("DeathDate should be later than BirthDate and should be in a valid format yyyy-MM-dd");

            RuleFor(p => p.BirthDate)
                .Must(BeValid)
                .When(p => !string.IsNullOrWhiteSpace(p.BirthDate.ToString()))
                .WithMessage("Please enter date in valid format yyyy-MM-dd");
        }
    }
}
