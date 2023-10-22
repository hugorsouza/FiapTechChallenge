using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Validations.Pessoas.Cadastro;

namespace Ecommerce.Application.Tests.CasosTeste.Pessoas.Cadastro;

public class AlteracaoPessoaFisicaTest
{
    public AlteracaoPessoaFisicaTest()
    {
        
    }

    [Fact]
    public async Task Dado_CadastroClienteVazio_DeveValidar()
    {
        var sut = new AlterarClienteValidator();
        var model = new AlterarClienteModel();
        DeveConterValidacoesCliente(await sut.TestValidateAsync(model));
    }
    
    [Fact]
    public async Task Dado_Alteracao_Funcionario_DadosVazios_DeveValidar()
    {
        var sut = new AlterarFuncionarioValidator();
        var model = new AlterarFuncionarioModel();
        DeveConterValidacoesFuncionario(await sut.TestValidateAsync(model));
    }
    
    [Fact]
    public async Task Dado_Alteracao_Pessoa_DadosVazios_DeveValidar()
    {
        var sut = new AlterarPessoaFisicaValidator();
        var modelCliente = new AlterarClienteModel() as AlterarPessoaModelBase;
        var modelFuncionario = new AlterarFuncionarioModel() as AlterarPessoaModelBase;
        DeveConterValidacoesPessoaFisica(await sut.TestValidateAsync(modelCliente));
        DeveConterValidacoesPessoaFisica(await sut.TestValidateAsync(modelFuncionario));
    }

    private void DeveConterValidacoesCliente(TestValidationResult<AlterarClienteModel> resultado)
    {
        DeveConterValidacoesPessoaFisica(resultado);
        //Adicionar propriedades exclusivas aqui
    }
    
    private void DeveConterValidacoesFuncionario(TestValidationResult<AlterarFuncionarioModel> resultado)
    {
        DeveConterValidacoesPessoaFisica(resultado);
        resultado.ShouldHaveValidationErrorFor(x => x.Cargo);
    }
    
    private void DeveConterValidacoesPessoaFisica<T>(TestValidationResult<T> resultado)
        where T : AlterarPessoaModelBase
    {
        resultado.ShouldHaveValidationErrorFor(x => x.Nome);
        resultado.ShouldHaveValidationErrorFor(x => x.Sobrenome);
        resultado.ShouldHaveValidationErrorFor(x => x.DataNascimento);
    }
}