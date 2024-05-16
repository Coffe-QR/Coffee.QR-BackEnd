﻿using AutoMapper;
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
            services.AddAutoMapper(typeof(SupplyService).Assembly);
            services.AddAutoMapper(typeof(SupplyItemService).Assembly);
            services.AddAutoMapper(typeof(LocalUserService).Assembly);
            services.AddAutoMapper(typeof(MenuItemService).Assembly);
            services.AddAutoMapper(typeof(StorageService).Assembly);
            services.AddAutoMapper(typeof(StorageItemService).Assembly);
            services.AddAutoMapper(typeof(JobApplicationService).Assembly);
            services.AddAutoMapper(typeof(LocalProfile).Assembly);
            services.AddAutoMapper(typeof(TableProfile).Assembly);
            services.AddAutoMapper(typeof(NotificationProfile).Assembly);
            services.AddAutoMapper(typeof(OrderProfile).Assembly);


            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenGenerator, JwtGenerator>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<ISupplyService, SupplyService>();
            services.AddScoped<ISupplyItemService, SupplyItemService>();
            services.AddScoped<ILocalUserService, LocalUserService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IStorageItemService, StorageItemService>();
            services.AddScoped<IJobApplicationService, JobApplicationService>();
            services.AddScoped<ILocalService, LocalService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IOrderService, OrderService>();

        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, Context>));
            services.AddScoped(typeof(ICrudRepository<Event>), typeof(CrudDatabaseRepository<Event, Context>));
            services.AddScoped(typeof(ICrudRepository<Item>), typeof(CrudDatabaseRepository<Item, Context>));
            services.AddScoped(typeof(ICrudRepository<Company>), typeof(CrudDatabaseRepository<Company, Context>));
            services.AddScoped(typeof(ICrudRepository<Menu>), typeof(CrudDatabaseRepository<Menu, Context>));
            services.AddScoped(typeof(ICrudRepository<Supply>), typeof(CrudDatabaseRepository<Supply, Context>));
            services.AddScoped(typeof(ICrudRepository<SupplyItem>), typeof(CrudDatabaseRepository<SupplyItem, Context>));
            services.AddScoped(typeof(ICrudRepository<LocalUser>), typeof(CrudDatabaseRepository<LocalUser, Context>));
            services.AddScoped(typeof(ICrudRepository<MenuItem>), typeof(CrudDatabaseRepository<MenuItem, Context>));
            services.AddScoped(typeof(ICrudRepository<Storage>), typeof(CrudDatabaseRepository<Storage, Context>));
            services.AddScoped(typeof(ICrudRepository<StorageItem>), typeof(CrudDatabaseRepository<StorageItem, Context>));
            services.AddScoped(typeof(ICrudRepository<JobApplication>), typeof(CrudDatabaseRepository<JobApplication, Context>));
            services.AddScoped(typeof(ICrudRepository<Local>), typeof(CrudDatabaseRepository<Local, Context>));
            services.AddScoped(typeof(ICrudRepository<Table>), typeof(CrudDatabaseRepository<Table, Context>));
            services.AddScoped(typeof(ICrudRepository<Notification>), typeof(CrudDatabaseRepository<Notification, Context>));
            services.AddScoped(typeof(ICrudRepository<Order>), typeof(CrudDatabaseRepository<Order, Context>));


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<ISupplyRepository, SupplyRepository>();
            services.AddScoped<ISupplyItemRepository, SupplyItemRepository>();
            services.AddScoped<ILocalUserRepository, LocalUserRepository>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository > ();
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IStorageItemRepository, StorageItemRepository>();
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<ILocalRepository, LocalRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddDbContext<Context>(opt =>
                opt.UseNpgsql(DbConnectionStringBuilder.Build("CoffeeQRSchema"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "CoffeeQRSchema")));
        }

    }
}
