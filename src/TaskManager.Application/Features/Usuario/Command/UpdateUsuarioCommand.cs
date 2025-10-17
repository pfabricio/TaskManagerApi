using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Usuario.Command;

public class UpdateUsuarioCommand: IRequest<UsuarioDto>
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public int Funcao { get; set; }
}