using Ecommerce.Application.Configuration;
using FluentValidation;
using FluentValidation.Validators;

namespace Ecommerce.Application.Validations.Propriedades;

public class SenhaValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private static readonly string _defaultMessage = "Senha inválida";
    private readonly string _currentErrorMessage = null;
    public SenhaValidator()
    {

    }

    public SenhaValidator(string errorMessage)
    {
        _currentErrorMessage = errorMessage;
    }
    
    public override string Name => "SenhaValidator";
    
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        try
        {
            var senha = value as string;
            return !string.IsNullOrWhiteSpace(senha) && senha.Length >= 6 && senha.Length <= 255;
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