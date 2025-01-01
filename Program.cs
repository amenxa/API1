using ApiTest.config;
using ApiTest.Data;
using ApiTest.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseMySql(builder.Configuration.GetConnectionString("MyConnection"),
         new MySqlServerVersion(new Version(10, 4, 32))
         ));
builder.Services.AddAutoMapper(typeof(AutoMapperconfig));
builder.Services.AddTransient<ICategoryRepo, CategoryRepo>();

builder.Services.AddScoped(typeof(ICommonRepo<>), typeof(CommonRepo<>));

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Depdemo>();
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
