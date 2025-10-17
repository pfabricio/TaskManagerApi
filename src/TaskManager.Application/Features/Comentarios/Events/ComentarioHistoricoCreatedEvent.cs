using MediatR;

namespace TaskManager.Application.Features.Comentarios.Events;

public record ComentarioHistoricoCreatedEvent: INotification
{
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public int UsuarioId { get; set; }
    public string NomeTarefa { get; set; }
    public string NomeUsuario { get; set; }
    public string CampoModificado { get; set; }
    public string ValorAnterior { get; set; }
    public string NovoValor { get; set; }
    public DateTime DataModificacao { get; set; } = DateTime.UtcNow;
}