using MediatR;
using TaskManager.Application.Dtos;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tarefas.Command;

public record CreateTarefaCommand : IRequest<TarefaDto>
{
    public int ProjetoId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataVencimento { get; set; }
    public PrioridadeTarefa Prioridade { get; set; } = PrioridadeTarefa.Media;
    public int UsuarioId { get; set; }
}