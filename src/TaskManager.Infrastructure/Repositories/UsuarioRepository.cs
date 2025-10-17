using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.Repositories;

namespace TaskManager.Infrastructure.Repositories;

public class UsuarioRepository: IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<List<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios
            .ToListAsync();
    }

    public async Task AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
    }

    public async Task RemoveAsync(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
    }

    public async Task<Usuario?> GetByLoginSenhaAsync(string login, string senha)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login && u.Senha == senha);
    }

    public async Task<Usuario?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }
}