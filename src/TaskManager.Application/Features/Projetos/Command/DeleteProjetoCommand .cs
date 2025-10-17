using MediatR;

namespace TaskManager.Application.Features.Projetos.Command;

public record DeleteProjetoCommand: IRequest<bool>
{
    public int ProjetoId { get; set; }
}