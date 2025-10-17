using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Features.Comentarios.Command;
using TaskManager.Application.Features.Comentarios.Events;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Comentarios.Handlers;

public class AddComentarioCommandHandler: IRequestHandler<AddComentarioCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public AddComentarioCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<int> Handle(AddComentarioCommand request, CancellationToken cancellationToken)
    {
        var tarefa = await _unitOfWork.TarefaRepository.GetByIdAsync(request.TarefaId);
        if (tarefa == null)
            throw new NotFoundException("Tarefa não encontrada.");

        var comentario = new Comentario
        {
            TarefaId = request.TarefaId,
            UsuarioId = request.UsuarioId,
            Conteudo = request.Texto,
            DataCriacao = DateTime.UtcNow
        };

        var historico = new ComentarioHistoricoCreatedEvent
        {
            TarefaId = request.TarefaId,
            UsuarioId = request.UsuarioId,
            CampoModificado = "Comentário",
            ValorAnterior = string.Empty,
            NovoValor = request.Texto,
            DataModificacao = DateTime.UtcNow
        };

        await _mediator.Publish(historico, cancellationToken);
        await _unitOfWork.ComentarioRepository.AddAsync(comentario);
        await _unitOfWork.CommitAsync();
        return comentario.Id;
    }
}