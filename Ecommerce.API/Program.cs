using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infra.Dapper.Interfaces;
using Ecommerce.Infra.Dapper.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IFazerPedidoService, FazerPedidoService>();
builder.Services.AddTransient<IConsultarPedidoService, ConsultarPedidoService>();
//builder.Services.AddTransient<IFazerPedidoDomainService, FazerPedidoDomainService>();
builder.Services.AddTransient<IConsultarPedidoDomainService, ConsultarPedidoDomainService>();
//builder.Services.AddTransient<IConsultarPedidoRepository, ConsultarPedidoRepository>();
builder.Services.AddTransient<IConsultarPedidoRepositoryDapper, ConsultarPedidoRepositoryDapper>(_ => new ConsultarPedidoRepositoryDapper("Ecommerce"));
//builder.Services.AddTransient<IConsultarPedidoRepositoryDapper, ConsultarPedidoRepository>();
//builder.Services.AddScoped<ConsultarPedidoRepositoryDapper>();

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
