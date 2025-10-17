using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Interfaces.UnitOfWork;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.UnitOfWork;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;

    #region Repositories
    private IUsuarioRepository? _usuarioRepo;
    private ITarefaRepository? _tarefaRepo;
    private IProjetoRepository? _projetoRepo;
    private IComentarioRepository? _comentarioRepo;
    private IHistoricoTarefaRepository? _historicoTarefaRepo;
    #endregion

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    #region Instances
    public IUsuarioRepository UsuarioRepository => _usuarioRepo ?? new UsuarioRepository(_context);
    public ITarefaRepository TarefaRepository => _tarefaRepo ?? new TarefaRepository(_context);
    public IProjetoRepository ProjetoRepository => _projetoRepo ?? new ProjetoRepository(_context);
    public IComentarioRepository ComentarioRepository => _comentarioRepo ?? new ComentarioRepository(_context);
    public IHistoricoTarefaRepository HistoricoTarefaRepository => _historicoTarefaRepo ?? new HistoricoTarefaRepository(_context);
    #endregion

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}