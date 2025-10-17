using Moq;
using Shouldly;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Features.Tarefas.Command;
using TaskManager.Application.Features.Tarefas.Handlers;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Test.Mediator.Tarefas;

public class AddTarefaCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveAdicionarTarefa_QuandoComandoValido()
    {
        // Arrange
        var projetoId = 1;
        var usuarioId = 1;

        var usuario = new Usuario
        {
            Id = usuarioId,
            Nome = "Paulo",
            Funcao = FuncaoUsuario.Gerente.ToInt(),
            Email = "paulo@teste.com"
        };
        var projeto = new Projeto
        {
            Id = projetoId,
            UsuarioId = usuarioId,
            Nome = "Projeto Secreto"
        };

        var projetoMock = new Mock<IProjetoRepository>();
        projetoMock.Setup(x => x.GetByIdAsync(projetoId)).ReturnsAsync(projeto);

        var usuarioMock = new Mock<IUsuarioRepository>();
        usuarioMock.Setup(x => x.GetByIdAsync(usuarioId)).ReturnsAsync(usuario);

        var TarefaRepoMock = new Mock<ITarefaRepository>();
        TarefaRepoMock.Setup(x => x.AddAsync(It.IsAny<Tarefa>()))
            .Callback<Tarefa>(t =>
            {
                t.Usuario = usuario;
                t.Projeto = projeto;
            })
            .Returns(Task.CompletedTask);

        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(x => x.ProjetoRepository).Returns(projetoMock.Object);
        unitOfWork.Setup(x => x.UsuarioRepository).Returns(usuarioMock.Object);
        unitOfWork.Setup(x => x.TarefaRepository.CountByProjetoIdAsync(projetoId)).ReturnsAsync(2);
        unitOfWork.Setup(x => x.TarefaRepository).Returns(TarefaRepoMock.Object);
        unitOfWork.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

        var handler = new CreateTarefaCommandHandler(unitOfWork.Object);
        var command = new CreateTarefaCommand{Titulo = "Nova Tarefa", Descricao = "Descrição", DataVencimento = DateTime.Today.AddDays(5), UsuarioId = 1, ProjetoId = 1};

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Titulo.ShouldBe("Nova Tarefa");
        result.Descricao.ShouldBe("Descrição");
        result.ProjetoId.ShouldBe(1);
        unitOfWork.Verify(x => x.TarefaRepository.AddAsync(It.IsAny<Tarefa>()), Times.Once);
        unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
    }
}