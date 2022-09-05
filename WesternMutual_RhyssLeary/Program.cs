using Microsoft.EntityFrameworkCore;

using WesternMutual_RhyssLeary;
using WesternMutual_RhyssLeary.Repository;
using WesternMutual_RhyssLeary.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDb"));// Connection string in appsettings.json
}
//, ServiceLifetime.Scoped // NOTE: Keep this at scoped. 
);


// *** Register services ***
AppConfig.RegisterServices(builder.Services);

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

// *** Set default values ***
var configuration = app.Services.GetService<IConfiguration>() 
    ?? throw new Exception("Unable to inject IConfiguration.");
AppConfig.Defaults.SetExpirationDays(configuration["Defaults:ExpirationDays"]);


app.Run();
