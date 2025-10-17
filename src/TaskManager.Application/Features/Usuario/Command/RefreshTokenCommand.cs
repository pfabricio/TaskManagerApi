using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Usuario.Command;

public record RefreshTokenCommand : IRequest<AuthResponseDto>
{
    public string RefreshToken { get; set; } = string.Empty;
}
