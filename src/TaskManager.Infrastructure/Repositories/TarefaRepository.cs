using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Extensions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces.Repositories;

namespace TaskManager.Infrastructure.Repositories;

public class TarefaRepository: ITarefaRepository
{

    private readonly AppDbContext _context;

    public TarefaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Tarefa?> GetByIdAsync(int id)
    {
        return await _context.Tarefas
            .Include(t => t.Comentarios)
            .Include(t => t.Historicos)
            .Include(t=>t.Usuario)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Tarefa>> ListByProjetoIdAsync(int projetoId)
    {
        return await _context.Tarefas
            .Include(t=>t.Usuario)
            .Where(t => t.ProjetoId == projetoId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
    }

    public async Task UpdateAsync(Tarefa tarefa)
    {
        _context.Tarefas.Update(tarefa);
    }

    public async Task RemoveAsync(Tarefa tarefa)
    {
        _context.Tarefas.Remove(tarefa);
    }

    public async Task<int> CountByProjetoIdAsync(int projetoId)
    {
        return await _context.Tarefas.CountAsync(t => t.ProjetoId == projetoId);
    }

    public async Task<List<Tarefa>> GetByProjetoIdAsync(int projetoId)
    {
        return await _context.Tarefas
            .Include(i=>i.Projeto)
            .Include(i=>i.Usuario)
            .Where(t => t.ProjetoId == projetoId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Tarefa>> GetPendentesPorProjetoAsync(int projetoId)
    {
        return await _context.Tarefas
            .Where(t => t.ProjetoId == projetoId && t.Status == 0)
            .ToListAsync();
    }

    public async Task<bool> ExistemTarefasPendentesPorProjetoAsync(int projetoId)
    {
        return await _context.Tarefas
            .AllAsync(t => t.ProjetoId == projetoId && t.Status == StatusTarefa.Pendente.ToInt());
    }

    public async Task<List<Tarefa>> BuscarConcluidasNosUltimos30DiasAsync(int usuarioId)
    {
        var dataLimite = DateTime.UtcNow.AddDays(-30);

        return await _context.Tarefas
            .Include(t => t.Usuario)
            .Where(t => t.UsuarioId == usuarioId && (int)t.Status == (int)StatusTarefa.Concluida && t.DataVencimento >= dataLimite)
            .ToListAsync();
    }
}