using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.Infra.Auth.Services;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Infra.Auth.Configuration;
using Ecommerce.Infra.Auth.Constants;
using Ecommerce.Infra.Auth.Interfaces;
using Ecommerce.Infra.Auth.Jwt;

namespace Ecommerce.Infra.Auth.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutenticacaoJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpContextAccessor()
                .AddOptions(configuration)
                .AddServices()
                .AddJwtSecurity(configuration);
            return services;
        }

        private static IServiceCollection AddJwtSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection(JwtConfig.AppSettingsKey).Get<JwtConfig>();
            ArgumentNullException.ThrowIfNull(jwtConfig);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = jwtConfig.GetSecurityKey();
                paramsValidation.ValidAudience = jwtConfig.Audience;
                paramsValidation.ValidIssuer = jwtConfig.Issuer;

                paramsValidation.ValidateAudience = false;
                paramsValidation.ValidateIssuer = false;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });
            
            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                const string defaultPolicyName = "Bearer";
                auth.AddPolicy(defaultPolicyName, new AuthorizationPolicyBuilder()
                    .ConfigurarPolicyAutenticadaPadrao()
                    .Build());

                auth.AddPolicy(CustomPolicies.SomenteAdministrador, new AuthorizationPolicyBuilder()
                    .ConfigurarPolicyAutenticadaPadrao()
                    .RequireRole(PerfilUsuarioExtensions.Funcionario)
                    .RequireClaim(CustomClaims.FlagAdmin, "true")
                    .Build());

                auth.DefaultPolicy = auth.GetPolicy(defaultPolicyName) 
                                     ?? throw new ArgumentException($"A política de autorização padrão [{defaultPolicyName}] não foi registrada");
            });
            return services;
        }

        private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection(JwtConfig.AppSettingsKey));
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<ISenhaHasher, BCryptSenhaHasher>()
                .AddScoped<IJwtHelper, JwtHelper>()
                .AddScoped<IAutenticacaoService, AutenticacaoService>()
                .AddScoped<IUsuarioManager, UsuarioManager>();
            return services;
        }

        private static AuthorizationPolicyBuilder ConfigurarPolicyAutenticadaPadrao(this AuthorizationPolicyBuilder authBuilder)
        {
            authBuilder.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .RequireClaim(CustomClaims.TipoToken, ((int)TipoToken.AccessToken).ToString());//Sem isso, refresh tokens seriam válidos
            return authBuilder;
        }
    }
}
