using MediatR;
using TaskManager.Application.Features.Tarefas.Events;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Tarefas.Handlers;

public class UpdateTarefaEventHandler : INotificationHandler<UpdateTarefaEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTarefaEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateTarefaEvent notification, CancellationToken cancellationToken)
    {
        var historico = new HistoricoTarefa
        {
            TarefaId = notification.Tarefa.Id,
            UsuarioId = notification.UsuarioId,
            CampoModificado = notification.CampoModificado,
            ValorAnterior = notification.ValorAnterior,
            NovoValor = notification.NovoValor,
            DataModificacao = DateTime.UtcNow
        };

        await _unitOfWork.HistoricoTarefaRepository.AddAsync(historico);
    }
}