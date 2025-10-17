using MediatR;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Tarefas.Events;

public record UpdateTarefaEvent(
    Tarefa Tarefa, 
    int UsuarioId, 
    string? CampoModificado, 
    string? ValorAnterior, 
    string? NovoValor) : INotification;