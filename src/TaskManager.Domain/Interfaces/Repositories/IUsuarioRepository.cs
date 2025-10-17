using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(int id);
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario?> GetByLoginSenhaAsync(string login, string senha);
    Task<Usuario?> GetByRefreshTokenAsync(string refreshToken);
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task RemoveAsync(Usuario usuario);
}