using Spendee.API.Extensions;
using Spendee.API.Managers;

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

builder.ConfigureCustomServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

// Categories
app.MapGet("/categories", async (CategoriesManager categoriesManager) =>
{
    var result = await categoriesManager.GetAllCategoriesAsync();
    if (result == null)
        return Results.StatusCode((int)StatusCodes.Status500InternalServerError);
    return Results.Ok(result);
});


// Wallets
app.MapGet("/wallets", async (WalletsManager walletsManager) =>
{
    var result = await walletsManager.GetAllWalletsAsync();
    if (result == null)
        return Results.StatusCode((int)StatusCodes.Status500InternalServerError);
    return Results.Ok(result);
});

app.MapGet("/wallets/{id}", async (int id, WalletsManager walletsManager) =>
{
    var result = await walletsManager.GetTransactionsByWalletIdAsync(id);
    if (result == null)
        return Results.StatusCode((int)StatusCodes.Status500InternalServerError);
    return Results.Ok(result);
});

app.Run();
