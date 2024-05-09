using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Data;
using TechChallengeFIAP.Infrastracture.Repositories;

namespace TechChallengeFIAP.Testes
{
    [TestFixture]
    public class GenericRepositoryContatoTestes
    {
        private IGenericRepository<Contato> _contatoRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FiapDbContext>()
             .UseInMemoryDatabase(databaseName: "TestDatabase")
             .Options;

            var dbContext = new FiapDbContext(options);

            _contatoRepository = new GenericRepository<Contato>(dbContext);
        }

        [Test]
        public async Task AdicionarEntidade_DeveAdicionarNoBancoDeDados()
        {
            // Arrange
            var telefone = new Telefone { DDD = "19", Numero = "999645350" };
            var entity = new Contato { Nome = "Julio", Email = "julio@fiap.com", Telefone = telefone };

            // Act
            await _contatoRepository.AddAsync(entity);

            //Assert
            var contatos = await _contatoRepository.GetAllAsync();
            Assert.IsTrue(contatos.Any(c => c.Nome == "Julio"));
        }

        [Test]
        public async Task AtualizarEntidade_DeveAtualizarNoBancoDeDados()
        {
            // Arrange
            var telefone = new Telefone { DDD = "11", Numero = "999852244" };
            var entity = new Contato { Nome = "Valterlei", Email = "valterlei@fiap.com", Telefone = telefone };
            await _contatoRepository.AddAsync(entity);

            entity.Email = "novoemail@fiap.com";

            // Act
            await _contatoRepository.UpdateAsync(entity);

            // Assert
            var contatoAtualizado = await _contatoRepository.GetByIdAsync(entity.Id);
            Assert.AreEqual("novoemail@fiap.com", contatoAtualizado.Email);
        }

        [Test]
        public async Task ExcluirEntidade_DeveExcluirDoBancoDeDados()
        {
            // Arrange
            var telefone = new Telefone { DDD = "12", Numero = "987745896" };
            var entity = new Contato { Nome = "Gustavo", Email = "gustavo@fiap.com", Telefone = telefone };
            await _contatoRepository.AddAsync(entity);

            // Act
            await _contatoRepository.DeleteAsync(entity);

            // Assert
            var contatoExcluido = await _contatoRepository.GetByIdAsync(entity.Id);
            Assert.IsNull(contatoExcluido);
        }

        [Test]
        public async Task ContarEntidades_DeveRetornarQuantidadeCorreta()
        {
            // Arrange
            var telefone1 = new Telefone { DDD = "19", Numero = "998742233" };
            var contato1 = new Contato { Nome = "Marcos", Email = "marcos@fiap.com", Telefone = telefone1 };
            var telefone2 = new Telefone { DDD = "15", Numero = "999874412" };
            var contato2 = new Contato { Nome = "Jhonas", Email = "jhonas@fiap.com", Telefone = telefone2 };

            await _contatoRepository.AddAsync(contato1);
            await _contatoRepository.AddAsync(contato2);

            // Act
            var count = await _contatoRepository.CountAsync();

            // Assert
            Assert.AreEqual(4, count);
        }
    }
}
