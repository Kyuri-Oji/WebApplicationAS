using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Model;
using WebApplication3.ViewModels;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>(
    options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;" +
    "Initial Catalog=AspNetAuth;" +
    "Integrated Security=True;"));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;

    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 12;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
}).AddEntityFrameworkStores<AuthDbContext>();


builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(60);
    options.ExcludedHosts.Add("example.com");
    options.ExcludedHosts.Add("www.example.com");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePages(async con =>
{
    if (con.HttpContext.Response.StatusCode == 404)
    {
        con.HttpContext.Response.Redirect("/404");
    }
    if (con.HttpContext.Response.StatusCode == 403)
    {
        con.HttpContext.Response.Redirect("/403");
    }
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
