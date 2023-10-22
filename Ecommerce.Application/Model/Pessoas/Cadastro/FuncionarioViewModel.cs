namespace Ecommerce.Application.Model.Pessoas.Cadastro;

public class FuncionarioViewModel
{
    public FuncionarioViewModel(string nome, string sobrenome, string cpf, DateTime dataNascimento, string cargo, bool administrador, UsuarioViewModel usuario)
    {
        Nome = nome;
        Sobrenome = sobrenome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Usuario = usuario;
        Cargo = cargo;
        Administrador = administrador;
    }

    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    public UsuarioViewModel Usuario { get; set; }
    public string Cargo { get; set; }
    public bool Administrador { get; set; }
}