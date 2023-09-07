using Mapster;
using MapsterMapper;
using Microsoft.OpenApi.Models;
using Persons.Application;
using System.Reflection;
using Persons.Persistence;

namespace Persons.API
{
    public static class PersonApiExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Person API"
                });
            });

            builder.Services.AddMappings();
            builder.Services
                .AddApplicationServiceCollection()
                .AddPersistenceServiceCollection();

            return builder.Build();
        }

        public static WebApplication ConfigureApiPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Person API");
                });
                app.UseExceptionHandler("/errorhandler/error-details");
            }
            else
            {
                app.UseExceptionHandler("/errorhandler/error");
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            return app;
        }

        private static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
    }
}
