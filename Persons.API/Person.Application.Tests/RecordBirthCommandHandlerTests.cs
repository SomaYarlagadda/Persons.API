
using ErrorOr;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Persons.Application.Interfaces;
using Persons.Application.Persons.RecordBirth;
using Persons.Domain.Common;
using Persons.Domain.Entities;

namespace Persons.Application.Tests
{
    public class RecordBirthCommandHandlerTests
    {
        private readonly Mock<IPersonCommandService> _personCommandServiceMock;
        private readonly Mock<IPersonQueryService> _personQueryServiceMock;
        private readonly Mock<ILogger<RecordBirthCommandHandler>> _loggerMock;

        public RecordBirthCommandHandlerTests()
        {
            _personCommandServiceMock = new Mock<IPersonCommandService>();
            _personQueryServiceMock = new Mock<IPersonQueryService>();
            _loggerMock = new Mock<ILogger<RecordBirthCommandHandler>>();
        }

        [Fact]
        public async Task Handle_Returns_Error_For_Invalid_PersonId()
        {
            //Arrange
            var recordBirthCommand =
                new RecordBirthCommand("Invalid_Id", DateOnly.FromDateTime(DateTime.Now), "Test_Location");

            var recordBirthHandler = new RecordBirthCommandHandler(_personCommandServiceMock.Object,
                _personQueryServiceMock.Object, _loggerMock.Object);

            //Act
            var result = await recordBirthHandler.Handle(recordBirthCommand, default);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be(PersonErrors.InvalidId.Code);
        }

        [Fact]
        public async Task Handle_Returns_Error_When_Person_Record_NotExists()
        {
            //Arrange
            var recordBirthCommand =
                new RecordBirthCommand(Guid.NewGuid().ToString(), DateOnly.FromDateTime(DateTime.Now), "Test_Location");

            var recordBirthHandler = new RecordBirthCommandHandler(_personCommandServiceMock.Object,
                _personQueryServiceMock.Object, _loggerMock.Object);

            _personQueryServiceMock.Setup(x => x.GetPersonById(It.IsAny<PersonId>()))
                .ReturnsAsync(PersonErrors.NotFound);
                

            //Act
            var result = await recordBirthHandler.Handle(recordBirthCommand, default);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be(PersonErrors.NotFound.Code);
        }

        [Fact]
        public async Task Handle_Returns_Success_When_Person_Record_Exists()
        {
            //Arrange
            var id = Guid.NewGuid().ToString();
            var birthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
            var birthLocation = "Test_Location";

            var person = new Person("Test", "User", "Male", DateOnly.MinValue, null, DateOnly.MinValue, null);
            var recordBirthCommand = new RecordBirthCommand(id, birthDate, birthLocation);

            var recordBirthHandler = 
                new RecordBirthCommandHandler(_personCommandServiceMock.Object,
                _personQueryServiceMock.Object, _loggerMock.Object);

            _personQueryServiceMock.Setup(x => x.GetPersonById(It.IsAny<PersonId>()))
                .ReturnsAsync(person);
            
            //Act
            var result = await recordBirthHandler.Handle(recordBirthCommand, default);

            //Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(Result.Updated);
        }

        [Fact]
        public async Task Handle_Should_Call_Person_Command_Service()
        {
            //Arrange
            var id = Guid.NewGuid().ToString();
            var birthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
            var birthLocation = "Test_Location";

            var person = new Person("Test", "User", "Male", DateOnly.MinValue, null, DateOnly.MinValue, null);
            var recordBirthCommand = new RecordBirthCommand(id, birthDate, birthLocation);

            var recordBirthHandler =
                new RecordBirthCommandHandler(_personCommandServiceMock.Object,
                    _personQueryServiceMock.Object, _loggerMock.Object);

            _personQueryServiceMock.Setup(x => x.GetPersonById(It.IsAny<PersonId>()))
                .ReturnsAsync(person);

            //Act
            var result = await recordBirthHandler.Handle(recordBirthCommand, default);

            //Assert
            _personCommandServiceMock.Verify(
                x => x.RecordBirth(It.Is<Person>(m => m.BirthDate == birthDate && m.BirthLocation == birthLocation)),
                Times.Once);
        }
    }
}