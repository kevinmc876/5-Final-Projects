using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Interface.Data;
using Interface.Areas.Identity.Data;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("InterfaceDbContextConnection") ?? throw new InvalidOperationException("Connection string 'InterfaceDbContextConnection' not found.");

builder.Services.AddDbContext<InterfaceDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<InterfaceUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<InterfaceDbContext>();

builder.Services.AddHttpClient("PlantStoreApi", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("https://localhost:7277/api/");
    HttpClient.DefaultRequestHeaders.Accept.Clear();
    HttpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers
            .MediaTypeWithQualityHeaderValue("application/json"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
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
using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider); //
}

app.Run();
