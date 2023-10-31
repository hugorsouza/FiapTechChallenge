using Ecommerce.Application.Configuration;
using FluentValidation;
using FluentValidation.Validators;

namespace Ecommerce.Application.Validations.Propriedades;

public class CnpjValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    
    private static readonly string _defaultMessage = "CNPJ inválido";
    private readonly string _currentErrorMessage = null;
    public CnpjValidator()
    {
        
    }
    
    public CnpjValidator(string errorMessage)
    {
        _currentErrorMessage = errorMessage;
    }
    
    public override string Name => "CnpjValidator";
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        try
        {
            var cnpj = value as string;
            return !string.IsNullOrWhiteSpace(cnpj) && ValidarDocumento.IsCnpj(cnpj);
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