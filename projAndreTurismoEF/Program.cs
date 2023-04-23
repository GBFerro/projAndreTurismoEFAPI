using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using projAndreTurismoEF.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<projAndreTurismoEFContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("projAndreTurismoEFContext") ?? throw new InvalidOperationException("Connection string 'projAndreTurismoEFContext' not found.")));

// Add services to the container.

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
