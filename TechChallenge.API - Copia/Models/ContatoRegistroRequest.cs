using System.ComponentModel.DataAnnotations;

namespace Fiap.Identidade.API.Models
{
    public class ContatoRegistroRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; } = string.Empty;
    }

}