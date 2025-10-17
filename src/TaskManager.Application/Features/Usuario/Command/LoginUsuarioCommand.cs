using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Usuario.Command;

public record LoginUsuarioCommand : IRequest<AuthResponseDto>
{
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
