using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<AppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<LoginDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LoginConnection")));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(50); // session timeout duration
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CommonDataLoadRepository>();
builder.Services.AddScoped<EBL_MigrationRepo>();
builder.Services.AddScoped<MailGenerator>();
builder.Services.AddScoped<WorkSpaceRepo>();
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
