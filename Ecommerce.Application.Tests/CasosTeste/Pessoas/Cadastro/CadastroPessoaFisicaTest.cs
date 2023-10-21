using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Validations.Pessoas;
using Ecommerce.Application.Validations.Pessoas.Cadastro;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;

namespace Ecommerce.Application.Tests.CasosTeste.Pessoas;

public class CadastroPessoaFisicaTest
{
    public CadastroPessoaFisicaTest()
    {
        
    }

    [Fact]
    public async Task Dado_CadastroClienteVazio_DeveValidar()
    {
        var sut = new CadastroClienteValidator();
        var model = new CadastroClienteModel();
        DeveConterValidacoesCliente(await sut.TestValidateAsync(model));
    }
    
    [Fact]
    public async Task Dado_CadastroFuncionarioVazio_DeveValidar()
    {
        var sut = new CadastroFuncionarioValidator();
        var model = new CadastroFuncionarioModel();
        DeveConterValidacoesFuncionario(await sut.TestValidateAsync(model));
    }
    
    [Fact]
    public async Task Dado_CadastroPessoaVazia_DeveValidar()
    {
        var sut = new CadastroPessoaFisicaValidator();
        var modelCliente = new CadastroClienteModel() as CadastroPessoaModelBase;
        var modelFuncionario = new CadastroClienteModel() as CadastroPessoaModelBase;
        DeveConterValidacoesPessoaFisica(await sut.TestValidateAsync(modelCliente));
        DeveConterValidacoesPessoaFisica(await sut.TestValidateAsync(modelFuncionario));
    }

    private void DeveConterValidacoesCliente(TestValidationResult<CadastroClienteModel> resultado)
    {
        DeveConterValidacoesPessoaFisica(resultado);
        //Adicionar propriedades exclusivas aqui
    }
    
    private void DeveConterValidacoesFuncionario(TestValidationResult<CadastroFuncionarioModel> resultado)
    {
        DeveConterValidacoesPessoaFisica(resultado);
        resultado.ShouldHaveValidationErrorFor(x => x.Cargo);
    }
    
    private void DeveConterValidacoesPessoaFisica<T>(TestValidationResult<T> resultado)
        where T : CadastroPessoaModelBase
    {
        resultado.ShouldHaveValidationErrorFor(x => x.Email);
        resultado.ShouldHaveValidationErrorFor(x => x.Senha);
        resultado.ShouldHaveValidationErrorFor(x => x.Cpf);
        resultado.ShouldHaveValidationErrorFor(x => x.Senha);
        resultado.ShouldHaveValidationErrorFor(x => x.Nome);
        resultado.ShouldHaveValidationErrorFor(x => x.Sobrenome);
        resultado.ShouldHaveValidationErrorFor(x => x.DataNascimento);
    }
}