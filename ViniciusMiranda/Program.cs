using System.Runtime.InteropServices;
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

    decimal salarioBruto = CalcularSalarioBruto(folhaPagamento.Quantidade, folhaPagamento.Valor);
    decimal descontoFGTS = CalcularDescontoFGTS(salarioBruto);
    decimal descontoINSS = CalcularDescontoINSS(salarioBruto);
    decimal impostoDeRenda = CalcularImpostoDeRenda(salarioBruto);
    decimal salarioLiquido = CalcularSalarioLiquido(salarioBruto);

    FolhaPagamento novaFolhaPagamento = new FolhaPagamento
    {
        Valor = folhaPagamento.Valor,
        Quantidade = folhaPagamento.Quantidade,
        Ano = folhaPagamento.Ano,
        Mes = folhaPagamento.Mes,
        FuncionarioId = folhaPagamento.FuncionarioId,
        SalarioBruto = salarioBruto,
        ImpostoFgts = descontoFGTS,
        ImpostoIrrf = impostoDeRenda,
        ImpostoInss = descontoINSS,
        SalarioLiquido = salarioLiquido,
    };
    await db.AddAsync(novaFolhaPagamento);
    await db.SaveChangesAsync();

    return Results.Ok(novaFolhaPagamento);
});

app.MapGet("/api/folha/listar", async (AppDbContext db) =>
{
    var folhasPagamentos = await db.FolhasPagamentos.Include(f => f.Funcionario).ToListAsync();

    if (folhasPagamentos is null) return Results.NotFound();

    return Results.Ok(folhasPagamentos);
});

app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", (string cpf, int mes, int ano, AppDbContext db) =>
{
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
    decimal impostoASerPago = 0;
    if (salarioBruto <= (decimal)1903.98)
    {
        impostoASerPago = 0;
    }
    else if (salarioBruto >= (decimal)1903.99 && salarioBruto <= (decimal)2826.65)
    {
        impostoASerPago = (salarioBruto * (decimal)0.075) + (decimal)142.80;
    }
    else if (salarioBruto >= (decimal)2826.66 && salarioBruto <= (decimal)3751.05)
    {
        impostoASerPago = (salarioBruto * (decimal)0.15) + (decimal)354.80;
    }
    else if (salarioBruto >= (decimal)3751.06 && salarioBruto <= (decimal)4664.68)
    {
        impostoASerPago = (salarioBruto * (decimal)0.225) + (decimal)636.13;
    }
    else
    {
        impostoASerPago = (salarioBruto * (decimal)0.275) + (decimal)869.36;
    }
    return impostoASerPago;
}
decimal CalcularDescontoINSS(decimal salarioBruto)
{
    decimal desconto = 0;
    if (salarioBruto <= (decimal)1693.72)
    {
        desconto = salarioBruto * (decimal)0.08;
    }
    else if (salarioBruto >= (decimal)1693.73 && salarioBruto <= (decimal)2822.90)
    {
        desconto = salarioBruto * (decimal)0.09;
    }
    else if (salarioBruto >= (decimal)2822.91 && salarioBruto <= (decimal)5645.80)
    {
        desconto = salarioBruto * (decimal)0.11;
    }
    else
    {
        desconto = salarioBruto - (decimal)621.03;
    }
    return desconto;
}
decimal CalcularDescontoFGTS(decimal salarioBruto) =>
    salarioBruto * (decimal)0.08;

decimal CalcularSalarioLiquido(decimal salarioBruto) =>
    salarioBruto - CalcularImpostoDeRenda(salarioBruto) - CalcularDescontoINSS(salarioBruto);
app.Run();
