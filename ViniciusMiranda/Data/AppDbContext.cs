using Microsoft.EntityFrameworkCore;
using ViniciusMiranda.Models;

namespace ViniciusMiranda.Data;
public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
        var caminho = AppContext.BaseDirectory;
        CaminhoDb = Path.Combine(caminho, "nicollaskvasnei_viniciusmiranda.db");
    }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }
    public virtual DbSet<FolhaPagamento> FolhasPagamentos { get; set; }
    public virtual string CaminhoDb { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={CaminhoDb}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}