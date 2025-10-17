using TaskManager.Domain.Interfaces.Repositories;

namespace TaskManager.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    IUsuarioRepository UsuarioRepository { get; }
    ITarefaRepository TarefaRepository { get; }
    IProjetoRepository ProjetoRepository { get; }
    IComentarioRepository ComentarioRepository { get; }
    IHistoricoTarefaRepository HistoricoTarefaRepository { get; }

    Task CommitAsync();
}