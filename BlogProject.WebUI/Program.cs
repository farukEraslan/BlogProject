using BlogProject.Core.Service;
using BlogProject.Entities.Context;
using BlogProject.Service.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();

// .Net Core MVC de tamamen Dependency Injection yapısıyla çalışıyoruz. ICoreService Interface'inin BaseService ile olan gevşek bağımlılığını tanımlıyoruz. Nerede ICoreService çağırılırsa, onun yerine BaseService gönderilecektir.
builder.Services.AddScoped(typeof(ICoreService<>), typeof(BaseService<>));

// Authenticaton'ı programa tanıttık.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options =>
    {
        options.LoginPath = "/Account/Login";    // Yetki istenilen sayfalara girmek istediğimizde bizi yönlendireceği sayfayı belirliyoruz.
    });

builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer("Server = desktop-ufhr98h; Database = BlogProject; uid = sa; pwd = 123;"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();    // Authentication, authorization'ın üstünde olması gerekli
app.UseAuthorization();


// Area default controller route
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

// Default Controller Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
