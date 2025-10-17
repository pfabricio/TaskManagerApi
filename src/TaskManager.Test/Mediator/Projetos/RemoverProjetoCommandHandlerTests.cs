using Moq;
using FluentAssertions;
using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Features.Projetos.Command;
using TaskManager.Application.Features.Projetos.Events;
using TaskManager.Application.Features.Projetos.Handlers;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Test.Mediator.Projetos;

public class RemoverProjetoCommandHandlerTests
{
    [Fact(DisplayName = "Deve remover projeto quando não há tarefas pendentes")]
    public async Task RemoverProjeto_DeveRemoverProjeto_QuandoNaoHaTarefasPendentes()
    {
        // Arrange
        var projetoId = 1;
        var usuarioId = 1;

        var projeto = new Projeto
        {
            Id = projetoId,
            Nome = "Projeto AA",
            UsuarioId = usuarioId
        };
        var usuario = new Usuario
        {
            Id = usuarioId,
            Nome = "Paulo",
            Funcao = FuncaoUsuario.Gerente.ToInt(),
            Email = "paulo@teste.com"
        };

        var projetoRepoMock = new Mock<IProjetoRepository>();
        projetoRepoMock.Setup(x => x.GetByIdAsync(projetoId)).ReturnsAsync(projeto);
        projetoRepoMock.Setup(x => x.RemoveAsync(It.IsAny<Projeto>())).Returns(Task.CompletedTask);

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(x => x.GetByIdAsync(usuarioId)).ReturnsAsync(usuario);

        var tarefaRepoMock = new Mock<ITarefaRepository>();
        tarefaRepoMock
            .Setup(x => x.GetPendentesPorProjetoAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<Tarefa>());
        tarefaRepoMock.Setup(x => x.ExistemTarefasPendentesPorProjetoAsync(projetoId)).ReturnsAsync(false);

        var mediaRMock = new Mock<IMediator>();
        var uowMock = new Mock<IUnitOfWork>();
        uowMock.Setup(x => x.ProjetoRepository).Returns(projetoRepoMock.Object);
        uowMock.Setup(x => x.TarefaRepository).Returns(tarefaRepoMock.Object);
        uowMock.Setup(x => x.UsuarioRepository).Returns(usuarioRepoMock.Object);
        uowMock.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

        var handler = new DeleteProjetoCommandHandler(uowMock.Object, mediaRMock.Object);

        // Act
        await handler.Handle(new DeleteProjetoCommand { ProjetoId = projetoId }, CancellationToken.None);

        // Assert
        mediaRMock.Verify(m => m.Publish(It.Is<ProjetoTarefasRemovidaEvent>(e => e.projetoId == projetoId), It.IsAny<CancellationToken>()), Times.Once, "O evento não foi publicado.");

        projetoRepoMock.Verify(x => x.RemoveAsync(It.IsAny<Projeto>()), Times.Once);
        uowMock.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact(DisplayName = "Deve lançar exceção quando há tarefas pendentes")]
    public async Task RemoverProjeto_DeveFalhar_QuandoHaTarefasPendentes()
    {
        // Arrange
        var projetoId = 1;
        var usuarioId = 1;
        var projeto = new Projeto
        {
            Id = projetoId,
            Nome = "Projeto AA",
            UsuarioId = usuarioId
        };

        var usuario = new Usuario
        {
            Id = 1,
            Nome = "Paulo",
            Funcao = FuncaoUsuario.Gerente.ToInt(),
            Email = "paulo@teste.com"
        };

        var tarefasPendentes = new List<Tarefa>
        {
            new Tarefa
            {
                Id = 5, 
                ProjetoId = projetoId, 
                Status = StatusTarefa.Pendente.ToInt()
            }
        };

        var projetoRepoMock = new Mock<IProjetoRepository>();
        projetoRepoMock.Setup(x => x.GetByIdAsync(projetoId)).ReturnsAsync(projeto);

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(x => x.GetByIdAsync(usuarioId)).ReturnsAsync(usuario);

        var tarefaRepoMock = new Mock<ITarefaRepository>();
        tarefaRepoMock.Setup(x => x.GetPendentesPorProjetoAsync(projetoId)).ReturnsAsync(tarefasPendentes);
        tarefaRepoMock.Setup(x => x.ExistemTarefasPendentesPorProjetoAsync(projetoId)).ReturnsAsync(true);

        var mediaRMock = new Mock<IMediator>();
        var uowMock = new Mock<IUnitOfWork>();
        uowMock.Setup(x => x.ProjetoRepository).Returns(projetoRepoMock.Object);
        uowMock.Setup(x => x.TarefaRepository).Returns(tarefaRepoMock.Object);
        uowMock.Setup(x => x.UsuarioRepository).Returns(usuarioRepoMock.Object);

        var handler = new DeleteProjetoCommandHandler(uowMock.Object, mediaRMock.Object);

        // Act
        var act = async () => await handler.Handle(new DeleteProjetoCommand { ProjetoId = projetoId }, CancellationToken.None);

        // Assert
        var exception = await Assert.ThrowsAsync<BusinessRuleException>(act);
        exception.Message.Should().Contain("Não é possível remover o projeto enquanto houver tarefas pendentes.");
        uowMock.Verify(x => x.ProjetoRepository.RemoveAsync(It.IsAny<Projeto>()), Times.Never);
        uowMock.Verify(x => x.CommitAsync(), Times.Never);
    }
}