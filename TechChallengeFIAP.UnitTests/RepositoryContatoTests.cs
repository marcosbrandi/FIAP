using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Data;
using TechChallengeFIAP.Infrastructure.Repositories;

namespace TechChallengeFIAP.Testes
{
    [TestFixture]
    public class RepositoryContatoTestes
    {
        private IContatoRepository _contatoRepository;
        private FiapDbContext _dbContext;
        private Mock<IDDDRegionService> _dddServiceMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FiapDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new FiapDbContext(options);
            _dddServiceMock = new Mock<IDDDRegionService>();

            _contatoRepository = new ContatoRepository(_dbContext, _dddServiceMock.Object);
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

            // Utilizado o mock para retornar um objeto DDDInfo simulado
            var dddServiceMock = new Mock<IDDDRegionService>();
            DDDInfo dddInfoMock = new DDDInfo
            {
                ddd = "11",
                state = "SP"
            };

            dddServiceMock.Setup(x => x.GetInfo("11")).ReturnsAsync(dddInfoMock);

            // Act
            _contatoRepository = new ContatoRepository(_dbContext, dddServiceMock.Object);
            await _contatoRepository.AddAsync(entity);

            // Assert
            var contatos = await _contatoRepository.GetAllAsync(null);
            Assert.IsTrue(contatos.Any(c => c.Nome == "Julio"));
        }


        [Test]
        public async Task AtualizarEntidade_DeveAtualizarNoBancoDeDados()
        {
            // Arrange      
            var currentEntity = new Contato
            {
                Nome = "Valterley",
                Email = "valterley@fiap.com",
                Telefone = new Telefone { DDD = "11", Numero = "982598878" }
            };

            var updatedEntity = new Contato
            {
                Nome = "Valterley",
                Email = "novoemail@fiap.com",
                Telefone = new Telefone { DDD = "11", Numero = "999877178" }
            };

            // Utilizado o mock para retornar um objeto DDDInfo simulado
            var dddServiceMock = new Mock<IDDDRegionService>();
            DDDInfo dddInfoMock = new DDDInfo
            {
                ddd = "11",
                state = "SP"
            };

            dddServiceMock.Setup(x => x.GetInfo("11")).ReturnsAsync(dddInfoMock);

            // Act
            await _contatoRepository.UpdateAsync(currentEntity, updatedEntity);

            // Assert
            var contatoAtualizado = await _contatoRepository.FindAsync(currentEntity.Id);
            Assert.AreEqual("novoemail@fiap.com", contatoAtualizado.Email);
        }

        [Test]
        public async Task ExcluirEntidade_DeveExcluirDoBancoDeDados()
        {
            // Arrange
            var entity = new Contato
            {
                Nome = "Gustavo",
                Email = "Gustavo@fiap.com",
                Telefone = new Telefone { DDD = "11", Numero = "982598878" }
            };

            // Utilizado o mock para retornar um objeto DDDInfo simulado
            var dddServiceMock = new Mock<IDDDRegionService>();
            DDDInfo dddInfoMock = new DDDInfo
            {
                ddd = "11",
                state = "SP"
            };

            dddServiceMock.Setup(x => x.GetInfo("11")).ReturnsAsync(dddInfoMock);

            _contatoRepository = new ContatoRepository(_dbContext, dddServiceMock.Object);
            await _contatoRepository.AddAsync(entity);

            // Act
            await _contatoRepository.DeleteAsync(entity);

            // Assert
            var contatos = await _contatoRepository.GetAllAsync(null);
            Assert.IsFalse(contatos.Any(c => c.Nome == "Gustavo"));
        }

        [Test]
        public async Task ContarEntidades_DeveRetornarQuantidadeCorreta()
        {
            // Arrange
            var entity = new Contato
            {
                Nome = "Marcos",
                Email = "marcos@fiap.com",
                Telefone = new Telefone { DDD = "11", Numero = "996554877" }
            };

            // Utilizado o mock para retornar um objeto DDDInfo simulado
            var dddServiceMock = new Mock<IDDDRegionService>();
            DDDInfo dddInfoMock = new DDDInfo
            {
                ddd = "11",
                state = "SP"
            };

            dddServiceMock.Setup(x => x.GetInfo("11")).ReturnsAsync(dddInfoMock);

            // Act
            _contatoRepository = new ContatoRepository(_dbContext, dddServiceMock.Object);
            await _contatoRepository.AddAsync(entity);


            // Act
            var count = await _contatoRepository.CountAsync();

            // Assert
            Assert.AreEqual(1, count);
        }

    }
}
