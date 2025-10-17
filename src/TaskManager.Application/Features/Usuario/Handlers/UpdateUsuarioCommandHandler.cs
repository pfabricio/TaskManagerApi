using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Usuario.Command;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Usuario.Handlers;

public class UpdateUsuarioCommandHandler: IRequestHandler<UpdateUsuarioCommand, UsuarioDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUsuarioCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UsuarioDto> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(request.Id)
                      ?? throw new NotFoundException(nameof(Domain.Entities.Usuario), request.Id);

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        usuario.Funcao = request.Funcao;

        await _unitOfWork.UsuarioRepository.UpdateAsync(usuario);
        await _unitOfWork.CommitAsync();

        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Funcao = (int)usuario.Funcao
        };
    }
}