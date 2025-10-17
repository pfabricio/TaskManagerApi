using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Extensions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Tarefas.Command;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Tarefas.Handlers;

public class CreateTarefaCommandHandler: IRequestHandler<CreateTarefaCommand, TarefaDto>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateTarefaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TarefaDto> Handle(CreateTarefaCommand request, CancellationToken cancellationToken)
    {
        var projeto = await _unitOfWork.ProjetoRepository.GetByIdAsync(request.ProjetoId);
        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(request.UsuarioId);

        if (projeto == null)
            throw new NotFoundException("Projeto não encontrado.", projeto);

        if (usuario == null)
            throw new NotFoundException("Usuário não encontrado.", projeto);

        var countTarefas = await _unitOfWork.TarefaRepository.CountByProjetoIdAsync(request.ProjetoId);
        if (countTarefas >= 20)
            throw new BusinessRuleException("Limite de 20 tarefas por projeto atingido.");

        var tarefa = new Tarefa
        {
            Titulo = request.Titulo,
            Descricao = request.Descricao,
            DataVencimento = request.DataVencimento,
            Prioridade = (int)request.Prioridade,
            Status = (int)StatusTarefa.Pendente,
            ProjetoId = request.ProjetoId,
            UsuarioId = request.UsuarioId,
            CriadoEm = DateTime.UtcNow
        };

        await _unitOfWork.TarefaRepository.AddAsync(tarefa);
        await _unitOfWork.CommitAsync();

        return new TarefaDto
        {
            Id = tarefa.Id,
            ProjetoId = tarefa.ProjetoId,
            ProjetoNome = tarefa.Projeto.Nome,
            NomeCriador = tarefa.Usuario.Nome,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            Status = tarefa.Status.GetDescriptionFromValue<StatusTarefa>(),
            DataVencimento = tarefa.DataVencimento,
            Prioridade = tarefa.Prioridade.GetDescriptionFromValue<PrioridadeTarefa>()
        };
    }
}