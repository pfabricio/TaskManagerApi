using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Usuario.Queries;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Usuario.Handlers;

public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, UsuarioDto>
{
    private readonly IUnitOfWork _nUnitOfWork;
    public GetUsuarioByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _nUnitOfWork = unitOfWork;
    }

    public async Task<UsuarioDto> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _nUnitOfWork.UsuarioRepository.GetByIdAsync(request.id)
                      ?? throw new NotFoundException(nameof(Domain.Entities.Usuario), request.id);
        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            NomeFuncao = usuario.Funcao.GetDescriptionFromValue<FuncaoUsuario>(),
            Funcao = (int)usuario.Funcao
        };
    }
}