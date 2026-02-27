using System;
using Microsoft.EntityFrameworkCore;
using Store.API.Models;

namespace Store.API.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<StoreContext>();

        dbContext.Database.EnsureDeleted(); // For clean DB (only with seeded data) after testing

        dbContext.Database.Migrate();

        if(!dbContext.Set<Category>().Any())
        {
            dbContext.Set<Category>().AddRange(
                new Category {Name = "Food"},
                new Category {Name = "Cooking utensil"},
                new Category {Name = "Other"}
            );
            dbContext.SaveChanges();
        }


        if(!dbContext.Set<ProductImage>().Any())
        {
            dbContext.Set<ProductImage>().AddRange(
                new ProductImage {ImagePath = "/images/apple_img.jpg"},
                new ProductImage {ImagePath = "/images/banana_img.jpg"},
                new ProductImage {ImagePath = "/images/bicycle_img.jpg"},
                new ProductImage {ImagePath = "/images/cutting_board_img.jpg"}
            );
            dbContext.SaveChanges();
        }

        if(!dbContext.Set<Product>().Any())
        {
            dbContext.Set<Product>().AddRange(
                new Product {Name = "Apple", Price = 0.99m, CategoryId = 1, ProductImageId = 1},
                new Product {Name = "Banana", Price = 1.99m, CategoryId = 1, ProductImageId = 2},
                new Product {Name = "Bicycle", Price = 100.50m, CategoryId = 3, ProductImageId = 3},
                new Product {Name = "Cutting board", Price = 5.00m, CategoryId = 2, ProductImageId = 4}
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
    }


}
