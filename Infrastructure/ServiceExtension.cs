using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceExtension
    {
        public static void AddDBContext(this IServiceCollection service, string connection)
        {
            service.AddDbContext<DataBaseContext>(options =>
            {
                options.UseSqlite(connection);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
        public static void AddRepository(this IServiceCollection service)
        {
            service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void AddIdentity(this IServiceCollection service)
        {
            service.AddIdentity<UserEntity, RoleEntity>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Stores.MaxLengthForKeys = 128;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                    .AddEntityFrameworkStores<DataBaseContext>()
                    .AddDefaultTokenProviders();
        }

    }
}