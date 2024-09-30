using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Fiap.Core.Messages.Integration;

namespace TechChallenge.Producer.Tests.Integration
{
    [TestFixture]
    public class RegisterContatoUpdateIntegrationTests : IDisposable
    {
        private RabbitMqTestHelper _rabbitMqHelper;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _rabbitMqHelper = new RabbitMqTestHelper();
            _rabbitMqHelper.DeclareQueue("Test_contato_queue"); // Declare a fila usada 

            var services = new ServiceCollection();
            services.AddHttpClient();
            var serviceProvider = services.BuildServiceProvider();
            _client = serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();
        }

        [Test]
        public async Task Should_UpdateContato_Successfully()
        {
            // Arrange
            var id = Guid.NewGuid(); 
            var atualizaContato = new AtualizaContato
            {
                Id = id,
                Nome = "Julio Atualizado",
                Email = "julioatualizado@hotmail.com"
            };
            var jsonContent = JsonConvert.SerializeObject(atualizaContato);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"https://localhost:55437/api/RegisterContato/{id}", content);

            // Assert
            response.EnsureSuccessStatusCode();
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
