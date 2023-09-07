using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persons.Application.Persons.AddPerson;
using Persons.Application.Persons.GetAppPeople;
using Persons.Application.Persons.GetPerson;
using Persons.Application.Persons.RecordBirth;

namespace Persons.API.Controllers
{
    public class PersonController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PersonController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(string id)
        {
            var getPersonQuery = new GetPersonQuery(id);
            var result = await _mediator.Send(getPersonQuery);

            return result.Match(personResult => Ok(_mapper.Map<PersonResponse>(personResult)),
                Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            var getAllQuery = new GetAllPeopleQuery();
            var result = await _mediator.Send(getAllQuery);

            return result.Match(personResult => Ok(_mapper.Map<IList<PersonResponse>>(personResult)),
                Problem);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(AddPersonRequest request)
        {
            var addPersonCommand = _mapper.Map<AddPersonCommand>(request);
            var result = await _mediator.Send(addPersonCommand);
            
            return result.Match(_ => base.CreatedAtAction(actionName: nameof(GetPerson),
                    routeValues: new {id = result.Value.Id.Value}, null),
                Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> RecordBirth(string id, RecordBirthRequest request)
        {
            var recordBirthCommand = _mapper.Map<RecordBirthCommand>((id, request));

            var result = await _mediator.Send(recordBirthCommand);
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
