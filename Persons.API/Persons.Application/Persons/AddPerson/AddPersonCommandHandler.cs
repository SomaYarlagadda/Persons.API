using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Persons.Application.Interfaces;
using Persons.Domain.Entities;

namespace Persons.Application.Persons.AddPerson
{
    public class AddPersonCommandHandler : 
        IRequestHandler<AddPersonCommand, ErrorOr<Person>>
    {
        private readonly IPersonCommandService _personCommandService;
        private readonly ILogger<AddPersonCommandHandler> _logger;

        public AddPersonCommandHandler(IPersonCommandService personCommandService, ILogger<AddPersonCommandHandler> logger)
        {
            _personCommandService = personCommandService;
            _logger = logger;
        }

        public async Task<ErrorOr<Person>> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding new person record");

            var person = new Person(request.GivenName, 
                request.Surname, 
                request.Gender, 
                request.BirthDate,
                request.BirthLocation, 
                request.DeathDate, 
                request.DeathLocation);

            var result = await _personCommandService.AddPerson(person);

            if (result.IsError)
            {
                _logger.LogWarning("Failed to add person record. {ErrorCode} {ErrorMessage}", result.FirstError.Code, result.FirstError.Description);
                return result.Errors;
            }
            
            _logger.LogInformation("Successfully added new person record with {PersonId}", person.Id.Value);
            return person;
        }
    }
}
