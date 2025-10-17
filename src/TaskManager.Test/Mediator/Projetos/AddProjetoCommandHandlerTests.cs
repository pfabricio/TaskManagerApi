using Moq;
using Shouldly;
using TaskManager.Application.Features.Projetos.Command;
using TaskManager.Application.Features.Projetos.Handlers;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Test.Mediator.Projetos;

public class AddProjetoCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveCriarProjeto_QuandoDadosValidos()
    {
        // Arrange
        var projetoRepoMock = new Mock<IProjetoRepository>();
        projetoRepoMock.Setup(x => x.AddAsync(It.IsAny<Projeto>()))
            .Returns(Task.CompletedTask);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.ProjetoRepository).Returns(projetoRepoMock.Object);
        unitOfWorkMock.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

        var handler = new CreateProjetoCommandHandler(unitOfWorkMock.Object);
        var command = new CreateProjetoCommand{Nome = "Projeto B", UsuarioId = 1};

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Nome.ShouldBe("Projeto B");
        result.CriadoPor.ShouldBe(1);
        projetoRepoMock.Verify(x => x.AddAsync(It.IsAny<Projeto>()), Times.Once);
        unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }
}