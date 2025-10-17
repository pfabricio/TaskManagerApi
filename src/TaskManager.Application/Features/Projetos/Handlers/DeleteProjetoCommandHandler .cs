using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Features.Projetos.Command;
using TaskManager.Application.Features.Projetos.Events;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Projetos.Handlers;

public class DeleteProjetoCommandHandler: IRequestHandler<DeleteProjetoCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    public DeleteProjetoCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteProjetoCommand request, CancellationToken cancellationToken)
    {
        var projeto = await _unitOfWork.ProjetoRepository.GetByIdAsync(request.ProjetoId);
        if (projeto == null)
            throw new NotFoundException("Projeto não encontrado.");

        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(projeto.UsuarioId);
        if (usuario == null || usuario.Funcao != (int)FuncaoUsuario.Gerente)
            throw new ForbiddenAccessException("Somente gerentes podem remover projetos.");

        var tarefasPendentes = await _unitOfWork.TarefaRepository.GetPendentesPorProjetoAsync(request.ProjetoId);
        if (tarefasPendentes.Count > 0)
            throw new BusinessRuleException("Não é possível remover o projeto enquanto houver tarefas pendentes.");

        await _mediator.Publish(new ProjetoTarefasRemovidaEvent(projeto.Id), cancellationToken);

        await _unitOfWork.ProjetoRepository.RemoveAsync(projeto);
        await _unitOfWork.CommitAsync();

        return true;
    }
}