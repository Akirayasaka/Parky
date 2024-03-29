﻿<h2>.Net 6 RESTful WebAPI</h2>

<div>
<h3>NuGet安裝套件</h3>
<ul>
    <li>Microsoft.EntityFrameworkCore</li>
    <li>Microsoft.EntityFrameworkCore.SqlServer</li>
    <li>Microsoft.EntityFrameworkCore.Tools</li>
    <li>AutoMapper</li>
    <li>AutoMapper.Extensions.Microsoft.DependencyInjection</li>
    <li>Microsoft.AspNetCore.Mvc.Versioning</li>
    <li>Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer</li>
</ul>
</div>

<div>
<h3>資料庫連線字串</h3>
<pre>
在appSettings.json內新增
    "ConnectionStrings": {
        "DefaultConnection": "資料庫連線位置"
    }
</pre>
</div>

<div>
<h3>Repository & UnitOfWork</h3>
<p>工廠模式; 共通方法寫在基底IRepository, Repository</p>
<p>類別整合在UnitOfWork內</p>
<p>Program.cs註冊 <pre>builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();</pre></p>
</div>

<div>
<h3>AutoMapper & Dto</h3>
<p>建立ParkyMappings.cs, 並繼承Profile; 在建構式內增減需要映射的 Model & Dto</p>
<p>Program.cs註冊 <pre>builder.Services.AddAutoMapper(typeof(ParkyMappings));</pre></p>
</div>    

<div>
<h3>API文件版控</h3>
<p>Program.cs註冊</p>
<p>新增ConfigureSwaggerOptions.cs作為設定檔</p>
<p>API Controller變更Route標籤屬性</p>
<p>API Controller就算沒有掛此標籤[ApiVersion("x.0")], 預設值為1.0</p>
</div>