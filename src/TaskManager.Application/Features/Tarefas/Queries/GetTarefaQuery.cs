using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Tarefas.Queries;

public record GetTarefaQuery(int tarefaId): IRequest<TarefaDto>;