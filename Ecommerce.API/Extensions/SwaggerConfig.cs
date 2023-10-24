using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Ecommerce.API.Extensions
{
    public static class SwaggerConfig
    {
        private const string VersaoApi = "v1";
        private const string TituloApi = "E-commerce - Fiap Tech Challenge";
        public static IServiceCollection AddDocumentacaoApi(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(VersaoApi, new OpenApiInfo
                {
                    Title = TituloApi,
                    Version = VersaoApi
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Header de autorização utilizando JWTs. Informe 'Bearer' [espaço] e logo em seguida o seu token. Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        Array.Empty<string>()
                    }
                });
                
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            
            
            return services;
        }

        public static IApplicationBuilder UseDocumentacaoApi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseReDoc(c =>
            {
                c.DocumentTitle = $"{TituloApi} - Redoc";
                c.SpecUrl = $"/swagger/{VersaoApi}/swagger.json";
            });
            
            return app;
        }
    }
}
