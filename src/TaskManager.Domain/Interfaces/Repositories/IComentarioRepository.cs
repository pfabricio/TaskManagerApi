using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces.Repositories;

public interface IComentarioRepository
{
    Task<Comentario?> GetByIdAsync(int id);
    Task<Comentario> GetByTarefaId(int tarefaId);
    Task<IEnumerable<Comentario>> GetByTarefaIdAsync(int tarefaId);
    Task AddAsync(Comentario comentario);
    Task RemoveAsync(Comentario comentario);
}