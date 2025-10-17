namespace TaskManager.Domain.Entities;

public class HistoricoTarefa
{
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public int UsuarioId { get; set; }
    public string CampoModificado{ get; set; }
    public string ValorAnterior{ get; set; }
    public string NovoValor{ get; set; }
    public DateTime DataModificacao { get; set; } = DateTime.UtcNow;

    public Usuario Usuario { get; set; }
    public Tarefa Tarefa { get; set; }
}