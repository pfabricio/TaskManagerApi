using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Comentarios.Queries;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Comentarios.Handlers;

public class ComentarioDaTarefaQueryHandler: IRequestHandler<ComentarioDaTarefaQuery, List<ComentarioDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public ComentarioDaTarefaQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<ComentarioDto>> Handle(ComentarioDaTarefaQuery request, CancellationToken cancellationToken)
    {
        var tarefa = await _unitOfWork.TarefaRepository.GetByIdAsync(request.tarefaId);
        if (tarefa == null)
            throw new NotFoundException("Tarefa");
        
        var comentario = await _unitOfWork.ComentarioRepository.GetByTarefaIdAsync(request.tarefaId);
        return comentario.Select(p => new ComentarioDto
        {
            Id = p.Id,
            TarefaId = p.TarefaId,
            UsuarioId = p.UsuarioId,
            Conteudo = p.Conteudo,
            NomeTarefa = p.Tarefa.Descricao,
            NomeUsuario = p.Usuario.Nome,
            DataCriacao = p.DataCriacao
        }).ToList();
    }
}