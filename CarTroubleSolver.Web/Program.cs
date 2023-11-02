using AutoMapper;
using CarTroubleSolver.Data.Configuration;
using CarTroubleSolver.Data.Database;
using CarTroubleSolver.Logic.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Dependency Injections Reposiotries
builder.Services.AddRepositories();

/*var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<CarMapper>();
    cfg.AddProfile<UserMapper>();
    cfg.AddProfile<AccidentMapper>();
});
IMapper mapper = mapperConfig.CreateMapper();
Services.AddSingleton(mapper);*/

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddDbContext<CarTroubleSolverDbContext>(options =>
options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarTroubleSolver;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));


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
