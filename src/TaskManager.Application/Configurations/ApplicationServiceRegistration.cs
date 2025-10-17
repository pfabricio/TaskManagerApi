using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Features.Tarefas.Command;

namespace TaskManager.Application.Configurations;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationServiceRegistration).Assembly;

        services.AddMediatR(typeof(CreateTarefaCommand).Assembly);
        return services;
    }
}