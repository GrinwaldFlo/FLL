using FLL.Data;
using FLL.Data.Models;
using FLL.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration.GetSection("LogPath").Value ?? "",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

if (!Crypto.SetKey(builder.Configuration["CryptoKey"] ?? ""))
{
    Crypto.CreateKey();
}

// Database managements
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var version = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContextFactory<ApplicationDbContext>(
          dbContextOptions => dbContextOptions
              .UseMySql(connectionString, version)
#if DEBUG
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
#endif
                    );


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<FLL.Services.DataService>();
builder.Services.AddSingleton<FLL.Services.ChronoService>();
builder.Services.AddSingleton<FLL.Services.MatchService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddHostedService<FLL.Server.MainService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
