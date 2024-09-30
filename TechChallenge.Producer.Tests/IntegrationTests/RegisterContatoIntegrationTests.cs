using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Fiap.Core.Messages.Integration;
using NetDevPack.Utilities;

namespace TechChallenge.Producer.Tests.Integration
{
    [TestFixture]
    public class RegisterContatoIntegrationTests : IDisposable
    {
        private RabbitMqTestHelper _rabbitMqHelper;

        [SetUp]
        public void Setup()
        {
            _rabbitMqHelper = new RabbitMqTestHelper();
            _rabbitMqHelper.DeclareQueue("Test_contato_queue"); // Declare a fila
        }

        [Test]
        public async Task Should_PublishAndConsumeMessage_Successfully()
        {
            // Arrange
            var contato = new NovoContato
            {
                Nome = "Julio",
                Email = "julio@hotmail.com"
            };

            // Act
            _rabbitMqHelper.PublishMessage("Test_contato_queue", contato); // Publica o objeto NovoContato
            var receivedMessage = _rabbitMqHelper.ConsumeMessage("Test_contato_queue"); // Consome a mensagem

            // Assert
            Assert.IsNotNull(receivedMessage);
            Assert.AreEqual(contato.Nome, receivedMessage.Nome);
            Assert.AreEqual(contato.Email, receivedMessage.Email);
        }

        [TearDown]
        public void TearDown()
        {
            _rabbitMqHelper?.Dispose();
        }

        public void Dispose()
        {
            TearDown();
        }
    }
}
