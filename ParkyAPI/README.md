<h2>.Net 6 RESTful WebAPI</h2>

1. NuGet安裝套件:
    Microsoft.EntityFrameworkCore
    Microsoft.EntityFrameworkCore.SqlServer
    Microsoft.EntityFrameworkCore.Tools
    AutoMapper
    AutoMapper.Extensions.Microsoft.DependencyInjection

2. 資料庫連線字串: 
    在appSettings.json內新增
    "ConnectionStrings": {
        "DefaultConnection": "資料庫連線位置" 
    }

3. Repository & UnitOfWork:
    工廠模式; 共通方法寫在基底IRepository, Repository

4. AutoMapper & Dto:
    建立ParkyMappings.cs, 並繼承Profile; 在建構式內增減需要映射的 Model & Dto
    Program.cs需註冊 builder.Services.AddAutoMapper(typeof(ParkyMappings));
    