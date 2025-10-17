using MediatR;
using TaskManager.Application.Features.Projetos.Events;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Projetos.Handlers;

public class ProjetoTarefasRemovidasEventHalder: INotificationHandler<ProjetoTarefasRemovidaEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjetoTarefasRemovidasEventHalder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ProjetoTarefasRemovidaEvent notification, CancellationToken cancellationToken)
    {
        var tarefas = await _unitOfWork.TarefaRepository.GetPendentesPorProjetoAsync(notification.projetoId);
        foreach (var tarefa in tarefas)
        {
            await _unitOfWork.TarefaRepository.RemoveAsync(tarefa);
        }
    }
}