using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Features.Tarefas.Command;
using TaskManager.Application.Features.Tarefas.Events;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Tarefas.Handlers;

public class UpdateTarefaCommandHandler: IRequestHandler<UpdateTarefaCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public UpdateTarefaCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<bool> Handle(UpdateTarefaCommand request, CancellationToken cancellationToken)
    {
        var tarefa = await _unitOfWork.TarefaRepository.GetByIdAsync(request.TarefaId);

        if (tarefa == null)
            if (tarefa == null)
                throw new NotFoundException("Tarefa");

        // Verifica modificações e publica eventos por campo alterado
        if (tarefa.Prioridade != request.Prioridade)
        {
            throw new BusinessRuleException("A prioridade da tarefa não pode ser alterada após sua criação.");
        }

        if (tarefa.Titulo != request.Titulo)
        {
            await _mediator.Publish(new UpdateTarefaEvent(tarefa, request.UsuarioId, "Título", tarefa.Titulo, request.Titulo), cancellationToken);
            tarefa.Titulo = request.Titulo;
        }

        if (tarefa.Descricao != request.Descricao)
        {
            await _mediator.Publish(new UpdateTarefaEvent(tarefa, request.UsuarioId, "Descrição", tarefa.Descricao, request.Descricao), cancellationToken);
            tarefa.Descricao = request.Descricao;
        }

        if (tarefa.DataVencimento != request.DataVencimento)
        {
            await _mediator.Publish(new UpdateTarefaEvent(tarefa, request.UsuarioId, "Data de Vencimento", tarefa.DataVencimento.ToString("s"), request.DataVencimento.Value.ToString("s")), cancellationToken);
            tarefa.DataVencimento = request.DataVencimento.Value;
        }

        await _unitOfWork.TarefaRepository.UpdateAsync(tarefa);
        await _unitOfWork.CommitAsync();

        return true;
    }
}