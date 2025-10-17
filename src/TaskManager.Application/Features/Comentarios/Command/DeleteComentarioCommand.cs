using MediatR;

namespace TaskManager.Application.Features.Comentarios.Command;

public record DeleteComentarioCommand(int comentarioId): IRequest<bool>;