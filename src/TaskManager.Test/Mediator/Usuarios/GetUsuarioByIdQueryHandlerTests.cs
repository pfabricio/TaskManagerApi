using Moq;
using Shouldly;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Features.Usuario.Handlers;
using TaskManager.Application.Features.Usuario.Queries;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Test.Mediator.Usuarios;

public class GetUsuarioByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarUsuario_QuandoIdExistente()
    {
        var id = 1;
        var usuario = new Usuario { Id = id, Nome = "Maria", Email = "maria@email.com", Funcao = FuncaoUsuario.Gerente.ToInt() };

        var mockRepo = new Mock<IUnitOfWork>();
        mockRepo.Setup(x => x.UsuarioRepository.GetByIdAsync(id)).ReturnsAsync(usuario);

        var handler = new GetUsuarioByIdQueryHandler(mockRepo.Object);

        var result = await handler.Handle(new GetUsuarioByIdQuery(id), CancellationToken.None);

        result.ShouldNotBeNull();
        result.Nome.ShouldBe("Maria");
        result.Funcao.ShouldBe((int)FuncaoUsuario.Gerente);
    }
}