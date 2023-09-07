using FluentValidation;
using static Persons.Application.Common.DateOnlyHelper;

namespace Persons.Application.Persons.RecordBirth
{
    public class RecordBirthRequestValidator : AbstractValidator<RecordBirthRequest>
    {
        public RecordBirthRequestValidator()
        {
            RuleFor(p => p.BirthDate)
                .Must(BeValid)
                .When(p => string.IsNullOrWhiteSpace(p.BirthLocation))
                .WithMessage("Birth date is required to record birth when no birth location specified");

            RuleFor(p => p.BirthLocation)
                .NotEmpty()
                .When(p => p.BirthDate?.CompareTo(DateOnly.MinValue) <= 0, ApplyConditionTo.CurrentValidator)
                .WithMessage("Birth location is required to record birth when no birth date is specified");
        }
    }
}
