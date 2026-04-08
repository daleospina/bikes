using cicloso.bikes.core.Data;
using cicloso.bikes.app.Components;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add database context

builder.Services.AddDbContextFactory<BikesContext>((serviceProvider, options) =>
{
    var config = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("BikesContext");

    var credential = new DefaultAzureCredential();
    var token = credential.GetToken(
        new TokenRequestContext(new[] { "https://database.windows.net/.default" })
    );

    var connection = new SqlConnection(connectionString);
    connection.AccessToken = token.Token;

    options.UseSqlServer(connection);
});

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add razor components

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add services controllers

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();
