using AluraFlix.Shared.Dados.Banco;
using AluraFlix.API.Requests;
using AluraFlix.API.Responses;
using AluraFlix.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace AluraFlix.API.Endpoints;

public static class VideosExtensions
{
    public static void AddEndPointsVideos(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("videos").RequireAuthorization().WithTags("Videos");

        groupBuilder.MapGet("", ([FromServices] DAL<Videos> dal, int skip = 0, int take = 5) =>
        {
            var listaDeVideos = dal.Listar().Skip(skip).Take(take);
            if (listaDeVideos is null)
            {
                return Results.NotFound();
            }
            var listaDeVideosResponse = EntityListToResponseList(listaDeVideos);
            return Results.Ok(listaDeVideosResponse);
        });

        groupBuilder.MapGet("{id}", ([FromServices] DAL<Videos> dal, int id) =>
        {
            var videos = dal.RecuperarPor(a => a.Id == id);
            if (videos is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(videos));

        });

        groupBuilder.MapGet("{titulo}", ([FromServices] DAL<Videos> dal, string titulo) =>
        {
            var videos = dal.RecuperarPor(a => a.Titulo.ToUpper().Equals(titulo.ToUpper()));
            if (videos is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(videos));
        });

        groupBuilder.MapPost("", ([FromServices] DAL<Videos> dal, [FromBody] VideosRequest videosRequest) =>
        {
            var videos = new Videos(videosRequest.Titulo, videosRequest.Descricao, videosRequest.Url);
            
            if (videosRequest.CategoriasId > 0)
                videos.CategoriasId = videosRequest.CategoriasId;
            else
                videos.CategoriasId = 1;

            dal.Adicionar(videos);
            return Results.Ok();
        });

        groupBuilder.MapDelete("{id}", ([FromServices] DAL<Videos> dal, int id) => {
            var videos = dal.RecuperarPor(a => a.Id == id);
            if (videos is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(videos);
            return Results.NoContent();

        });

        groupBuilder.MapPut("", ([FromServices] DAL<Videos> dal, [FromBody] VideosRequestEdit videosRequestEdit) => {
            
            var videosAtualizar = dal.RecuperarPor(a => a.Id == videosRequestEdit.Id);
            if (videosAtualizar is null)
            {
                return Results.NotFound();
            }
            videosAtualizar.Titulo = videosRequestEdit.Titulo;
            videosAtualizar.Descricao = videosRequestEdit.Descricao;
            videosAtualizar.Url = videosRequestEdit.Url;
            dal.Atualizar(videosAtualizar);
            return Results.Ok();

        });

        groupBuilder.MapGet("{categoriaTitulo}/Videos", async ([FromServices] DAL<Videos> dal, string categoriaTitulo) =>
        {
            var videos = await dal.RecuperarVideosPorCategoriaAsync(categoriaTitulo);

            if (videos == null || !videos.Any())
            {
                return Results.NotFound();
            }

            var videosResponse = videos.Select(VideosExtensions.EntityToResponse).ToList();
            return Results.Ok(videosResponse);
        });

        app.MapGet("/videos/free", ([FromServices] DAL<Videos> dal) =>
        {
            var listaDeVideos = dal.Listar();

            if (listaDeVideos is null)
            {
                return Results.NotFound();
            }

            int maxVideosGratuitos = 3;

            var listaDeVideosResponse = EntityListToResponseList(listaDeVideos)
                .Take(maxVideosGratuitos)
                .ToList();

            return Results.Ok(listaDeVideosResponse);
        });
    }

    private static ICollection<VideosResponse> EntityListToResponseList(IEnumerable<Videos> listaDeVideos)
    {
        return listaDeVideos.Select(a => EntityToResponse(a)).ToList();
    }

    private static VideosResponse EntityToResponse(Videos videos)
    {
        return new VideosResponse(videos.Id, videos.Titulo, videos.Url, videos.Descricao, videos.CategoriasId);
    }
}