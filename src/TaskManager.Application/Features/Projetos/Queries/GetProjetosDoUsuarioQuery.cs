using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Projetos.Queries;

public record GetProjetosDoUsuarioQuery(int usuarioId): IRequest<List<ProjetoDto>>;