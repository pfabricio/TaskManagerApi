using MediatR;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Usuario.Command;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Usuario.Handlers;

public class AddUsuarioCommandHandler: IRequestHandler<AddUsuarioCommand, UsuarioDto>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddUsuarioCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UsuarioDto> Handle(AddUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = new Domain.Entities.Usuario
        {
            Nome = request.Nome,
            Email = request.Email,
            Funcao = request.Funcao,
            Login = request.Login,
            Senha = request.Senha
        };

        await _unitOfWork.UsuarioRepository.AddAsync(usuario);
        await _unitOfWork.CommitAsync();

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