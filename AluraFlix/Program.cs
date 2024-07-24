using AluraFlix.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using AluraFlix.Shared.Dados.Banco;
using Microsoft.EntityFrameworkCore;
using AluraFlix.API.Endpoints;
using System.Text.Json.Serialization;
using AluraFlix.Shared.Dados.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AluraFlixContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AluraFlixContext")));

builder.Services
    .AddIdentityApiEndpoints<PessoaComAcesso>()
    .AddEntityFrameworkStores<AluraFlixContext>();

builder.Services.AddAuthorization();

builder.Services.AddTransient<DAL<Videos>>();
builder.Services.AddTransient<DAL<Categorias>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options => options.AddPolicy("wasm", policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:7089", builder.Configuration["FrontendUrl"] ?? "https://localhost:7015"]).AllowAnyMethod().SetIsOriginAllowed(pol => true).AllowAnyHeader().AllowCredentials()));

var app = builder.Build();

app.UseCors("wasm");

app.UseStaticFiles();
app.UseAuthorization();

app.AddEndPointsVideos();
app.AddEndpointsCategorias();

app.MapGroup("auth").MapIdentityApi<PessoaComAcesso>().WithTags("Autorização");

app.MapPost("auth/logout", async ([FromServices] SignInManager<PessoaComAcesso> signInManager) =>
{
    await signInManager.SignOutAsync();
    Results.Ok();
}).RequireAuthorization().WithTags("Autorização");

app.UseSwagger();
app.UseSwaggerUI();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 401)
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Não autorizado");
        await context.Response.CompleteAsync();
    }
    else if (context.Response.StatusCode == 403)
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Credenciais inválidas");
        await context.Response.CompleteAsync();
    }
});

app.Run();