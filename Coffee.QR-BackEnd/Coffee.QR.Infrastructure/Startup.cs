using AutoMapper;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.BuildingBlocks.Infrastructure.Database;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Mappers;
using Coffee.QR.Core.Services;
using Coffee.QR.Infrastructure.Database;
using Coffee.QR.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Coffee.QR.Infrastructure
{
    public static class Startup
    {

        public static IServiceCollection ConfigureModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            //services.AddScoped<IUserService, UserService>(); // treba ovo ali iz nekog razloga prolazi samo sa repom
            services.AddScoped<IUserRepository, UserRepository>();

        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, Context>));

            services.AddDbContext<Context>(opt =>
                opt.UseNpgsql(DbConnectionStringBuilder.Build("CoffeeQRSchema"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "CoffeeQRSchema")));
        }

    }
}
