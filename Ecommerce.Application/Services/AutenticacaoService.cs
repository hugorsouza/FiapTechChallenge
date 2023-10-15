using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ecommerce.Application.Model.Auth;
using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Exceptions;
using LanguageExt.Common;

namespace Ecommerce.Application.Services
{
    public class AutenticacaoService
    {
        public Result<LoginResponse> Login(LoginModel credenciais)
        {
            var resultadoValidacao = ValidarCredenciais(credenciais);
            if (resultadoValidacao is not null)
                return new Result<LoginResponse>(resultadoValidacao);

            Usuario usuario = null;
            if(usuario is null || !ChecarSenha(credenciais.Senha, ""))
                return new Result<LoginResponse>(ValidacaoException.Erro("Credenciais inválidas")); 
            
            return new LoginResponse();
        }

        public bool ChecarSenha(string senha, string hash)
        {
            return true;
        }

        private Exception ValidarCredenciais(LoginModel credenciais)
        {
            if (credenciais is null)
                return RequisicaoInvalidaException.CorpoInvalido();

            if (!EmailValido(credenciais.Email))
                return ValidacaoException.Erro("E-mail inválido");
            
            if(string.IsNullOrWhiteSpace(credenciais.Senha))
                return ValidacaoException.Erro("Senha inválida");

            return null;
        }
        
        public static bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
