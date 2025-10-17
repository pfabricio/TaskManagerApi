using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Features.Usuario.Command;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Usuario.Handlers;

public class DeleteUsuarioCommandHandler: IRequestHandler<DeleteUsuarioCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteUsuarioCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(request.id);
        if (usuario == null)
            throw new NotFoundException("Usuário não encontrado.");

        await _unitOfWork.UsuarioRepository.RemoveAsync(usuario);
        await _unitOfWork.CommitAsync();
        return true;
    }
}