using Microsoft.EntityFrameworkCore;
using PostsRestApiService;
using PostsRestApiService.Data;
using PostsRestApiService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services
    .AddDbContext<PostsdbContext>(options => options.UseSqlServer(connectionString));

builder.Services
    .AddTransient<IPostDbService, PostDbService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
RabbitMQListener.ListenForIntegrationEvents(builder);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/GetPosts", async (IPostDbService postDbService) =>
{    
    return await postDbService.GetPosts();
});

app.MapPost("/CreatePost", async (IPostDbService postDbService, Post post) =>
{   
    return await postDbService.CreatePost(post);
});



app.Run();