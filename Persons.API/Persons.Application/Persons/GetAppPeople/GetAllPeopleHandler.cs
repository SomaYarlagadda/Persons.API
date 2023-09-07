using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Persons.Application.Interfaces;
using Persons.Domain.Entities;

namespace Persons.Application.Persons.GetAppPeople
{
    public class GetAllPeopleHandler : 
        IRequestHandler<GetAllPeopleQuery, ErrorOr<IList<Person>>>
    {
        private readonly IPersonQueryService _personQueryService;
        private readonly ILogger<GetAllPeopleHandler> _logger;

        public GetAllPeopleHandler(IPersonQueryService personQueryService, ILogger<GetAllPeopleHandler> logger)
        {
            _personQueryService = personQueryService;
            _logger = logger;
        }

        public async Task<ErrorOr<IList<Person>>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all person records");
            return await _personQueryService.GetAllPersons();
        }
    }
}
