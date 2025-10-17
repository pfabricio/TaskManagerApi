using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Interfaces.UnitOfWork;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Configuration;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        //IoC
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<IProjetoRepository, ProjetoRepository>();
        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IComentarioRepository, ComentarioRepository>();
        services.AddScoped<IHistoricoTarefaRepository, HistoricoTarefaRepository>();

        return services;
    }
}