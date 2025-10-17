using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Usuario.Queries;

public record GetUsuarioByIdQuery(int id): IRequest<UsuarioDto>;