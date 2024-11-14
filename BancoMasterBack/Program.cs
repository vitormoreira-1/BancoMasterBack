using BancoMasterBack.Domain.Interfaces.Repositories;
using BancoMasterBack.Domain.Interfaces.Services;
using BancoMasterBack.Domain.Services;
using BancoMasterBack.Repository;
using BancoMasterBack.Repository.Repositories;
using BancoMasterBack.Repository.Seed;
using BancoMasterBack.Webapi.Automapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Automapper
builder.Services.AddAutoMapper(typeof(DomainToViewModelProfile));
builder.Services.AddAutoMapper(typeof(ViewModelToViewModelProfile));

// AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BancoMasterBackInMemoryDb"));

// Dependency Injection
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<ICalculateDijkstraService, CalculateDijkstraService>();

builder.Services.AddScoped<IRouteRepository, RouteRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAngularApp");
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
}

app.MapControllers();

// Seed initial data for routes in the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    SeedDataDbInMemory.Initialize(services, context);
}

app.Run();
