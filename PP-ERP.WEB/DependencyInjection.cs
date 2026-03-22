using PP_ERP.WEB.Services.Base;
using System.Reflection;

namespace PP_ERP.WEB
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddScoped<RestCommon>();

            var assembly = Assembly.GetExecutingAssembly();
            var serviceTypes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseService)));

            foreach (var type in serviceTypes)
            {
                services.AddScoped(type);
            }

            return services;
        }
    }
}
