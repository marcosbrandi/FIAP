using System.ComponentModel.DataAnnotations;

namespace TechChallengeFIAP.Core.Entities
{
    public class Telefone : BaseEntity
    {
        [Range(11, 99, ErrorMessage = "DDD Inválido!")]
        public required string DDD { get; set; }

        [Range(200000000, 999999999, ErrorMessage = "Telefone Inválido!")]
        public required string Numero { get; set; }
        public string TelefoneCompleto => $"({DDD}) {Numero}";

    }
}
