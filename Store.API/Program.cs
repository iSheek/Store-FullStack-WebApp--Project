var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/{id}", () => "");


app.Run();
