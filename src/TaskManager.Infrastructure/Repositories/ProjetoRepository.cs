using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Extensions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.Repositories;

namespace TaskManager.Infrastructure.Repositories;

public class ProjetoRepository: IProjetoRepository
{
    private readonly AppDbContext _context;

    public ProjetoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Projeto?> GetByIdAsync(int id)
    {
        return await _context.Projetos
            .Include(t => t.Usuario)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Projeto>> GetByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Projetos
            .Where(p => p.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task AddAsync(Projeto projeto)
    {
        _context.Projetos.Add(projeto);
    }

    public async Task RemoveAsync(Projeto projeto)
    {
        _context.Projetos.Remove(projeto);
    }

    public async Task<bool> HasTarefasPendentesAsync(int projetoId)
    {
        return await _context.Tarefas.AnyAsync(t => t.ProjetoId == projetoId && t.Status == StatusTarefa.Pendente.ToInt());
    }
}