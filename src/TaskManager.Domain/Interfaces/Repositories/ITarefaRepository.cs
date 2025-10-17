using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces.Repositories;

public interface ITarefaRepository
{
    Task<Tarefa?> GetByIdAsync(int id);
    Task<List<Tarefa>> ListByProjetoIdAsync(int projetoId);
    Task AddAsync(Tarefa tarefa);
    Task UpdateAsync(Tarefa tarefa);
    Task RemoveAsync(Tarefa tarefa);
    Task<int> CountByProjetoIdAsync(int projetoId);
    Task<List<Tarefa>> GetByProjetoIdAsync(int projetoId);
    Task<List<Tarefa>> GetPendentesPorProjetoAsync(int projetoId);
    Task<bool> ExistemTarefasPendentesPorProjetoAsync(int projetoId);
    Task<List<Tarefa>> BuscarConcluidasNosUltimos30DiasAsync(int usuarioId);
}