using Ecommerce.Application.Configuration;
using FluentValidation;
using FluentValidation.Validators;

namespace Ecommerce.Application.Validations.Propriedades;

public class CpfValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private static readonly string _defaultMessage = "CPF inválido";
    private readonly string _currentErrorMessage = null;
    public CpfValidator()
    {

    }

    public CpfValidator(string errorMessage)
    {
        _currentErrorMessage = errorMessage;
    }
    
    public override string Name => "CpfValidator";
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        try
        {
            var cpf = value as string;
            return !string.IsNullOrWhiteSpace(cpf) && ValidarDocumento.IsCpf(cpf);
        }
        catch
        {
            return false;
        }
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return string.IsNullOrWhiteSpace(_currentErrorMessage) ? _defaultMessage : _currentErrorMessage;
    }
}