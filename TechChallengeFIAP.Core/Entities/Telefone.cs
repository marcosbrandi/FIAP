using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechChallengeFIAP.Core.Entities
{
    public class Telefone : BaseEntity
    {
        //public required Contato Contato { get; set; }

        [Range(11, 99, ErrorMessage = "DDD Inválido!")]
        public required string DDD { get; set; }

        [Range(200000000, 999999999, ErrorMessage = "Telefone Inválido!")]
        public required string Numero { get; set; }
        public virtual string TelefoneCompleto => $"({DDD}) {Numero}";
        public virtual string? UF { get; set; }
        //{
        //    get
        //    {
        //        return DDDRegionService.GetInfo(DDD).state;
        //    }
        //}

    }
}
