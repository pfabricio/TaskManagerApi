using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Usuario.Queries;
public record GetAllUsuariosQuery : IRequest<List<UsuarioDto>>;