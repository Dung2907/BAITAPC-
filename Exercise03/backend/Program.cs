using Exercise03.Context;
using Microsoft.EntityFrameworkCore;
string Exercise03JSDomain = "_Exercise03JSDomain";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnectString")));
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Exercise03JSDomain,
    policy => policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());
});
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

app.UseAuthorization();
app.UseCors(Exercise03JSDomain);
app.MapControllers();

app.Run();
