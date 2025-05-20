using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ICT272_Assignment_3_Online_Tourism_Platform.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ICT272_Assignment_3_Online_Tourism_PlatformContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ICT272_Assignment_3_Online_Tourism_PlatformContext") ?? throw new InvalidOperationException("Connection string 'ICT272_Assignment_3_Online_Tourism_PlatformContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login"; // Redirects unauthenticated users
        options.AccessDeniedPath = "/Home/Login"; //When access is denied
    });

builder.Services.AddAuthorization();


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