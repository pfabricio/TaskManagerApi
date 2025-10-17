using MediatR;
using TaskManager.Application.Dtos;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Usuario.Command;

public record AddUsuarioCommand : IRequest<UsuarioDto>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public int Funcao { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
}