using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Persistence.Context.Configuration;

namespace TaskManager.Infrastructure;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<HistoricoTarefa> Historicos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMap());
        modelBuilder.ApplyConfiguration(new TarefaMap());
        modelBuilder.ApplyConfiguration(new ProjetoMap());
        modelBuilder.ApplyConfiguration(new ComentarioMap());
        modelBuilder.ApplyConfiguration(new HistoricoTarefaMap());
    }
}