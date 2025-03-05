using Backend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

const byte MaxTriesDefault = 10;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlite<AppDbContext>(builder.Configuration.GetConnectionString("LOCAL"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.
        AllowAnyOrigin().
        AllowAnyHeader().
        AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();
app.MapGet("login", async (AppDbContext ctx) =>
{
    byte[] buffer = new byte[4];
    Random.Shared.NextBytes(buffer);
    var session = new Session()
    {
        Code = BitConverter.ToString(SHA256.HashData(buffer)).Replace("-", "")
    };
    await ctx.AddAsync(session);
    await ctx.SaveChangesAsync();
    return Results.Ok(session.Code);
});
app.MapPost("start-game", async (AppDbContext ctx, [FromHeader] string sessionToken) =>
{
    if ((await ctx.Sessions.FindAsync(sessionToken)) is not Session foundSession)
        return Results.NotFound(sessionToken);
    var game = new Game()
    {
        FK_Session = foundSession.Code,
        Number = (byte)Random.Shared.Next(0, 101),
        CurrentTries = MaxTriesDefault
    };
    await ctx.AliveGames.AddAsync(game);
    await ctx.SaveChangesAsync();
    return Results.Ok();
});
app.MapPost("game-try", async (AppDbContext ctx, [FromHeader] string sessionToken, [FromQuery] byte tryNumber) =>
{
    if ((await ctx.Sessions.FindAsync(sessionToken)) is not Session foundSession)
        return Results.NotFound(sessionToken);
    var foundGame = await ctx.AliveGames.FirstOrDefaultAsync(g => g.FK_Session == sessionToken);
    if (foundGame is null)
        return Results.Conflict($"There's not a game with session: {sessionToken}. Please start a game.");
    if (foundGame.Number != tryNumber)
    {
        foundGame.CurrentTries--;
        if (foundGame.CurrentTries is 0)
        {
            ctx.AliveGames.Remove(foundGame);
            ctx.Sessions.Remove(foundSession);
        }
    }
    else
    {
        ctx.AliveGames.Remove(foundGame);
        ctx.Sessions.Remove(foundSession);
    }
    await ctx.SaveChangesAsync();
    return foundGame.Number == tryNumber
        ? Results.Ok($"You're right, number was {foundGame.Number}")
        : Results.Ok($"Try {MaxTriesDefault - foundGame.CurrentTries}: You're wrong number is {(foundGame.Number > tryNumber ? "bigger" : "smaller")} than {tryNumber}");
});

app.Run(app.Configuration["ListenTo"]);