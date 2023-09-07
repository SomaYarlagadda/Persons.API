using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Persons.Application.Interfaces;
using Persons.Domain.Entities;

namespace Persons.Application.Persons.RecordBirth
{
    public class RecordBirthCommandHandler : IRequestHandler<RecordBirthCommand, ErrorOr<Updated>>
    {
        private readonly IPersonCommandService _personCommandService;
        private readonly IPersonQueryService _personQueryService;
        private readonly ILogger<RecordBirthCommandHandler> _logger;

        public RecordBirthCommandHandler(IPersonCommandService personCommandService, IPersonQueryService personQueryService, ILogger<RecordBirthCommandHandler> logger)
        {
            _personCommandService = personCommandService;
            _personQueryService = personQueryService;
            _logger = logger;
        }
        public async Task<ErrorOr<Updated>> Handle(RecordBirthCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating birth record for {PersonId}", request.Id);
            
            //Validate PersonId
            var personIdResult = PersonId.GetPersonId(request.Id);
            
            if (personIdResult.IsError)
            {
                _logger.LogInformation("Invalid {PersonId} provided for updating the birth record", request.Id);
                return personIdResult.Errors;
            }

            //Make sure the person exists
            var personExistResult = await _personQueryService.GetPersonById(personIdResult.Value);
            if (personExistResult.IsError)
            {
                _logger.LogInformation("No person record exists for the {PersonId}", personIdResult.Value.Value);
                return personExistResult.Errors;
            }

            var person = personExistResult.Value;
            _logger.LogInformation("Updating {PreviousBirthDate} to {NewBirthDate} for {PersonId}",
                person.BirthDate, request.BirthDate, request.Id);
            person.BirthDate = request.BirthDate;

            _logger.LogInformation("Updating {PreviewBirthLocation} to {NewBirthLocation} for {PersonId}",
                person.BirthLocation, request.BirthLocation, request.Id);
            person.BirthLocation =request.BirthLocation;

            var personUpdateResult = await _personCommandService.RecordBirth(person);

            if (personUpdateResult.IsError)
            {
                _logger.LogWarning("Failed to update birth record for {PersonId}.", request.Id);
                return personUpdateResult.Errors;
            }

            _logger.LogInformation("Successfully updated birth record for {PersonId}", request.Id);

            return Result.Updated;
        }
    }
}
