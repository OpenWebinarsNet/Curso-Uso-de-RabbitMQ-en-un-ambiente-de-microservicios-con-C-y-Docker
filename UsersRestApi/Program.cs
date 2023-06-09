using Microsoft.EntityFrameworkCore;
using UsersRestApiService.Data;
using UsersRestApiService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services
    .AddDbContext<UsersdbContext>(options => options.UseSqlServer(connectionString));

builder.Services
    .AddTransient<IUserDbService, UserDbService>();

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

app.MapGet("/GetUsers", async(IUserDbService userDbService) =>
{    
    return await userDbService.GetUsers();
});

app.MapPut("/UpdateUser", async(IUserDbService userDbService, User user) =>
{    
    return await userDbService.UpdateUser(user);
});

app.MapPost("/CreateUser", async (IUserDbService userDbService, User user) =>
{    
    return await userDbService.CreateUser(user);
});

app.Run();