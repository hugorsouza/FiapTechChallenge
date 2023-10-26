
using System.Globalization;
using Ecommerce.API.Extensions;
using Ecommerce.API.Middleware;
using Ecommerce.Application;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Application.Services.Interfaces.Pessoas;
using Ecommerce.Infra.Auth.Extensions;
using Ecommerce.Infra.Dapper.Extensions;
using Ecommerce.Infra.Dapper.Seed;
using Ecommerce.Infra.Logging.Logging;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");
builder.Services.AddControllers();
builder.Services.AddDocumentacaoApi();

builder.Services
    .AddRepositories()
    .AddAutenticacaoJwt(builder.Configuration)
    .AddValidatorsFromAssemblyContaining<IApplicationAssemblyMarker>()
    .AddScoped<IFazerPedidoService, FazerPedidoService>()
    .AddScoped<IClienteService, ClienteService>()
    .AddScoped<IFuncionarioService, FuncionarioService>()
    .AddScoped<ExceptionMiddleware>()
    .AddAppServices();
builder.Logging.ClearProviders()
    .AddProvider(new CustomLoggerProvider( new CustomLoggerProviderConfiguration(), builder.Configuration));



var app = builder.Build();

app.UseDocumentacaoApi();
if (app.Environment.IsDevelopment())
{
    app.SeedDatabase();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
