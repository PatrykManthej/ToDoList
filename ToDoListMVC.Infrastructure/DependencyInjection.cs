using Microsoft.Extensions.DependencyInjection;
using ToDoListMVC.Domain.Interfaces;
using ToDoListMVC.Infrastructure.Repositories;

namespace ToDoListMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IToDoListRepository, ToDoListRepository>();
            services.AddTransient<IToDoTaskRepository, ToDoTaskRepository>();
            return services;
        }
    }
}
