using AutoMapper;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.BuildingBlocks.Infrastructure.Database;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Mappers;
using Coffee.QR.Core.Services;
using Coffee.QR.Infrastructure.Auth;
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
            services.AddAutoMapper(typeof(EventProfile).Assembly);
            services.AddAutoMapper(typeof(ItemService).Assembly);
            services.AddAutoMapper(typeof(CompanyService).Assembly);
            services.AddAutoMapper(typeof(MenuService).Assembly);
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenGenerator,JwtGenerator>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IMenuService, MenuService>();

        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, Context>));
            services.AddScoped(typeof(ICrudRepository<Event>), typeof(CrudDatabaseRepository<Event, Context>));
            services.AddScoped(typeof(ICrudRepository<Item>), typeof(CrudDatabaseRepository<Item, Context>));
            services.AddScoped(typeof(ICrudRepository<Company>), typeof(CrudDatabaseRepository<Company, Context>));
            services.AddScoped(typeof(ICrudRepository<Menu>), typeof(CrudDatabaseRepository<Menu, Context>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();


            services.AddDbContext<Context>(opt =>
                opt.UseNpgsql(DbConnectionStringBuilder.Build("CoffeeQRSchema"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "CoffeeQRSchema")));
        }

    }
}
