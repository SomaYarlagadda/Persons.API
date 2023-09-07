using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Persons.Application.Interfaces;
using Persons.Domain.Entities;

namespace Persons.Application.Persons.GetPerson
{
    internal class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, ErrorOr<Person>>
    {
        private readonly IPersonQueryService _personQueryService;
        private readonly ILogger<GetPersonQueryHandler> _logger;

        public GetPersonQueryHandler(IPersonQueryService personQueryService, ILogger<GetPersonQueryHandler> logger)
        {
            _personQueryService = personQueryService;
            _logger = logger;
        }
        public async Task<ErrorOr<Person>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving person record for {PersonId}", request.Id);
            var personIdResult = PersonId.GetPersonId(request.Id);

            if (!personIdResult.IsError)
            {
                return await _personQueryService.GetPersonById(personIdResult.Value);
            }

            _logger.LogInformation("Failed to retrieve person record for {PersonId}", request.Id);
            return personIdResult.Errors;
        }
    }
}
