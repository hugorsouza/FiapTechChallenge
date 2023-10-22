namespace Ecommerce.Application.Model.Pessoas.Cadastro;

public class UsuarioViewModel
{
    public UsuarioViewModel()
    {
    }

    public UsuarioViewModel(string email, string nomeExibicao)
    {
        Email = email;
        NomeExibicao = nomeExibicao;
    }

    public string NomeExibicao { get; set; }
    public string Email { get; set; }
}