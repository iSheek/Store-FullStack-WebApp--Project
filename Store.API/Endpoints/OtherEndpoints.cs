using System;
using Store.API.Data;
using Store.API.DTO;
using Store.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Store.API.Endpoints;

public static class OtherEndpoints
{
    public static void MapOtherEndpoints(this WebApplication app)
    {
        app.MapPost("/upload-image", async (IFormFile file, IWebHostEnvironment env, StoreContext dbContext) =>
        {
            if (file == null || file.Length == 0)
            {
                return Results.BadRequest("File not found!");
            }

            var imageFolder = Path.Combine(env.WebRootPath, "images");

            if(!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            var filePath = Path.Combine(imageFolder, uniqueFileName);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            var newImage = new ProductImage
            {
              ImagePath = "images/" + uniqueFileName  
            };

            dbContext.Set<ProductImage>().Add(newImage);
            await dbContext.SaveChangesAsync();

            return Results.Ok(newImage.Id);
        })
        .DisableAntiforgery();
    }
}