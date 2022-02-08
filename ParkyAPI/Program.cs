using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.ParkyMapper;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region ���U��Ʈw�s�u�覡(���appsettings.json=>ConnectionStrings)
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region ���UDI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region ���UAutoMapper(��X�޲zDto, ����Dto�u�ݦbParkyMappings.cs�����W��)
builder.Services.AddAutoMapper(typeof(ParkyMappings));
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("ParkyOpenAPISpecNP",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "Parky API(National Park)",
            Version = "1",
            Description = "Parky API: NationalPark",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            {
                Email = "abc@abc.com",
                Name = "Tester",
                Url = new Uri("https://www.google.com")
            },
            License = new Microsoft.OpenApi.Models.OpenApiLicense() 
            {
                Name = "MIT License",
                Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
            }
        });
    options.SwaggerDoc("ParkyOpenAPISpecTrails",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "Parky API(Trails)",
            Version = "1",
            Description = "Parky API: Trail",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            {
                Email = "abc@abc.com",
                Name = "Tester",
                Url = new Uri("https://www.google.com")
            },
            License = new Microsoft.OpenApi.Models.OpenApiLicense()
            {
                Name = "MIT License",
                Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
            }
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecNP/swagger.json", "Parky API NationalPark");
        options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecTrails/swagger.json", "Parky API Trails");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
