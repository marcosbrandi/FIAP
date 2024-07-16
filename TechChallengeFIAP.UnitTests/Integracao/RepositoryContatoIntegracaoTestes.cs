using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Data;
using TechChallengeFIAP.Infrastructure.Repositories;
using TechChallengeFIAP.Infrastructure.Services;

namespace TechChallengeFIAP.Testes.Integracao
{
    [TestFixture]
    public class RepositoryContatoIntegracaoTestes
    {
        private IContatoRepository _contatoRepository;
        private FiapDbContext _dbContext;
        private IDDDRegionService _dddService;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _configuration = configuration;

            var options = new DbContextOptionsBuilder<FiapDbContext>()
                .UseNpgsql(configuration.GetConnectionString("FiapDbContextConnection"))
                .Options;

            _dbContext = new FiapDbContext(options);
            _dbContext.Database.EnsureCreated();

            var httpClient = new HttpClient(); 
            _dddService = new DDDRegionService(httpClient); 
            _contatoRepository = new ContatoRepository(_dbContext, _dddService);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task AdicionarEntidade_DeveAdicionarNoBancoDeDados()
        {
            // Arrange
            var entity = new Contato
            {
                Nome = "Julio",
                Email = "julio@fiap.com",
                Telefone = new Telefone { DDD = "11", Numero = "982598878" }
            };

            // Act
            await _contatoRepository.AddAsync(entity);

            // Assert
            var contatos = await _contatoRepository.GetAllAsync(null);
            Assert.IsTrue(contatos.Any(c => c.Nome == "Julio"));
        }
    }
}
