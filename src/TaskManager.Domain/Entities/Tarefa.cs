using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;


public class Tarefa
{
    public Tarefa()
    {
        Comentarios = new List<Comentario>();
        Historicos = new List<HistoricoTarefa>();
    }
    public int Id { get; set; }
    public int ProjetoId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataVencimento { get; set; }
    public int Status { get; set; }
    public int Prioridade { get; set; }
    public int UsuarioId { get; set; }
    public DateTime CriadoEm { get; set; }
    public Usuario Usuario { get; set; }
    public Projeto Projeto { get; set; }
    public List<Comentario> Comentarios { get; set; } = new();
    public List<HistoricoTarefa> Historicos { get; set; } = new();
}