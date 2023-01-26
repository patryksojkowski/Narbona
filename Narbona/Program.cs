using Microsoft.EntityFrameworkCore;
using Narbona.Database;
using Narbona.Services;
using Narbona.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AddServices(builder.Services);

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

void AddServices(IServiceCollection services)
{
    services.AddControllersWithViews();

    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    services.AddDbContext<PeopleContext>(options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=NarbonaDatabase;Trusted_Connection=true"));

    services.AddScoped<IPersonService, PersonService>();
}
