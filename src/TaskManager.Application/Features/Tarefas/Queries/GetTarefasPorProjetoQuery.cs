using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Tarefas.Queries;

public record GetTarefasPorProjetoQuery(int projetoId): IRequest<List<TarefaDto>>;