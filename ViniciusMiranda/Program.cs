using System.Security.Cryptography;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViniciusMiranda.Data;
using ViniciusMiranda.Models;
using ViniciusMiranda.Schemas;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/funcionario/cadastrar", async (AddFuncionario funcionario, AppDbContext db) =>
{
    Funcionario novoFuncionario = new Funcionario
    {
        CPF = funcionario.CPF,
        Nome = funcionario.Nome
    };
    await db.AddAsync(novoFuncionario);
    await db.SaveChangesAsync();

    return Results.Created();
});
app.MapGet("/api/funcionario/listar", async (AppDbContext db) =>
{
    var funcionarios = await db.Funcionarios.ToListAsync();

    return Results.Ok(funcionarios);
});
app.MapPost("/api/folha/cadastrar", async ([FromBody] AddFolhaPagamento folhaPagamento, AppDbContext db) =>
{

    var funcionario = await db.Funcionarios.FirstOrDefaultAsync(f => f.FuncionarioId == folhaPagamento.FuncionarioId);

    if (funcionario is null) return Results.NotFound();

    FolhaPagamento novaFolhaPagamento = new FolhaPagamento
    {
        Valor = folhaPagamento.Valor,
        Quantidade = folhaPagamento.Quantidade,
        Ano = folhaPagamento.Ano,
        Mes = folhaPagamento.Mes,
        FuncionarioId = folhaPagamento.FuncionarioId
    };
    await db.AddAsync(novaFolhaPagamento);
    await db.SaveChangesAsync();

    return Results.Created();
});

app.MapGet("/api/folha/listar", async (AppDbContext db) =>
{
    var folhasPagamentos = await db.FolhasPagamentos.Include(f => f.Funcionario).ToListAsync();

    if (folhasPagamentos is null) return Results.NotFound();

    return Results.Ok(folhasPagamentos);
});

app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", (string cpf, int mes, int ano, AppDbContext db) =>
{
    //Include depois
    var folhaPagamento = db.FolhasPagamentos
    .Include(f => f.Funcionario)
    .FirstOrDefault(f => f.Funcionario.CPF.Equals(cpf) && f.Mes == mes && f.Ano == ano);

    if (folhaPagamento is null) return Results.NotFound();

    return Results.Ok(folhaPagamento);
});

decimal CalcularSalarioBruto(int horasTrabalhadas, decimal valorDaHora) =>
    horasTrabalhadas * valorDaHora;

decimal CalcularImpostoDeRenda(decimal salarioBruto)
{
    return 0;
}

app.Run();
