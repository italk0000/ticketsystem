using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyModel;
using TicketSystem.Common.DataAccess;
using TicketSystem.Common.Middlewares;

namespace TicketSystem.Common.AutoInjection
{
    public static class AutoInjectionServiceCollection
    {
        public static IServiceCollection AddAutoInjection(this IServiceCollection services)
        {
            Injection(services);
            return services;
        }

        private static void Injection(IServiceCollection services)
        {
            services.AddSingleton<ExceptionMiddleware>();
            services.TryAddScoped<IUnitOfWork, UnitOfWork>();

            var assemblyNames = DependencyContext.Default.GetDefaultAssemblyNames();

            // Repository
            var assemblyRepository = Assembly.Load(assemblyNames.Single(x => x.FullName.StartsWith("TicketSystem") && x.FullName.EndsWith(".Repository")));

            var types = assemblyRepository
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => x.Name.EndsWith("Repository"));

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces().Where(x => x.Name.EndsWith("Repository"));
                foreach (var @interface in interfaces)
                {
                    services.TryAddScoped(@interface, type);
                }
            }

            // Service
            var assemblyService = Assembly.Load(assemblyNames.Single(x => x.FullName.StartsWith("TicketSystem") && x.FullName.EndsWith(".Service")));
            types = assemblyService
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => x.Name.EndsWith("Service"));

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces().Where(x => x.Name.EndsWith("Service"));
                foreach (var @interface in interfaces)
                {
                    services.TryAddTransient(@interface, type);
                }
            }

            // Middleware
            var assemblyCommon = Assembly.Load(assemblyNames.Single(x => x.FullName.StartsWith("TicketSystem") && x.FullName.EndsWith(".Common")));
            types = assemblyCommon
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => x.Name.EndsWith("Middleware"));

            foreach (var type in types)
            {
                services.TryAddSingleton(type);
            }
        }
    }
}
