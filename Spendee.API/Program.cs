using Spendee.API.Extensions;
using Spendee.API.Managers;
using Spendee.Shared.Models;

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
    if (result.StatusCode == StatusCodes.Status200OK)
        return Results.Ok(result.Result);
    return Results.StatusCode(result.StatusCode);
});


// Wallets
app.MapGet("/wallets", async (WalletsManager walletsManager) =>
{
    var result = await walletsManager.GetAllWalletsAsync();
    if (result.StatusCode == StatusCodes.Status200OK)
        return Results.Ok(result.Result);
    return Results.StatusCode(result.StatusCode);
});

app.MapGet("/wallets/{id}", async (int id, WalletsManager walletsManager) =>
{
    var result = await walletsManager.GetTransactionsByWalletIdAsync(id);
    if (result.StatusCode == StatusCodes.Status200OK)
        return Results.Ok(result.Result);
    return Results.StatusCode(result.StatusCode);
});

app.MapPost("/wallets/{id}", async (int id, TransactionDTO transaction, WalletsManager walletsManager) =>
{
    var result = await walletsManager.AddTransactionAsync(id, transaction);
    if (result == StatusCodes.Status201Created)
        return Results.Created($"/wallets/{id}", transaction);
    return Results.StatusCode(result);
});

app.Run();
