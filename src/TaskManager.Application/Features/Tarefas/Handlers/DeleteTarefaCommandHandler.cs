using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Features.Tarefas.Command;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Tarefas.Handlers;

public class DeleteTarefaCommandHandler: IRequestHandler<DeleteTarefaCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTarefaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTarefaCommand request, CancellationToken cancellationToken)
    {
        var tarefa = await _unitOfWork.TarefaRepository.GetByIdAsync(request.tarefaId);
        if (tarefa == null)
            throw new NotFoundException("Tarefa não encontrada.");

        await _unitOfWork.TarefaRepository.RemoveAsync(tarefa);
        await _unitOfWork.CommitAsync();
        return true;
    }
}