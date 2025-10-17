using MediatR;

namespace TaskManager.Application.Features.Comentarios.Command;

public record AddComentarioCommand: IRequest<int>
{
    public int TarefaId { get; set; }
    public int UsuarioId { get; set; }
    public string Texto { get; set; } = string.Empty;
}