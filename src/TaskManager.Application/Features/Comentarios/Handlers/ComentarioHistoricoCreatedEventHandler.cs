using MediatR;
using TaskManager.Application.Features.Comentarios.Events;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Comentarios.Handlers;

public class ComentarioHistoricoCreatedEventHandler : INotificationHandler<ComentarioHistoricoCreatedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ComentarioHistoricoCreatedEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ComentarioHistoricoCreatedEvent notification, CancellationToken cancellationToken)
    {
        var historico = new HistoricoTarefa
        {
            TarefaId = notification.TarefaId,
            UsuarioId = notification.UsuarioId,
            CampoModificado = notification.CampoModificado,
            ValorAnterior = string.Empty,
            NovoValor = notification.NovoValor,
            DataModificacao = DateTime.UtcNow
        };

        await _unitOfWork.HistoricoTarefaRepository.AddAsync(historico);
    }
}