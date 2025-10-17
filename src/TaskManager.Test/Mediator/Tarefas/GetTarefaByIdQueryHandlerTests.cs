using Moq;
using Shouldly;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Features.Tarefas.Handlers;
using TaskManager.Application.Features.Tarefas.Queries;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Test.Mediator.Tarefas;

public class GetTarefaByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarTarefa_QuandoIdValido()
    {
        // Arrange
        var tarefaId = 10;
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
        var tarefa = new Tarefa
        {
            Id = tarefaId,
            Titulo = "Tarefa A",
            Descricao = "Descrição",
            DataVencimento = DateTime.Today.AddDays(3),
            ProjetoId = 1,
            UsuarioId = 1,
            Projeto = projeto,
            Usuario = usuario
        };

        var tarefaMock = new Mock<ITarefaRepository>();
        tarefaMock.Setup(e => e.GetByIdAsync(tarefaId)).ReturnsAsync(tarefa);

        var mockRepo = new Mock<IUnitOfWork>();
        mockRepo.Setup(x => x.TarefaRepository).Returns(tarefaMock.Object);

        // Act
        var handler = new GetTarefaQueryHandler(mockRepo.Object);
        var result = await handler.Handle(new GetTarefaQuery(tarefaId), CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Titulo.ShouldBe("Tarefa A");
    }
}