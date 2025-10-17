using MediatR;

namespace TaskManager.Application.Features.Tarefas.Command;

public record DeleteTarefaCommand(int tarefaId): IRequest<bool>;