namespace Persons.Domain.Entities
{
    public class Person : IEntity
    {
        private readonly string _id;
        public PersonId Id => (PersonId)_id;

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public GenderType Gender { get; set; }

        //Birthdate can be a ValueObject with checks for date validity i.e. Not in future and than MinValue
        public DateOnly BirthDate { get; set; }

        public string BirthLocation { get; set; }

        //Death date can be a ValueObject with checks for date validity like Not before birthdate and greater than MinValue
        public DateOnly DeathDate { get; set; }

        public string DeathLocation { get; set; }

        public Person(
            string givenName,
            string surname,
            string gender,
            DateOnly birthDate,
            string birthLocation,
            DateOnly deathDate,
            string deathLocation,
            PersonId? id = null)
        {
            _id = id ?? PersonId.UniquePersonId();
            GivenName = givenName;
            Surname = surname;
            Gender = Enum.Parse<GenderType>(gender);
            BirthDate = birthDate;
            BirthLocation = birthLocation;
            DeathDate = deathDate;
            DeathLocation = deathLocation;
        }
    }

    public enum GenderType
    {
        Female,
        Male,
        Other
    }
}
