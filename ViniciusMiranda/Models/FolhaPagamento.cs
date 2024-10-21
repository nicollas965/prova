namespace ViniciusMiranda.Models;

public class FolhaPagamento
{
    public long FolhaPagamentoId { get; set; }
    public decimal Valor { get; set; }
    public int Quantidade { get; set; }
    public int Mes { get; set; }
    public int Ano { get; set; }
    public int FuncionarioId { get; set; }
    public Funcionario Funcionario { get; set; }
}