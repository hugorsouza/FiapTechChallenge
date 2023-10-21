using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Interfaces.Repository;
using FluentValidation;

namespace Ecommerce.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUsuarioManager _usuarioManager;
    private readonly IValidator<CadastroClienteModel> _validatorCadastro;
    
    public ClienteService(IClienteRepository clienteRepository, IValidator<CadastroClienteModel> validatorCadastro, IUsuarioManager usuarioManager)
    {
        _clienteRepository = clienteRepository;
        _validatorCadastro = validatorCadastro;
        _usuarioManager = usuarioManager;
    }

    public async Task<ClienteViewModel> Cadastrar(CadastroClienteModel model)
    {
        await _validatorCadastro.ValidateAsync(model);
        model.Cpf = string.Join("", model.Cpf.Where(char.IsDigit).ToArray());
        
        var usuario = _usuarioManager.CriarUsuarioParaCliente(model);
        var cliente = BuildCliente(model, usuario);
        await _clienteRepository.Inserir(cliente);
        
        var clienteViewModel = new ClienteViewModel
        {
            Cpf = cliente.Cpf,
            Nome = cliente.Nome,
            Sobrenome = cliente.Sobrenome,
            DataNascimento = cliente.DataNascimento,
            RecebeNewsletterEmail = cliente.RecebeNewsletterEmail,
            Usuario = new UsuarioViewModel
            {
                Email = cliente.Usuario.Email,
                NomeExibicao = cliente.Usuario.NomeExibicao
            }
        };
        return clienteViewModel;
    }
    
    private Cliente BuildCliente(CadastroClienteModel model, Usuario usuario)
    {
        var cliente = new Cliente(
            nome: model.Nome,
            sobrenome: model.Sobrenome,
            cpf: model.Cpf,
            dataNascimento: model.DataNascimento,
            usuario: usuario
        );
        return cliente;
    }
}