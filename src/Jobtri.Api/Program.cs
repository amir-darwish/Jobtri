using Jobtri.Application;
using Jobtri.Infrastructure;
using Jobtri.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("JobtriDatabase");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Database connection string was not found.");
}

builder.Services.AddDbContext<JobtriDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
