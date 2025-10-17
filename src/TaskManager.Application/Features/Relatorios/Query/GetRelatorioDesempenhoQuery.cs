using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Relatorios.Query;

public record GetRelatorioDesempenhoQuery(int usuarioId) : IRequest<List<RelatorioDesempenhoDto>>;
