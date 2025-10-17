using Moq;
using Shouldly;
using TaskManager.Application.Features.Usuario.Command;
using TaskManager.Application.Features.Usuario.Handlers;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Test.Mediator.Usuarios;

public class AddUsuarioCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveAdicionarUsuario_QuandoComandoValido()
    {
        // Arrange
        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(x => x.AddAsync(It.IsAny<Usuario>()))
            .Returns(Task.CompletedTask);

        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(x => x.UsuarioRepository).Returns(usuarioRepoMock.Object);
        unitOfWork.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);
        
        var handler = new AddUsuarioCommandHandler(unitOfWork.Object);
        var command = new  AddUsuarioCommand{Nome = "João", Email = "joao@gmail.com", Funcao = 0};

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Nome.ShouldBe("João");
        result.Email.ShouldBe("joao@gmail.com");
        result.Funcao.ShouldBe(0);

        usuarioRepoMock.Verify(x => x.AddAsync(It.IsAny<Usuario>()), Times.Once);
        unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
    }
}