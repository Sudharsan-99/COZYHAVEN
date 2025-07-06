using DAL.Context;
using DAL.DataAccess.Interface;
using DAL.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services.Implementation;
using ServiceLayer.Services.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Register the repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register the service
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<CozyHavenDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Default route: HomeController -> Index()
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");


app.Run();