using Microsoft.EntityFrameworkCore;
using Store.API.Data;
using Store.API.Endpoints;


var builder = WebApplication.CreateBuilder(args);



builder.AddStoreDb();

const string AllowEverything = "AllowAll";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowEverything,
                        policy =>
                        {
                            policy.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin();
                        });
});



var app = builder.Build();

app.UseCors(AllowEverything);
app.UseStaticFiles();

app.MapGet("/", () => "Hello World!");


app.MapProductsEndpoints();
app.MapCategoriesEndpoints();
app.MapProductImagesEndpoints();
app.MapOtherEndpoints();


app.MigrateDb();

app.Run();
