using Microsoft.EntityFrameworkCore;
using Store.API.Data;
using Store.API.DTO;
using Store.API.Models;

var builder = WebApplication.CreateBuilder(args);

const string GetProductEndpoint = "GetProduct";

builder.AddStoreDb();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// app.MapGet("/{id}", () => "");

app.MapGet("/products", async (StoreContext dbContext) =>
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


app.MapGet("/products/{id}", async (int id, StoreContext dbContext) =>
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

app.MapPost("/products", async (CreateProductDTO newProduct, StoreContext dbContext) =>
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



app.MigrateDb();

app.Run();
