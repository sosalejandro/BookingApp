using BookingApp.API.Middlewares;
using BookingApp.DomainLayer.Repositories;
using BookingApp.Persistance;
using BookingApp.Persistance.Repositories;
using BookingApp.ServiceLayer.Abstractions;
using BookingApp.ServiceLayer.Services.V1;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(BookingApp.Presentation.AssemblyReference).Assembly);
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddDbContext<HotelDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

ApplyMigrations(app.Services);

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

partial class Program
{
    private static void ApplyMigrations(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        HotelDbContext context = scope.ServiceProvider.GetService<HotelDbContext>();
        context.Database.EnsureCreated();
        context.Dispose();
    }
}