using MediatR;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Projetos.Command;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.UnitOfWork;

namespace TaskManager.Application.Features.Projetos.Handlers;

public class CreateProjetoCommandHandler: IRequestHandler<CreateProjetoCommand, ProjetoDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjetoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProjetoDto> Handle(CreateProjetoCommand request, CancellationToken cancellationToken)
    {
        var projeto = new Projeto
        {
            Nome = request.Nome,
            UsuarioId = request.UsuarioId
        };

        await _unitOfWork.ProjetoRepository.AddAsync(projeto);
        await _unitOfWork.CommitAsync();

        return new ProjetoDto
        {
            Id = projeto.Id,
            Nome = projeto.Nome,
            CriadoPor = projeto.UsuarioId
        };
    }
}