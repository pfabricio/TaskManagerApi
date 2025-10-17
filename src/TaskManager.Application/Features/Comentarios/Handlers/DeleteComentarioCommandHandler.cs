using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Features.Comentarios.Command;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Comentarios.Handlers;

public class DeleteComentarioCommandHandler: IRequestHandler<DeleteComentarioCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteComentarioCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteComentarioCommand request, CancellationToken cancellationToken)
    {
        var comentario = await _unitOfWork.ComentarioRepository.GetByIdAsync(request.comentarioId);
        if (comentario == null)
            throw new NotFoundException("Comentário não encontrado.");

        await _unitOfWork.ComentarioRepository.RemoveAsync(comentario);
        await _unitOfWork.CommitAsync();
        return true;
    }
}