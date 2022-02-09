using ParkyWeb.Repository;
using ParkyWeb.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Nuget安裝: Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
#endregion

#region Http連線
builder.Services.AddHttpClient();
#endregion

#region 註冊DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion


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
