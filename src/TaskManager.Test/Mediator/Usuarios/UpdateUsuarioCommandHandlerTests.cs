using Moq;
using Shouldly;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Features.Usuario.Command;
using TaskManager.Application.Features.Usuario.Handlers;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Test.Mediator.Usuarios;

public class UpdateUsuarioCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveAtualizarUsuario_QuandoComandoValido()
    {
        var id = 1;
        var usuario = new Usuario { Id = id, Nome = "Antigo", Email = "antigo@email.com", Funcao = FuncaoUsuario.Colaborador.ToInt() };

        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(x => x.UsuarioRepository.GetByIdAsync(id)).ReturnsAsync(usuario);

        var handler = new UpdateUsuarioCommandHandler(unitOfWork.Object);
        var command = new UpdateUsuarioCommand{Id = id, Nome = "Novo Nome", Email = "novo@email.com", Funcao = (int)FuncaoUsuario.Gerente };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Nome.ShouldBe("Novo Nome");
        result.Email.ShouldBe("novo@email.com");
        result.Funcao.ShouldBe((int)FuncaoUsuario.Gerente);
    }
}