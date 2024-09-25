using Fiap.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Core.Entities
{
    public class Telefone : Entity, IAggregateRoot
    {
        public Telefone(string ddd, string numero)
        {
            Ddd = ddd;
            Numero = numero;
        }

        [Range(11, 99, ErrorMessage = "DDD Inválido!")]
        public string Ddd { get; set; }

        [Range(200000000, 999999999, ErrorMessage = "Telefone Inválido!")]
        public string Numero { get; set; }
        public string TelefoneCompleto => $"({Ddd}) {Numero}";

    }
}
