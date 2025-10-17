using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Historicos.Queries;

public record HistoricaPorTarefaQuery(int tarefaId): IRequest<List<HistoricoDto>>;