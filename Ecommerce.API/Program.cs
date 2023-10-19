using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Ecommerce.Infra.Dapper.Interfaces;
using Ecommerce.Infra.Dapper.Repositories;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IConsultarPedidoService, ConsultarPedidoService>();
builder.Services.AddTransient<IConsultarPedidoDapperService, ConsultarPedidoDapperService>();
builder.Services.AddTransient<IConsultarPedidoRepositoryDapper, ConsultarPedidoRepositoryDapper>
    (AppSettingsReader => new ConsultarPedidoRepositoryDapper(
        //"Server=.\\SQLEXPRESS01;Database=Ecommerce;TrustServerCertificate=True;User ID=TechChallenge; Password=Fiap@2000;Trusted_Connection=True"
        "ConnectionStrings:Ecommerce"));

builder.Services.AddControllers();  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
