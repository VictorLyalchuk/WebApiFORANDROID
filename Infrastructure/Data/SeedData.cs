using Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
                context.Database.Migrate();

                if (!context.Categories.Any())
                {
                    var Openshoes = new CategoryEntity() { Id = 1, Name = "Open shoes" };
                    var Pumps = new CategoryEntity() { Id = 2, Name = "Pumps and loafers" };
                    var athletic = new CategoryEntity() { Id = 4, Name = "Women's athletic sneakers" };
                    var sneakers = new CategoryEntity() { Id = 5, Name = "Women's sneakers" };
                    var Heeled = new CategoryEntity() { Id = 3, Name = "Heeled shoes" };                
                    context.Categories.Add(Openshoes);
                    context.Categories.Add(Pumps);
                    context.Categories.Add(athletic);
                    context.Categories.Add(sneakers);
                    context.Categories.Add(Heeled);
                    context.SaveChanges();
                }
            }
        }
    }
}

              

