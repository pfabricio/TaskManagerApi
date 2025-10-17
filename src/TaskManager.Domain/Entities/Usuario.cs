using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public class Usuario
{
    public Usuario()
    {
        Comentarios = new List<Comentario>();
        Tarefas = new List<Tarefa>();
        Projetos = new List<Projeto>();
        HistoricoTarefas = new List<HistoricoTarefa>();
    }

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Funcao { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public List<Comentario> Comentarios { get; set; }
    public List<HistoricoTarefa> HistoricoTarefas { get; set; }
    public List<Projeto> Projetos { get; set; }
    public List<Tarefa> Tarefas { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}