using ErrorOr;
using MediatR;

namespace Persons.Application.Persons.RecordBirth
{
    public record RecordBirthCommand(
        string Id,
        DateOnly BirthDate,
        string BirthLocation) : IRequest<ErrorOr<Updated>>;
}
