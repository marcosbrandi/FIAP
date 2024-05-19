using System.ComponentModel.DataAnnotations;

namespace TechChallengeFIAP.Core.Entities
{
    public class Contato : BaseEntity
    {
        [Required(ErrorMessage = "Nome não informado", AllowEmptyStrings = false)]
        [Display(Name = "Nome", Description = "Informe o Nome do Contato.")]
        [StringLength(100, ErrorMessage = "O campo Nome permite até 100 caracteres")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "e-Mail não informado", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "e-Mail em formato inválido.")]
        [StringLength(200, ErrorMessage = "O campo e-Mail permite até 100 caracteres")]
        public required string Email { get; set; }
        
        public required Telefone Telefone { get; set; }
        //public ICollection<Telefone>? Telefones { get; set; }

    }
}
