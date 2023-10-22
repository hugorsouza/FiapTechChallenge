using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repository;
using FluentValidation;

namespace Ecommerce.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUsuarioManager _usuarioManager;
    private readonly IValidator<CadastroClienteModel> _validatorCadastro;
    private readonly IValidator<AlterarClienteModel> _validatorAlteracao;
    private readonly ITransactionService _transactionService;
    
    public ClienteService(IClienteRepository clienteRepository,
        IValidator<CadastroClienteModel> validatorCadastro,
        IUsuarioManager usuarioManager,
        ITransactionService transactionService,
        IValidator<AlterarClienteModel> validatorAlteracao)
    {
        _clienteRepository = clienteRepository;
        _validatorCadastro = validatorCadastro;
        _usuarioManager = usuarioManager;
        _transactionService = transactionService;
        _validatorAlteracao = validatorAlteracao;
    }

    public async Task<ClienteViewModel> Cadastrar(CadastroClienteModel model)
    {
        await _validatorCadastro.ValidateAsync(model);
        model.Cpf = model.ObterCpfSemFormatacao();
        
        _transactionService.BeginTransaction();
        
        var usuario = _usuarioManager.CadastrarUsuario(_usuarioManager.BuildUsuarioParaCliente(model));
        var cliente = BuildCliente(model, usuario);
        _clienteRepository.Cadastrar(cliente);
        
        _transactionService.Commit();
        
        var clienteViewModel = BuildViewModel(cliente);
        return clienteViewModel;
    }

    public async Task<ClienteViewModel> ObterPorId(int id)
    {
        var cliente = _clienteRepository.ObterPorId(id);
        return BuildViewModel(cliente);
    }

    public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
    {
        var clientes = _clienteRepository.ObterTodos();
        return clientes.Select(x => BuildViewModel(x));
    }

    public async Task<ClienteViewModel> Alterar(AlterarClienteModel model)
    {
        await _validatorAlteracao.ValidateAsync(model);
        var agora = DateTime.Now;
        var usuario = _usuarioManager.ObterUsuarioAtual();
        var cliente = usuario.Cliente;
        cliente.Nome = model.Nome;
        cliente.Sobrenome = model.Sobrenome;
        cliente.DataNascimento = model.DataNascimento;
        cliente.DataAlteracao = agora;

        usuario.DataAlteracao = agora;
        usuario.NomeExibicao = cliente.NomeExibicao();
        
        _transactionService.BeginTransaction();
        _clienteRepository.Alterar(cliente);
        _usuarioManager.Alterar(usuario);
        _transactionService.Commit();
        
        var clienteViewModel = BuildViewModel(cliente);
        return clienteViewModel;
    }
    
    private Cliente BuildCliente(CadastroClienteModel model, Usuario usuario)
    {
        var cliente = new Cliente(
            usuarioId: usuario.Id,
            nome: model.Nome,
            sobrenome: model.Sobrenome,
            cpf: model.Cpf,
            dataNascimento: model.DataNascimento,
            usuario: usuario
        );
        return cliente;
    }
    
    public ClienteViewModel BuildViewModel(Cliente cliente)
    {
        var clienteViewModel = new ClienteViewModel(cpf: cliente.Cpf, nome: cliente.Nome, sobrenome: cliente.Sobrenome,
            dataNascimento: cliente.DataNascimento, recebeNewsletterEmail: cliente.RecebeNewsletterEmail,
            usuario: cliente.Usuario is null
                ? null
                : new UsuarioViewModel(email: cliente.Usuario.Email, nomeExibicao: cliente.Usuario.NomeExibicao));
        return clienteViewModel;
    }
}