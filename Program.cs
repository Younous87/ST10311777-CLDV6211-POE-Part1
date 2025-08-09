using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Azure.Identity;
using FirstWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Set the connection string in the static DataAccess class
DataAccess.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register IHttpContextAccessor and session-related services.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
});

// Register the Search service
builder.Services.AddScoped<Search>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable session
app.UseSession();

// UseRouting should be called before UseEndpoints
app.UseRouting();

app.UseAuthorization();

// Consolidate the endpoints configuration
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=HomePage}/{id?}");
});

app.Run();