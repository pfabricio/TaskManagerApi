using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.Repositories;

namespace TaskManager.Infrastructure.Repositories;

public class HistoricoTarefaRepository : IHistoricoTarefaRepository
{
    private readonly AppDbContext _context;

    public HistoricoTarefaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HistoricoTarefa>> GetHistoricoByUsuario(int usuarioId)
    {
        var historico = await _context.Historicos
            .Include(t => t.Usuario)
            .Include(t => t.Tarefa)
            .Where(s => s.UsuarioId == usuarioId)
            .AsNoTracking()
            .ToListAsync();
        return historico;
    }

    public async Task<IEnumerable<HistoricoTarefa>> GetHistoricoByTarefa(int tarefaId)
    {
        var historico = await _context.Historicos
            .Include(t => t.Usuario)
            .Include(t => t.Tarefa)
            .Where(s => s.TarefaId == tarefaId)
            .AsNoTracking()
            .ToListAsync();
        return historico;
    }

    public async Task AddAsync(HistoricoTarefa historico)
    {
        _context.Historicos.Add(historico);
    }
}