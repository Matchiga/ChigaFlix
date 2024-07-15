using AluraFlix.Modelos;
using AluraFlix.Shared.Dados.Banco;
using Microsoft.EntityFrameworkCore;
using ScreenSound.API.Endpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AluraFlixContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AluraFlixContext")));

builder.Services.AddTransient<DAL<Videos>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.AddEndPointsVideos();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
