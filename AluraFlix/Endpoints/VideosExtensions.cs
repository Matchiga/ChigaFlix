using AluraFlix.Shared.Dados.Banco;
using AluraFlix.API.Requests;
using AluraFlix.API.Responses;
using AluraFlix.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace ScreenSound.API.Endpoints;

public static class VideosExtensions
{
    public static void AddEndPointsVideos(this WebApplication app)
    {

        #region Endpoint Artistas
        app.MapGet("/Videos", ([FromServices] DAL<Videos> dal) =>
        {
            var listaDeVideos = dal.Listar();
            if (listaDeVideos is null)
            {
                return Results.NotFound();
            }
            var listaDeVideosResponse = EntityListToResponseList(listaDeVideos);
            return Results.Ok(listaDeVideosResponse);
        });

        app.MapGet("/Videos/{id}", ([FromServices] DAL<Videos> dal, int id) =>
        {
            var videos = dal.RecuperarPor(a => a.Id == id);
            if (videos is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(videos));

        });

        app.MapPost("/Videos", ([FromServices] DAL<Videos> dal, [FromBody] VideosRequest videosRequest) =>
        {
            var videos = new Videos(videosRequest.titulo, videosRequest.descricao, videosRequest.url);

            dal.Adicionar(videos);
            return Results.Ok();
        });

        app.MapDelete("/Videos/{id}", ([FromServices] DAL<Videos> dal, int id) => {
            var videos = dal.RecuperarPor(a => a.Id == id);
            if (videos is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(videos);
            return Results.NoContent();

        });

        app.MapPut("/Videos", ([FromServices] DAL<Videos> dal, [FromBody] VideosRequestEdit videosRequestEdit) => {
            
            var videosAtualizar = dal.RecuperarPor(a => a.Id == videosRequestEdit.id);
            if (videosAtualizar is null)
            {
                return Results.NotFound();
            }
            videosAtualizar.Titulo = videosRequestEdit.titulo;
            videosAtualizar.Descricao = videosRequestEdit.descricao;
            videosAtualizar.Url = videosRequestEdit.url;
            dal.Atualizar(videosAtualizar);
            return Results.Ok();

        });
        #endregion
    }

    private static ICollection<VideosResponse> EntityListToResponseList(IEnumerable<Videos> listaDeVideos)
    {
        return listaDeVideos.Select(a => EntityToResponse(a)).ToList();
    }

    private static VideosResponse EntityToResponse(Videos videos)
    {
        return new VideosResponse(videos.Id, videos.Titulo, videos.Url, videos.Descricao);
    }
}