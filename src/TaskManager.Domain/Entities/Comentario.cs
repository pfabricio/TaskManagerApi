namespace TaskManager.Domain.Entities;

public class Comentario
{
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public int UsuarioId { get; set; }
    public string Conteudo { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public Tarefa Tarefa { get; set; }
    public Usuario Usuario { get; set; }
}