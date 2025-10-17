using MediatR;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Projetos.Queries;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Projetos.Handlers;

public class GetProjetosDoUsuarioQueryHandler : IRequestHandler<GetProjetosDoUsuarioQuery, List<ProjetoDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProjetosDoUsuarioQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ProjetoDto>> Handle(GetProjetosDoUsuarioQuery request, CancellationToken cancellationToken)
    {
        var projetos = await _unitOfWork.ProjetoRepository.GetByUsuarioIdAsync(request.usuarioId);
        return projetos.Select(p => new ProjetoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            CriadoPor = p.UsuarioId
        }).ToList();
    }
}