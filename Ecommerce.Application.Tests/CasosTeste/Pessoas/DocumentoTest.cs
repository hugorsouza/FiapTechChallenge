using Bogus;
using Bogus.Extensions.Brazil;
using Ecommerce.Application.Configuration;

namespace Ecommerce.Application.Tests.CasosTeste.Pessoas;

public class DocumentoTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("123456")]
    [InlineData("000.000.000-00")]
    [InlineData("00000000000")]
    [InlineData("a")]
    [InlineData("12345678901")]
    [InlineData("123.456.789-01")]
    [InlineData("123456789012")]
    [InlineData("123.456.789-012")]
    [InlineData("1234567890123")]
    [InlineData("123.456.789-0123")]
    [InlineData("12345678901234")]
    [InlineData("123.456.789-01234")]
    [InlineData("123456789012345")]
    [InlineData("123.456.789-012345")]
    [InlineData("1234567890123456")]
    [InlineData("123.456.789-0123456")]
    [InlineData("00000000001")]
    [InlineData("000.000.000-01")]
    [InlineData("12345678901234567")]
    [InlineData("123.456.789-01234567")]
    [InlineData("123456789012345678")]
    [InlineData("123.456.789-012345678")]
    [InlineData("00000000000")]
    public void Dado_Cpf_DeveValidar(string? cpf)
    {
        ValidarDocumento.IsCpf(cpf).Should().BeFalse();
    }


    [Fact]
    public void Dado_CpfValidoComFormatacao_DeveRetornarSucesso()
    {
        var cpf = new Faker().Person.Cpf(true);
        ValidarDocumento.IsCpf(cpf).Should().BeTrue();
    }
    
    [Fact]
    public void Dado_CpfValidoSemFormatacao_DeveRetornarSucesso()
    {
        var cpf = new Faker().Person.Cpf(false);
        ValidarDocumento.IsCpf(cpf).Should().BeTrue();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("123456")]
    [InlineData("00.000.000/0000-00")]
    [InlineData("00000000000000")]
    [InlineData("a")]
    [InlineData("12345678901234")]
    [InlineData("12.345.678/9012-34")]
    [InlineData("123456789012345")]
    [InlineData("12.345.678/9012-345")]
    [InlineData("1234567890123456")]
    [InlineData("12.345.678/9012-3456")]
    [InlineData("12345678901234567")]
    [InlineData("12.345.678/9012-34567")]
    [InlineData("123456789012345678")]
    [InlineData("12.345.678/9012-345678")]
    [InlineData("00000000000001")]
    [InlineData("01.000.000/0000-00")]
    [InlineData("1234567890123456789")]
    [InlineData("12.345.678/9012-3456789")]
    [InlineData("123.456.789/0123-45")]
    [InlineData("123.456.789/0123-456")]
    [InlineData("123.456.789/0123-4567")]
    [InlineData("123.456.789/0123-45678")]
    [InlineData("12.345.678/9012-345679")]
    [InlineData("00000000000000")]
    public void Dado_Cnpj_DeveValidar(string? cnpj)
    {
        ValidarDocumento.IsCnpj(cnpj).Should().BeFalse();
    }

    
    [Fact]
    public void Dado_CnpjValidoComFormatacao_DeveRetornarSucesso()
    {
        var cnpj = new Faker().Company.Cnpj(true);
        ValidarDocumento.IsCnpj(cnpj).Should().BeTrue();
    }
    
    [Fact]
    public void Dado_CnpjValidoSemFormatacao_DeveRetornarSucesso()
    {
        var cnpj = new Faker().Company.Cnpj(false);
        ValidarDocumento.IsCnpj(cnpj).Should().BeTrue();
    }
}