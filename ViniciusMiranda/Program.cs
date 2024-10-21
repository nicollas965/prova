using Microsoft.AspNetCore.Http.HttpResults;
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
app.MapPost("/api/folha/cadastrar", async (AddFolhaPagamento folhaPagamento, AppDbContext db) =>
{
    // FolhaPagamento novaFolhaPagamento = new FolhaPagamento
    // {

    // };
    // await db.AddAsync(novoFuncionario);
    // await db.SaveChangesAsync();

    // return Results.Created();
});
app.Run();
