using Mapster;
using Persons.Application.Persons.AddPerson;
using Persons.Application.Persons.GetPerson;
using Persons.Application.Persons.RecordBirth;
using Persons.Domain.Entities;

namespace Persons.API.Mapping
{
    public class PersonMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddPersonRequest, AddPersonCommand>();

            config.NewConfig<(string id, RecordBirthRequest Request), RecordBirthCommand>()
                .Map(dest => dest.Id, src => src.id)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<Person, PersonResponse>()
                .Map(dest => dest.Id, src => src.Id.Value);
        }
    }
}
