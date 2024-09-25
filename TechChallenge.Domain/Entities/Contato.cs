using Fiap.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Core.Entities
{
    public class Contato : Entity, IAggregateRoot
    {
        public Contato(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public void Update(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        [Required(ErrorMessage = "Nome não informado", AllowEmptyStrings = false)]
        [Display(Name = "Nome", Description = "Informe o Nome do Contato.")]
        [StringLength(100, ErrorMessage = "O campo Nome permite até 100 caracteres")]
        public string Nome { get; private set; }

        [Required(ErrorMessage = "e-Mail não informado", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "e-Mail em formato inválido.")]
        [StringLength(200, ErrorMessage = "O campo e-Mail permite até 100 caracteres")]
        public string Email { get; private set; }
        public Guid? TelefoneId { get; private set; }
        public Telefone? Telefone { get; private set; }


        public void TrocarEmail(string email)
        {
            Email = email;
        }
    }
}
