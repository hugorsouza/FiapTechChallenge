using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.Infra.Auth.Services;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Infra.Auth.Configuration;
using Ecommerce.Infra.Auth.Interfaces;
using Ecommerce.Infra.Auth.Jwt;

namespace Ecommerce.Infra.Auth.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutenticacaoJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services
                //.AddMemoryCache()
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
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .AddRequirements()
                    .RequireAuthenticatedUser()
                    .Build());
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
                .AddScoped<IJwtFactory, JwtFactory>()
                .AddScoped<IAutenticacaoService, AutenticacaoService>();
            return services;
        }
    }
}
