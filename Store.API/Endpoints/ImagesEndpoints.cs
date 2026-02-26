using System;
using Store.API.Data;
using Store.API.DTO;
using Store.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Store.API.Endpoints;

public static class ImagesEndpoints
{
    const string GetImageEndpoint = "GetImage";

    public static void MapProductImagesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/productimages");

        group.MapGet("/", async (StoreContext dbContext) =>
            await dbContext.ProductImages
                            .Select(productImage => new ProductImageDTO(
                                productImage.Id,
                                productImage.ImagePath
                            ))
                            .AsNoTracking()
                            .ToListAsync());

        group.MapGet("/{id}", async (int id, StoreContext dbContext) =>
        {
            var productImage = await dbContext.ProductImages
                                            .FirstOrDefaultAsync(i => i.Id == id);
            
            return productImage is null ? Results.NotFound() : Results.Ok(
                new ProductImageDTO(
                    productImage.Id,
                    productImage.ImagePath
                )
            );
            
        })
            .WithName(GetImageEndpoint);


        group.MapPost("/", async (StoreContext dbContext, CreateProductImageDTO newProductImage) =>
        {
            ProductImage productImage = new()
            {
              ImagePath = newProductImage.ImagePath  
            };

            dbContext.ProductImages.Add(productImage);

            await dbContext.SaveChangesAsync();

            ProductImageInfoDTO productImageInfo = new (
                productImage.Id,
                productImage.ImagePath
            );

            return Results.CreatedAtRoute(GetImageEndpoint, new {id = productImageInfo.Id}, productImageInfo);

        });

        group.MapPut("/{id}", async (int id, EditProductImageDTO changedProductImage, StoreContext dbContext) =>
        {
            var existingProductImage = await dbContext.ProductImages.FindAsync(id);

            if (existingProductImage is null)
            {
                return Results.NotFound();
            }

            existingProductImage.ImagePath = changedProductImage.ImagePath;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, StoreContext dbContext) =>
        {
            await dbContext.ProductImages
                            .Where(productImage => productImage.Id == id)
                            .ExecuteDeleteAsync();

            return Results.NoContent();
        });

    }

}