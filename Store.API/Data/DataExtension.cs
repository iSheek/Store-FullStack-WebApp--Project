using System;
using Microsoft.EntityFrameworkCore;
using Store.API.Models;

namespace Store.API.Data;

public static class DataExtension
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<StoreContext>();
        dbContext.Database.Migrate();

        if(!dbContext.Categories.Any())
        {
            dbContext.Categories.AddRange(
                new Category {Name = "Food"},
                new Category {Name = "Cooking utensil"},
                new Category {Name = "Other"}
            );
            dbContext.SaveChanges();
        }

    }

    public static void AddStoreDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<StoreContext>((sp, options) =>
        {
            options.UseNpgsql(connString);
        });
        /*
        builder.Services.AddDbContext<StoreContext>(options =>
             options.UseNpgsql(connString)
             .UseSeeding((context, _) =>
             {
                if(!context.Set<Category>().Any())
                 {
                     context.Set<Category>().AddRange(
                        new Category {Name = "Food"},
                        new Category {Name = "Cooking utensil"},
                        new Category {Name = "Other"}
                     );

                     context.SaveChanges();
                 }

             })
             );
             */
    }


}
