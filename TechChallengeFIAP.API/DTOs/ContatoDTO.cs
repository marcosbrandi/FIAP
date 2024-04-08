using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.DTOs
{
    public record ContatoDto(int Id, string Nome, Telefone Telefone, string eMail);

}
