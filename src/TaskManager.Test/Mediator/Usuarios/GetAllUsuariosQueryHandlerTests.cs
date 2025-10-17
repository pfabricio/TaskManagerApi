using Moq;
using Shouldly;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Features.Usuario.Handlers;
using TaskManager.Application.Features.Usuario.Queries;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Test.Mediator.Usuarios;

public class GetAllUsuariosQueryHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarListaDeUsuarios()
    {
        var usuarios = new List<Usuario>
        {
            new() { Id = 1, Nome = "User1", Email = "user1@email.com", Funcao = FuncaoUsuario.Colaborador.ToInt() },
            new() { Id = 2, Nome = "User2", Email = "user2@email.com", Funcao = FuncaoUsuario.Gerente.ToInt() }
        };

        var mockRepo = new Mock<IUnitOfWork>();
        mockRepo.Setup(x => x.UsuarioRepository.GetAllAsync()).ReturnsAsync(usuarios);

        var handler = new GetAllUsuariosQueryHandler(mockRepo.Object);

        var result = await handler.Handle(new GetAllUsuariosQuery(), CancellationToken.None);

        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
    }
}