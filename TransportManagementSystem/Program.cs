using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System;
using TransportManagementSystem.Data;
using TransportManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MVC
builder.Services.AddControllersWithViews();

// Add Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services
builder.Services.AddScoped<DistanceService>();
builder.Services.AddScoped<PricingService>();
builder.Services.AddScoped<BookingService>();

// Enable Session
builder.Services.AddSession();

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(db);
}

// Rotativa Setup
//RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

app.Run();
