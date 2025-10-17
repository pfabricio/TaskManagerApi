using Moq;
using Shouldly;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Features.Tarefas.Handlers;
using TaskManager.Application.Features.Tarefas.Queries;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Test.Mediator.Tarefas;

public class GetTarefasByProjetoIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarListaTarefas_QuandoProjetoIdValido()
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
        var tarefas = new List<Tarefa>
        {
            new()
            {
                Id = 1, Titulo = "Tarefa 1", Descricao = "Desc", DataVencimento = DateTime.Today, ProjetoId = projetoId,
                UsuarioId = usuarioId, Projeto = projeto, Usuario = usuario
            },
            new()
            {
                Id = 2, Titulo = "Tarefa 2", Descricao = "Desc", DataVencimento = DateTime.Today, ProjetoId = projetoId,
                UsuarioId = usuarioId, Projeto = projeto, Usuario = usuario
            }
        };

        var mockRepo = new Mock<IUnitOfWork>();
        mockRepo.Setup(x => x.TarefaRepository.GetByProjetoIdAsync(projetoId)).ReturnsAsync(tarefas);

        // Act
        var handler = new GetTarefasPorProjetoQueryHandler(mockRepo.Object);
        var result = await handler.Handle(new GetTarefasPorProjetoQuery(1), CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);
    }
}