using System.Collections.Concurrent;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Scalar.AspNetCore;
using Web.SharpRoyale.Hubs;
using Engine.SharpRoyale;
using Web.SharpRoyale.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<TokenService>();
builder.Services.AddSingleton<LobbyService>();
builder.Services.AddSingleton<MatchNotifier>();
builder.Services.AddSingleton<ITickResultPublisher, SignalRTickResultPublisher>();
builder.Services.AddSingleton<TickClientFeedback>();
builder.Services.AddHostedService<MatchmakingWorker>();
builder.Services.AddSingleton<MatchService>();

// Register the dictionary only (to avoid circular dependency)
builder.Services.AddSingleton<ConcurrentDictionary<int, Match>>(sp =>
{
    var matchService = sp.GetRequiredService<MatchService>();
    return matchService._matches;
});

builder.Services.AddSingleton<GameEngine>();
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["access_token"];
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.MapHub<MatchHub>("/hubs/match/{matchId:int}");
app.Run();
