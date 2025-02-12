using AdR.Controllers;
using AdR.DatabaseContext;
using AdR.Interfaces;
using AdR.Repositories;
using AdR.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional:true, reloadOnChange:true);
var connectionPath = builder.Configuration.GetValue<string>("DatabasePath");

builder.Services.AddDbContext<AdrDbContext>(opt=> opt.UseSqlServer(connectionPath));
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<INotaRepository, NotaRepository>();

builder.Services.AddTransient<CadastroController>();
builder.Services.AddTransient<CarrinhoController>();
builder.Services.AddTransient<CadastroService>();
builder.Services.AddTransient<CarrinhoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
