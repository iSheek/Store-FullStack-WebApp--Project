using System;
using Store.API.Data;
using Store.API.DTO;
using Store.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Store.API.Endpoints;

public static class CategoriesEndpoints
{   
    const string GetCategoryEndpoint = "GetCategory";
    public static void MapCategoriesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/categories");

        group.MapGet("/", async (StoreContext dbContext) =>
            await dbContext.Categories
                            .Select(category => new CategoryDTO(
                                category.Id,
                                category.Name
                            ))
                            .AsNoTracking()
                            .ToListAsync());

        group.MapGet("/{id}", async (int id, StoreContext dbContext) =>
        {
            var category = await dbContext.Categories
                                        .FirstOrDefaultAsync(c => c.Id == id);

            return category is null ? Results.NotFound() : Results.Ok(
                new CategoryDTO(
                    category.Id,
                    category.Name
                )
            );

        })
            .WithName(GetCategoryEndpoint);

        group.MapPost("/", async (StoreContext dbContext, CreateCategoryDTO newCategory) =>
        {
            Category category = new()
            {
                Name = newCategory.Name
            };

            dbContext.Categories.Add(category);

            await dbContext.SaveChangesAsync();

            CategoryInfoDTO categoryInfo = new(
                category.Id,
                category.Name
            );

            return Results.CreatedAtRoute(GetCategoryEndpoint, new {id = categoryInfo.Id}, categoryInfo);

        });

        group.MapPut("/{id}", async (int id, EditCategoryDTO changedCategory, StoreContext dbContext) =>
        {
            var existingCategory = await dbContext.Categories.FindAsync(id);

            if (existingCategory is null)
            {
                return Results.NotFound();
            }

            existingCategory.Name = changedCategory.Name;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, StoreContext dbContext) =>
        {
            await dbContext.Categories
                            .Where(category => category.Id == id)
                            .ExecuteDeleteAsync();
            
            return Results.NoContent();
        });

    }

}
