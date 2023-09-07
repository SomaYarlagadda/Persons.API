using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Persons.Application.Interfaces;
using Persons.Application.Persons.AddPerson;
using Persons.Domain.Entities;

namespace Persons.Application.Tests
{
    public class AddPersonCommandHandlerTests
    {
        private readonly Mock<IPersonCommandService> _personCommandServiceMock;
        private readonly Mock<ILogger<AddPersonCommandHandler>> _loggerMock;

        public AddPersonCommandHandlerTests()
        {
            _personCommandServiceMock = new Mock<IPersonCommandService>();
            _loggerMock = new Mock<ILogger<AddPersonCommandHandler>>();
        }

        [Fact]
        public async Task Handle_Returns_Success_When_Adding_Person()
        {
            //Arrange
            var addPersonCommand = new AddPersonCommand(
                "Test", "User", "Male", DateOnly.MinValue,
                null, DateOnly.MinValue, null);

            var addPersonCommandHandler =
                new AddPersonCommandHandler(_personCommandServiceMock.Object, _loggerMock.Object);
            
            //Act
            var result = await addPersonCommandHandler.Handle(addPersonCommand, default);

            //Assert
            result.IsError.Should().BeFalse();
            result.Value.Id.Value.Should().NotBe(string.Empty);
        }

        [Fact]
        public async Task Handle_Should_Call_AddPerson_From_Person_Command_service()
        {
            //Arrange
            var givenName = "Test";

            var addPersonCommand = new AddPersonCommand(
                givenName, "User", "Male", DateOnly.MinValue,
                null, DateOnly.MinValue, null);

            var addPersonCommandHandler =
                new AddPersonCommandHandler(_personCommandServiceMock.Object, _loggerMock.Object);

            //Act
            var result = await addPersonCommandHandler.Handle(addPersonCommand, default);

            //Assert
            _personCommandServiceMock.Verify(
                x => x.AddPerson(It.Is<Person>(m => m.GivenName == givenName && !string.IsNullOrWhiteSpace(m.Id.Value))),
                Times.Once);

        }
    }
}
