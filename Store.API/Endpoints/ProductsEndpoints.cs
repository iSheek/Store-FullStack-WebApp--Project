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

        group.MapGet("/", async (StoreContext dbContext) =>
            await dbContext.Products
                            .Include(product => product.Category)
                            .Include(product => product.ProductImage)
                            .Select(product => new ProductDTO(
                                product.Id,
                                product.Name,
                                product.Price,
                                product.Category!.Name,
                                product.ProductImage!.ImagePath
                            ))
                            .AsNoTracking()
                            .ToListAsync());


        group.MapGet("/{id}", async (int id, StoreContext dbContext) =>
        {

            var product = await dbContext.Products.FindAsync(id);

            return product is null ? Results.NotFound() : Results.Ok(
                new ProductDTO(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Category!.Name,
                    product.ProductImage!.ImagePath
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
            var product = await dbContext.Products.FindAsync(id);

            
            
        });

    }

}
