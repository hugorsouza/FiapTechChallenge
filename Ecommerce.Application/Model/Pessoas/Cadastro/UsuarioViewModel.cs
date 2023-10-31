namespace Ecommerce.Application.Model.Pessoas.Cadastro;

public class UsuarioViewModel
{
    public UsuarioViewModel()
    {
    }

    public UsuarioViewModel(string email, string nomeExibicao, bool ativo)
    {
        Email = email;
        NomeExibicao = nomeExibicao;
        Ativo = ativo;
    }

    public string NomeExibicao { get; set; }
    public string Email { get; set; }
    public bool Ativo { get; set; }
}