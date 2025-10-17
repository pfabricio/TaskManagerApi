using MediatR;
using Moq;
using Shouldly;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Features.Comentarios.Command;
using TaskManager.Application.Features.Comentarios.Events;
using TaskManager.Application.Features.Comentarios.Handlers;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Test.Mediator.Comentarios;

public class AddComentarioHandlerTest
{
    [Fact]
    public async Task Handle_DeveAdicionarComentario_EPublicarEvento_QuandoComandoValido()
    {
        // Arrange
        var tarefaId = 1;
        var usuarioId = 2;

        var tarefa = new Tarefa
        {
            Id = tarefaId,
            Titulo = "Tarefa teste"
        };

        var comentarioRepoMock = new Mock<IComentarioRepository>();
        comentarioRepoMock.Setup(x => x.AddAsync(It.IsAny<Comentario>()))
            .Callback<Comentario>(c => c.Id = 123)
            .Returns(Task.CompletedTask);

        var tarefaRepoMock = new Mock<ITarefaRepository>();
        tarefaRepoMock.Setup(x => x.GetByIdAsync(tarefaId)).ReturnsAsync(tarefa);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.TarefaRepository).Returns(tarefaRepoMock.Object);
        unitOfWorkMock.Setup(x => x.ComentarioRepository).Returns(comentarioRepoMock.Object);
        unitOfWorkMock.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(x => x.Publish(It.IsAny<ComentarioHistoricoCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new AddComentarioCommandHandler(unitOfWorkMock.Object, mediatorMock.Object);

        var command = new AddComentarioCommand
        {
            TarefaId = tarefaId,
            UsuarioId = usuarioId,
            Texto = "Comentário de teste"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBe(123); // ID simulado no Callback
        tarefaRepoMock.Verify(x => x.GetByIdAsync(tarefaId), Times.Once);
        comentarioRepoMock.Verify(x => x.AddAsync(It.IsAny<Comentario>()), Times.Once);
        unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        mediatorMock.Verify(x => x.Publish(It.IsAny<ComentarioHistoricoCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DeveLancarNotFoundException_QuandoTarefaNaoEncontrada()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.TarefaRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Tarefa)null);

        var mediatorMock = new Mock<IMediator>();

        var handler = new AddComentarioCommandHandler(unitOfWorkMock.Object, mediatorMock.Object);

        var command = new AddComentarioCommand
        {
            TarefaId = 99,
            UsuarioId = 1,
            Texto = "Comentário qualquer"
        };

        // Act & Assert
        await Should.ThrowAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));

        unitOfWorkMock.Verify(x => x.TarefaRepository.GetByIdAsync(99), Times.Once);
        mediatorMock.Verify(x => x.Publish(It.IsAny<ComentarioHistoricoCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
    }
}