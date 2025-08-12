using ChigaFlix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using ChigaFlix.Shared.Data.Bank;
using Microsoft.EntityFrameworkCore;
using ChigaFlix.API.Endpoints;
using System.Text.Json.Serialization;
using ChigaFlix.Shared.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChigaFlixContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChigaFlixContext")));

builder.Services
    .AddIdentityApiEndpoints<PersonWithAccess>()
    .AddEntityFrameworkStores<ChigaFlixContext>();

builder.Services.AddAuthorization();

builder.Services.AddTransient<DAL<Videos>>();
builder.Services.AddTransient<DAL<Categories>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options => options.AddPolicy("wasm", policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:7089", builder.Configuration["FrontendUrl"] ?? "https://localhost:7015"]).AllowAnyMethod().SetIsOriginAllowed(pol => true).AllowAnyHeader().AllowCredentials()));

var app = builder.Build();

app.UseCors("wasm");

app.UseStaticFiles();
app.UseAuthorization();

app.AddEndPointsVideos();
app.AddEndpointsCategories();

app.MapGroup("auth").MapIdentityApi<PersonWithAccess>().WithTags("Authorization");

app.MapPost("auth/logout", async ([FromServices] SignInManager<PersonWithAccess> signInManager) =>
{
    await signInManager.SignOutAsync();
    Results.Ok();
}).RequireAuthorization().WithTags("Authorization");

app.UseSwagger();
app.UseSwaggerUI();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 401)
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Unauthorized");
        await context.Response.CompleteAsync();
    }
    else if (context.Response.StatusCode == 403)
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Invalid credentials");
        await context.Response.CompleteAsync();
    }
});

app.Run();