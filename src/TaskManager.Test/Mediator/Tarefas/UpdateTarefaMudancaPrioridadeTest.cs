using FluentAssertions;
using MediatR;
using Moq;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Features.Tarefas.Command;
using TaskManager.Application.Features.Tarefas.Handlers;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Test.Mediator.Tarefas;

public class UpdateTarefaMudancaPrioridadeTest
{
    [Fact]
    public async Task Handle_DeveLancarExcecao_SePrioridadeForAlterada()
    {
        // Arrange
        var tarefaId = 1;
        var usuarioId = 10;

        var mockRepo = new Mock<IUnitOfWork>();
        var mockMediar = new Mock<IMediator>();

        var tarefa = new Tarefa
        {
            Id = tarefaId,
            ProjetoId = 1,
            Titulo = "Tarefa de teste",
            Descricao = "Descrição teste",
            DataVencimento = DateTime.UtcNow.AddDays(3),
            Prioridade = PrioridadeTarefa.Media.ToInt(),
            Status = StatusTarefa.Pendente.ToInt(),
            UsuarioId = usuarioId
        };

        mockRepo.Setup(r => r.TarefaRepository.GetByIdAsync(tarefaId)).ReturnsAsync(tarefa);


        var command = new UpdateTarefaCommand
        {
            TarefaId = tarefaId,
            Titulo = "Tarefa de teste atualizada",
            Descricao = "Descrição atualizada",
            DataVencimento = DateTime.UtcNow.AddDays(5),
            Prioridade = PrioridadeTarefa.Alta.ToInt(),
            UsuarioId = usuarioId
        };

        var _handler = new UpdateTarefaCommandHandler(mockRepo.Object, mockMediar.Object);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        var exception = await Assert.ThrowsAsync<BusinessRuleException>(act);
        exception.Message.Should().Contain("A prioridade da tarefa não pode ser alterada após sua criação.");
    }
}