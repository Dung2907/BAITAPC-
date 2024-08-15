using Exercise01.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Định nghĩa CORS policy
string Example06JSDomain = "AllowLocalhost3000";

builder.Services.AddControllers();

// Cấu hình DbContext
builder.Services.AddDbContext<Exercise01Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnectString"));
});

// Thêm CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Example06JSDomain,
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Thêm middleware CORS vào pipeline
app.UseCors(Example06JSDomain);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); // Thêm dòng này nếu bạn sử dụng xác thực

app.MapControllers();

app.Run();