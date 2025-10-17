using TaskManager.Application.Common.Extensions;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Dtos;

public record TarefaDto
{
    public int Id { get; set; }
    public int ProjetoId { get; set; }
    public string ProjetoNome { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataVencimento { get; set; }
    public string Status { get; set; } = StatusTarefa.Pendente.GetDescription();
    public string Prioridade { get; set; } = PrioridadeTarefa.Baixa.GetDescription();
    public string NomeCriador { get; set; }
}