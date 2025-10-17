using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tarefas.Command;

public record UpdateTarefaCommand: IRequest<bool>
{
    public int TarefaId { get; set; }
    public int ProjetoId { get; set; }
    public int UsuarioId { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public int Status { get; set; }
    public int Prioridade { get; set; }
    public DateTime? DataVencimento { get; set; }
    
}