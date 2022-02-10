using ParkyWeb.Repository;
using ParkyWeb.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Nuget�w��: Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
#endregion

#region Http�s�u
builder.Services.AddHttpClient();
#endregion

#region ���UDI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region �ҥ�Session�\��
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    // Make the session cookie essential
    options.Cookie.IsEssential = true;
});
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

#region �ҥ�Session & ���v
app.UseCors(x => x
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

app.UseSession();
app.UseAuthentication();
#endregion

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
