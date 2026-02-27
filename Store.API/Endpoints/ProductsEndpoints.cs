using System;
using Store.API.Data;
using Store.API.DTO;
using Store.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Store.API.Endpoints;

public static class ProductsEndpoints
{   
    const string GetProductEndpoint = "GetProduct";
    public static void MapProductsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products");

        group.MapGet("/", async (StoreContext dbContext, int? categoryId) =>
        {
            if (categoryId is null)
            {
                return Results.Ok(await dbContext.Products
                            .Include(product => product.Category)
                            .Include(product => product.ProductImage)
                            .Select(product => new ProductDTO(
                                product.Id,
                                product.Name,
                                product.Price,
                                product.CategoryId,
                                product.Category != null ? product.Category.Name : null,
                                product.ProductImage!.ImagePath
                            ))
                            .AsNoTracking()
                            .ToListAsync());
            }

            else
            {
                        return Results.Ok(await dbContext.Products
                            .Include(product => product.Category)
                            .Include(product => product.ProductImage)
                            .Where(product => product.CategoryId == categoryId)
                            .Select(product => new ProductDTO(
                                product.Id,
                                product.Name,
                                product.Price,
                                product.CategoryId,
                                product.Category != null ? product.Category.Name : null,
                                product.ProductImage!.ImagePath
                            ))
                            .AsNoTracking()
                            .ToListAsync());
            } 
    });


        group.MapGet("/{id}", async (int id, StoreContext dbContext) =>
        {

            var product = await dbContext.Products
                    .Include(p => p.Category)
                    .Include(p => p.ProductImage)
                    .FirstOrDefaultAsync(p => p.Id == id);

            return product is null ? Results.NotFound() : Results.Ok(
                new ProductDTO(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.CategoryId,
                    product.Category?.Name,
                    product.ProductImage?.ImagePath
                )
            );

        })
            .WithName(GetProductEndpoint);

        group.MapPost("/", async (CreateProductDTO newProduct, StoreContext dbContext) =>
        {
            Product product = new()
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                CategoryId = newProduct.CategoryId,
                ProductImageId = newProduct.ImagePathId
            };
            
            dbContext.Products.Add(product);

            await dbContext.SaveChangesAsync();

            ProductInfoDTO productInfo = new(
                product.Id,
                product.Name,
                product.Price,
                product.CategoryId,
                product.ProductImageId
            );

            return Results.CreatedAtRoute(GetProductEndpoint, new {id = productInfo.Id}, productInfo);

        });

        group.MapPut("/{id}", async (int id, EditProductDTO changedProduct, StoreContext dbContext) =>
        {
            var existingProduct = await dbContext.Products.FindAsync(id);

            if (existingProduct is null)
            {
                return Results.NotFound();
            }

            existingProduct.Name = changedProduct.Name;
            existingProduct.Price = changedProduct.Price;
            existingProduct.CategoryId = changedProduct.CategoryId;
            existingProduct.ProductImageId = changedProduct.ImagePathId;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, StoreContext dbContext) =>
        {
            await dbContext.Products
                            .Where(product => product.Id == id)
                            .ExecuteDeleteAsync();
            return Results.NoContent();
        });


        
    }

}
