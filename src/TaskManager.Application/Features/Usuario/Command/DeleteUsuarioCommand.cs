using MediatR;

namespace TaskManager.Application.Features.Usuario.Command;

public record DeleteUsuarioCommand(int id): IRequest<bool>;