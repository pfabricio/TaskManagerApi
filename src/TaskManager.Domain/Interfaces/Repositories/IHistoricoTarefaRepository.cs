using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces.Repositories;

public interface IHistoricoTarefaRepository
{
    Task<IEnumerable<HistoricoTarefa>> GetHistoricoByUsuario(int usuarioId);
    Task<IEnumerable<HistoricoTarefa>> GetHistoricoByTarefa(int tarefaId);
    Task AddAsync(HistoricoTarefa historico);
}