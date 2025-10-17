using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public class Projeto
{
    public Projeto()
    {
        Tarefas = new List<Tarefa>();
    }
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool PodeRemover() => Tarefas.Any(t => t.Status != (int)StatusTarefa.Pendente);
    public bool PodeAdicionarTarefa() => Tarefas.Count < 20;
    public Usuario Usuario { get; set; }
    public List<Tarefa> Tarefas { get; set; } = new();
}