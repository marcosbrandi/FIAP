using Microsoft.EntityFrameworkCore;
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

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FiapDbContext>()
             .UseInMemoryDatabase(databaseName: "TestDatabase")
             .Options;

            var dbContext = new FiapDbContext(options);

            _contatoRepository = new ContatoRepository(dbContext);
        }

        [Test]
        public async Task AdicionarEntidade_DeveAdicionarNoBancoDeDados()
        {
            // Arrange
            var entity = new Contato { Nome = "Julio", Email = "julio@fiap.com", Telefone = 
                new Telefone { DDD = "19", Numero = "999645350" } };

            // Act
            await _contatoRepository.AddAsync(entity);

            //Assert
            var contatos = await _contatoRepository.GetAllAsync(null);
            Assert.IsTrue(contatos.Any(c => c.Nome == "Julio"));
        }

        [Test]
        public async Task AtualizarEntidade_DeveAtualizarNoBancoDeDados()
        {
            // Arrange
            var entity = new Contato { Nome = "Valterlei", Email = "valterlei@fiap.com", 
                Telefone = new Telefone { DDD = "11", Numero = "999852244" }
            };

            await _contatoRepository.AddAsync(entity);

            entity.Email = "novoemail@fiap.com";

            // Act
            await _contatoRepository.UpdateAsync(entity);

            // Assert
            var contatoAtualizado = await _contatoRepository.FindAsync(entity.Id);
            Assert.AreEqual("novoemail@fiap.com", contatoAtualizado.Email);
        }

        [Test]
        public async Task ExcluirEntidade_DeveExcluirDoBancoDeDados()
        {
            // Arrange
            var contato = new Contato
            {
                Nome = "Gustavo",
                Email = "gustavo@fiap.com",
                Telefone = new Telefone { DDD = "11", Numero = "999852244" }
            };
            await _contatoRepository.AddAsync(contato);

            // Act
            await _contatoRepository.DeleteAsync(contato);

            // Assert
            var contatos = await _contatoRepository.GetAllAsync(null);
            Assert.IsFalse(contatos.Any(c => c.Nome == "Gustavo"));
        }


        [Test]
        public async Task ContarEntidades_DeveRetornarQuantidadeCorreta()
        {
            // Arrange
            var contato1 = new Contato
            {
                Nome = "Marcos",
                Email = "marcos@fiap.com",
                Telefone = new Telefone { DDD = "19", Numero = "998742233" }
            };

            var contato2 = new Contato
            {
                Nome = "Jhonas",
                Email = "jhonas@fiap.com",
                Telefone = new Telefone { DDD = "15", Numero = "999874412" }
            };

            await _contatoRepository.AddAsync(contato1);
            await _contatoRepository.AddAsync(contato2);

            // Act
            var count = await _contatoRepository.CountAsync();

            // Assert
            Assert.AreEqual(4, count);
        }
    }
}