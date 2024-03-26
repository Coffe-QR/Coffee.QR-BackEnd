using Coffee.QR.Infrastructure;

namespace Coffee.QR_BackEnd
{
    public static class ModulesConfiguration
    {
        public static IServiceCollection RegisterModules(this IServiceCollection services)
        {
            services.ConfigureModule();


            return services;
        }

    }
}
