using MediatR;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Historicos.Queries;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Historicos.Handlers;

public class HistoricoPorTarefaQueryHandler: IRequestHandler<HistoricaPorTarefaQuery, List<HistoricoDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public HistoricoPorTarefaQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<HistoricoDto>> Handle(HistoricaPorTarefaQuery request, CancellationToken cancellationToken)
    {
        var historicos = await _unitOfWork.HistoricoTarefaRepository.GetHistoricoByTarefa(request.tarefaId);
        return historicos.Select(p => new HistoricoDto
        {
            Id = p.Id,
            TarefaId = p.TarefaId,
            UsuarioId = p.UsuarioId,
            NomeTarefa = p.Tarefa.Descricao,
            NomeUsuario = p.Usuario.Nome,
            CampoModificado = p.CampoModificado,
            ValorAnterior = p.ValorAnterior,
            NovoValor = p.NovoValor,
            DataModificacao = p.DataModificacao
        }).ToList();
    }
}