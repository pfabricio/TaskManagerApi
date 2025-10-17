using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Tarefas.Queries;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Tarefas.Handlers;

public class GetTarefaQueryHandler: IRequestHandler<GetTarefaQuery, TarefaDto>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetTarefaQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TarefaDto> Handle(GetTarefaQuery request, CancellationToken cancellationToken)
    {
        var tarefas = await _unitOfWork.TarefaRepository.GetByIdAsync(request.tarefaId);
        if (tarefas == null)
            throw new NotFoundException("Tarefa não encontrada.");

        return new TarefaDto
        {
            Id = tarefas.Id,
            ProjetoId = tarefas.ProjetoId,
            Titulo = tarefas.Titulo,
            Descricao = tarefas.Descricao,
            ProjetoNome = tarefas.Projeto.Nome,
            NomeCriador = tarefas.Usuario.Nome,
            Prioridade = tarefas.Prioridade.GetDescriptionFromValue<PrioridadeTarefa>(),
            Status = tarefas.Status.GetDescriptionFromValue<StatusTarefa>(),
            DataVencimento = tarefas.DataVencimento
        };
    }
}