using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Persons.Application.Common;

namespace Persons.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServiceCollection(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

            return services;
        }
    }
}
