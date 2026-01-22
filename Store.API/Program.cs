using Microsoft.EntityFrameworkCore;
using Store.API.Data;
using Store.API.DTO;
using Store.API.Models;

var builder = WebApplication.CreateBuilder(args);


builder.AddStoreDb();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// app.MapGet("/{id}", () => "");

app.MapGet("/products", async (StoreContext dbContext) =>
    await dbContext.Products
                    .Include(product => product.Category)
                    .Include(product => product.ProductImage)
                    .Select(product => new GetProductDTO(
                        product.Id,
                        product.Name,
                        product.Category!.Name,
                        product.ProductImage!.ImagePath
                    ))
                    .AsNoTracking()
                    .ToListAsync());



app.MigrateDb();

app.Run();
