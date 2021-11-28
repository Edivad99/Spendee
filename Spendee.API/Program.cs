using Spendee.API.Managers;
using Spendee.Database;
using Spendee.Database.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
        builder.SetIsOriginAllowed(o => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

string connectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddScoped<IWalletRepository>(_ => new WalletRepository(connectionString));
builder.Services.AddScoped<ICategoryRepository>(_ => new CategoryRepository(connectionString));
builder.Services.AddScoped<CategoriesManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapGet("/categories", async (CategoriesManager categoriesManager) =>
{
    var result = await categoriesManager.GetAllCategoriesAsync();
    if (result == null)
        return Results.StatusCode((int)StatusCodes.Status500InternalServerError);
    return Results.Ok(result);
});

app.Run();
