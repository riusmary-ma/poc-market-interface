using PocMarketInterface.Module;
using PocMarketInterface.Matching;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5000");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowLocalhost3000");

app.MapPost("/api/match", (MatchRequest request) =>
{
    var matchData = new MatchData
    {
        MatchId = 0,
        Data = (request.Instruction, request.Allegement)
    };

    var matcher = new TryMatching();
    var result = matcher.CompareInstructionAndAllegement(matchData);

    return Results.Ok(new
    {
        matchId = result.MatchId,
        matchData = (int)result.Result,
        result = result.Result.ToString(),
        message = result.Result.ToString()
    });
});
app.MapGet("/", () => Results.Ok("API is running"));

app.Run();
