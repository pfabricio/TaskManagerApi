using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces.Repositories;

public interface IProjetoRepository
{
    Task<Projeto?> GetByIdAsync(int id);
    Task<List<Projeto>> GetByUsuarioIdAsync(int usuarioId);
    Task AddAsync(Projeto projeto);
    Task RemoveAsync(Projeto projeto);
    Task<bool> HasTarefasPendentesAsync(int projetoId);
}