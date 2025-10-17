using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Projetos.Command;

public record CreateProjetoCommand: IRequest<ProjetoDto>
{
    public string Nome { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
}