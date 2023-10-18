using System.Globalization;
using Ecommerce.API.Extensions;
using Ecommerce.API.Middleware;
using Ecommerce.Application;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.Auth.Extensions;
using Ecommerce.Infra.Dapper.Extensions;
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
    .AddScoped<ExceptionMiddleware>()
    .AddAppServices();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
