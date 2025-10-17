using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Historicos.Queries;

public record HistoricoPorUsuarioQuery(int usuarioId): IRequest<List<HistoricoDto>>;