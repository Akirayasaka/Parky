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
