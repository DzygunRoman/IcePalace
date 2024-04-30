using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AcePalace.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AcePalaceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AcePalaceContext")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AcePalaceContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddSession();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<IAllOrders, OrdersRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";   
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();
}
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();          
app.UseRouting();
app.UseAuthorization();            
app.MapRazorPages();
app.MapControllerRoute(name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

