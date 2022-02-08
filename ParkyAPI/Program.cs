using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ParkyAPI;
using ParkyAPI.Data;
using ParkyAPI.ParkyMapper;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region 註冊資料庫連線方式(對照appsettings.json=>ConnectionStrings)
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region 註冊DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region 註冊AutoMapper(整合管理Dto, 異動Dto只需在ParkyMappings.cs內做增減)
builder.Services.AddAutoMapper(typeof(ParkyMappings));
#endregion

#region API文件版本控制
builder.Services.AddApiVersioning(options => {
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
#region Version for Swagger
builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();
#endregion

#region Old SwaggerGen
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("ParkyOpenAPISpec",
//        new Microsoft.OpenApi.Models.OpenApiInfo()
//        {
//            Title = "Parky API",
//            Version = "1",
//            Description = "Parky API: NationalPark",
//            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
//            {
//                Email = "abc@abc.com",
//                Name = "Tester",
//                Url = new Uri("https://www.google.com")
//            },
//            License = new Microsoft.OpenApi.Models.OpenApiLicense() 
//            {
//                Name = "MIT License",
//                Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
//            }
//        });
//    //options.SwaggerDoc("ParkyOpenAPISpecTrails",
//    //    new Microsoft.OpenApi.Models.OpenApiInfo()
//    //    {
//    //        Title = "Parky API(Trails)",
//    //        Version = "1",
//    //        Description = "Parky API: Trail",
//    //        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
//    //        {
//    //            Email = "abc@abc.com",
//    //            Name = "Tester",
//    //            Url = new Uri("https://www.google.com")
//    //        },
//    //        License = new Microsoft.OpenApi.Models.OpenApiLicense()
//    //        {
//    //            Name = "MIT License",
//    //            Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
//    //        }
//    //    });
//});
#endregion

builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    #region Version for Swagger
    app.UseSwaggerUI(options =>
    {
        foreach(var desc in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
        }
    });
    #endregion

    #region Old SwaggerUI
    //app.UseSwaggerUI(options => {
    //    options.SwaggerEndpoint("/swagger/ParkyOpenAPISpec/swagger.json", "Parky API NationalPark");

    //    #region Switch Pattern
    //    //options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecNP/swagger.json", "Parky API NationalPark");
    //    //options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecTrails/swagger.json", "Parky API Trails");
    //    #endregion
    //});
    #endregion
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
