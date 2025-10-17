using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Relatorios.Query;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Relatorios.Handler;

public class GetRelatorioDesempenhoQueryHandler : IRequestHandler<GetRelatorioDesempenhoQuery, List<RelatorioDesempenhoDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetRelatorioDesempenhoQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<RelatorioDesempenhoDto>> Handle(GetRelatorioDesempenhoQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(request.usuarioId);

        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado.");

        if (usuario.Funcao != (int)FuncaoUsuario.Gerente)
            throw new ForbiddenAccessException("Acesso permitido apenas para gerentes.");

        var tarefas = await _unitOfWork.TarefaRepository
            .BuscarConcluidasNosUltimos30DiasAsync(usuario.Id);

        var agrupadoPorUsuario = tarefas
            .GroupBy(t => t.UsuarioId)
            .Select(g => new RelatorioDesempenhoDto(
                UsuarioId: g.Key,
                Nome: g.First().Usuario!.Nome,
                TarefasConcluidas: g.Count(),
                MediaTarefasPorDia: Math.Round(g.Count() / 30.0, 2)
            ))
            .ToList();

        return agrupadoPorUsuario;
    }
}