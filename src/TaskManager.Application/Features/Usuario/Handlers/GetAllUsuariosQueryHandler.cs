using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Usuario.Queries;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Usuario.Handlers;

public class GetAllUsuariosQueryHandler : IRequestHandler<GetAllUsuariosQuery, List<UsuarioDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllUsuariosQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<UsuarioDto>> Handle(GetAllUsuariosQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await _unitOfWork.UsuarioRepository.GetAllAsync();
        return usuarios.Select(u => new UsuarioDto
        {
            Id = u.Id, 
            Nome = u.Nome, 
            Email = u.Email, 
            Funcao = u.Funcao, 
            NomeFuncao = u.Funcao.GetDescriptionFromValue<FuncaoUsuario>()
        }).ToList();
    }
}