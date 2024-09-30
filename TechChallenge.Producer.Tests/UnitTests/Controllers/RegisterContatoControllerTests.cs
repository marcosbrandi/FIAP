using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using TechChallenge.Producer.Controllers;
using Fiap.Core.Messages.Integration;
using Fiap.MessageBus;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Tests.UnitTests.Controllers
{
    [TestFixture]
    public class RegisterContatoControllerTests : IDisposable
    {
        private readonly Mock<IMessageBus> _messageBusMock;
        private readonly RegisterContatoController _controller;

        public RegisterContatoControllerTests()
        {
            _messageBusMock = new Mock<IMessageBus>();
            _controller = new RegisterContatoController(_messageBusMock.Object);
        }

        [Test]
        public async Task Create_ValidRequest_ShouldReturnSuccessResponse()
        {
            // Arrange
            var novoContato = new NovoContato { Nome = "Julio", Email = "juliodovale@hotmail.com" };
            var responseMessage = new ResponseMessage(new FluentValidation.Results.ValidationResult());

            _messageBusMock
                .Setup(mb => mb.RequestAsync<ContatoRegistradoIntegrationEvent, ResponseMessage>(It.IsAny<ContatoRegistradoIntegrationEvent>()))
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _controller.Create(novoContato);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        public void Dispose()
        {
            _controller?.Dispose();
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }
    }
}
