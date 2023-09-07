using Microsoft.Extensions.DependencyInjection;
using Persons.Application.Interfaces;
using Persons.Persistence.Services;

namespace Persons.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IPersonQueryService, PersonQueryService>();
            services.AddScoped<IPersonCommandService, PersonCommandService>();

            return services;
        }
    }
}
