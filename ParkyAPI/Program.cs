using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.ParkyMapper;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
