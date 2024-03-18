using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(50); // session timeout duration
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<GameRepository>();
builder.Services.AddScoped<GameCategoryService>();
builder.Services.AddScoped<PatchRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PatchGameRepository>();
builder.Services.AddScoped<CommonDataLoadRepository>();
builder.Services.AddScoped<GamePortalClientRepository>();
builder.Services.AddScoped<PromotionRepository>();
builder.Services.AddScoped<EBL_MigrationRepo>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=login}/{action=Index}/{id?}");

app.Run();
