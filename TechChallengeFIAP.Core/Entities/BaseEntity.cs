using System.ComponentModel.DataAnnotations;

namespace TechChallengeFIAP.Core.Entities
{
    public class BaseEntity
    {

        [Key]
        public int Id { get; set; }
        
    }
}
