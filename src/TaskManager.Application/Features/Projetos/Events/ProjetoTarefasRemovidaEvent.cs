using MediatR;

namespace TaskManager.Application.Features.Projetos.Events;

public record ProjetoTarefasRemovidaEvent(int projetoId) : INotification;
