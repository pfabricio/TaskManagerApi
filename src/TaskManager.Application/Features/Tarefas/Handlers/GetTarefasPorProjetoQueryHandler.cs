using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Tarefas.Queries;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Tarefas.Handlers;

public class GetTarefasPorProjetoQueryHandler: IRequestHandler<GetTarefasPorProjetoQuery, List<TarefaDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetTarefasPorProjetoQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<TarefaDto>> Handle(GetTarefasPorProjetoQuery request, CancellationToken cancellationToken)
    {
        var tarefas = await _unitOfWork.TarefaRepository.GetByProjetoIdAsync(request.projetoId);
        if (tarefas == null)
            throw new NotFoundException("Projeto não encontrado ou sem tarefas.");

        return tarefas.Select(t => new TarefaDto
        {
            Id = t.Id,
            ProjetoId = t.ProjetoId,
            Titulo = t.Titulo,
            Descricao = t.Descricao,
            ProjetoNome = t.Projeto.Nome,
            NomeCriador = t.Usuario.Nome,
            Prioridade = t.Prioridade.GetDescriptionFromValue<PrioridadeTarefa>(),
            Status = t.Status.GetDescriptionFromValue<StatusTarefa>(),
            DataVencimento = t.DataVencimento
        }).ToList();
    }
}