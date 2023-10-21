namespace Ecommerce.Application.Model.Pessoas;

public class ClienteViewModel
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    public UsuarioViewModel Usuario { get; set; }
    public bool RecebeNewsletterEmail { get; set; }
}