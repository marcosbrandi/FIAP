using System.ComponentModel.DataAnnotations;

public class NovoContato
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}