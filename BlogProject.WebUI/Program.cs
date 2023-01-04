using BlogProject.Core.Service;
using BlogProject.Service.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();

// .Net Core MVC de tamamen Dependency Injection yapısıyla çalışıyoruz. ICoreService Interface'inin BaseService ile olan gevşek bağımlılığını tanımlıyoruz. Nerede ICoreService çağırılırsa, onun yerine BaseService gönderilecektir.
builder.Services.AddScoped(typeof(ICoreService<>), typeof(BaseService<>);
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
