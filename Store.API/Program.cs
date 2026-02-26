using Microsoft.EntityFrameworkCore;
using Store.API.Data;
using Store.API.Endpoints;


var builder = WebApplication.CreateBuilder(args);



builder.AddStoreDb();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapProductsEndpoints();
app.MapCategoriesEndpoints();
app.MapProductImagesEndpoints();


app.MigrateDb();

app.Run();
