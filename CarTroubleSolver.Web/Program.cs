using AutoMapper;
using CarTroubleSolver.Data.Configuration;
using CarTroubleSolver.Data.Database;
using CarTroubleSolver.Logic.Configuration;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using CarTroubleSolver.Logic.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Dependency Injections Reposiotries
builder.Services.AddRepositories();


builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterUserDtoValidator>());



// Dependency Injections Services
builder.Services.AddServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
