using MediatR;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Features.Comentarios.Queries;

public record ComentarioDaTarefaQuery(int tarefaId): IRequest<List<ComentarioDto>>;