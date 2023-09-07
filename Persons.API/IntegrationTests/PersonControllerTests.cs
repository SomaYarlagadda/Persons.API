using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Persons.Application.Persons.AddPerson;
using Persons.Application.Persons.GetPerson;
using Persons.Application.Persons.RecordBirth;

namespace Person.Api.IntegrationTests
{
    public class PersonControllerTests : IntegrationTests
    {
        private const string TestGuid = "26cb4773-3063-4323-b626-cca00648d67e";

        [Fact]
        public async Task GetAll_Without_Persons_Returns_Empty()
        {
            var response = await TestClient.GetAsync("Person");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPerson_Without_Person_Returns_Empty()
        {
            var response = await TestClient.GetAsync($"Person/{TestGuid}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_Creates_Person()
        {
            var addPersonRequest = 
                new AddPersonRequest("Test", "User", "Male", null, null, null, null);

            var response = await TestClient.PostAsJsonAsync("Person", addPersonRequest);
            
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBe(string.Empty);
        }

        [Fact]
        public async Task Get_Returns_Person_If_Person_Exists()
        {
            //Arrange
            var addPersonRequest =
                new AddPersonRequest("Test", "User", "Male", null, null, null, null);
            var response = await TestClient.PostAsJsonAsync("Person", addPersonRequest);

            //Act            
            var personResponse = await TestClient.GetAsync(response.Headers.Location);

            //Assert
            personResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Record_Birth_Should_Return_NoFound_When_No_Person()
        {
            //Arrange
            var birthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
            var recordBirthRequest = new RecordBirthRequest(birthDate, "Test_Location");

            //Act            
            var response = await TestClient.PutAsJsonAsync($"Person/{TestGuid}", recordBirthRequest);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Record_Birth_Should_Succeed_If_Person_Exists()
        {
            //Arrange
            var birthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
            var birthLocation = "Test_Location";
            var recordBirthRequest = new RecordBirthRequest(birthDate, birthLocation);
            var addPersonRequest = new AddPersonRequest("Test", "User", "Male", null, null, null, null);

            var response = await TestClient.PostAsJsonAsync("Person", addPersonRequest);
            var personResponse = await TestClient.GetAsync(response.Headers.Location);
            var person = await personResponse.Content.ReadFromJsonAsync<PersonResponse>();
            
            //Act            
            var recordBirthresponse = await TestClient.PutAsJsonAsync($"Person/{person.Id}", recordBirthRequest);

            //Assert
            recordBirthresponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            //Act
            personResponse = await TestClient.GetAsync(response.Headers.Location);
            person = await personResponse.Content.ReadFromJsonAsync<PersonResponse>();

            //Assert
            person.Should().NotBeNull();
            person.BirthDate.Should().Be(birthDate);
            person.BirthLocation.Should().Be(birthLocation);
        }
    }
}
