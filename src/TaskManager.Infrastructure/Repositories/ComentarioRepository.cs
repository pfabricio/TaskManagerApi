using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.Repositories;

namespace TaskManager.Infrastructure.Repositories;

public class ComentarioRepository: IComentarioRepository
{
    private readonly AppDbContext _context;

    public ComentarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Comentario?> GetByIdAsync(int id)
    {
        return await _context.Comentarios
            .Include(t => t.Usuario)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Comentario?> GetByTarefaId(int id)
    {
        return await _context.Comentarios
            .Include(t=>t.Usuario)
            .Include(t=>t.Tarefa)
            .AsNoTracking()
            .FirstOrDefaultAsync(t=>t.TarefaId == id);
    }

    public async Task<IEnumerable<Comentario>> GetByTarefaIdAsync(int tarefaId)
    {
        return await _context.Comentarios
            .Include(t => t.Usuario)
            .Include(t => t.Tarefa)
            .Where(t => t.TarefaId == tarefaId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Comentario comentario)
    {
        _context.Comentarios.Add(comentario);
    }

    public async Task RemoveAsync(Comentario comentario)
    {
        _context.Comentarios.Remove(comentario);
    }
}