using PlantInterface.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlantInterface.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PlantDbContextConnection") ?? throw new InvalidOperationException("Connection string 'PlantDbContextConnection' not found.");

builder.Services.AddDbContext<PlantDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<PlantInterface.Areas.Identity.Data.PlantUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<PlantDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("PlantStoreApi", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("https://localhost:7277/api/");
    HttpClient.DefaultRequestHeaders.Accept.Clear();
    HttpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers
            .MediaTypeWithQualityHeaderValue("application/json"));
});

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


app.MapRazorPages();

app.Run();