using ChigaFlix.Shared.Data.Bank;
using ChigaFlix.API.Requests;
using ChigaFlix.API.Responses;
using ChigaFlix.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChigaFlix.API.Endpoints;

public static class VideosExtensions
{
    public static void AddEndPointsVideos(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("videos").RequireAuthorization().WithTags("Videos");

        groupBuilder.MapGet("", ([FromServices] DAL<Videos> dal, int skip = 0, int take = 5) =>
        {
            var videoList = dal.List().Skip(skip).Take(take);
            if (videoList is null)
            {
                return Results.NotFound();
            }
            var listOfVideosResponse = EntityListToResponseList(videoList);
            return Results.Ok(listOfVideosResponse);
        });

        groupBuilder.MapGet("{id}", ([FromServices] DAL<Videos> dal, int id) =>
        {
            var videos = dal.RecoverBy(a => a.Id == id);
            if (videos is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(videos));

        });

        groupBuilder.MapGet("{title}", ([FromServices] DAL<Videos> dal, string title) =>
        {
            var videos = dal.RecoverBy(a => a.Title.ToUpper().Equals(title.ToUpper()));
            if (videos is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(videos));
        });

        groupBuilder.MapPost("", ([FromServices] DAL<Videos> dal, [FromBody] VideosRequest videosRequest) =>
        {
            var videos = new Videos(videosRequest.Title, videosRequest.Description, videosRequest.Url);
            
            if (videosRequest.CategoriesId > 0)
                videos.CategoriesId = videosRequest.CategoriesId;
            else
                videos.CategoriesId = 1;

            dal.Add(videos);
            return Results.Ok();
        });

        groupBuilder.MapDelete("{id}", ([FromServices] DAL<Videos> dal, int id) => {
            var videos = dal.RecoverBy(a => a.Id == id);
            if (videos is null)
            {
                return Results.NotFound();
            }
            dal.Remove(videos);
            return Results.NoContent();

        });

        groupBuilder.MapPut("", ([FromServices] DAL<Videos> dal, [FromBody] VideosRequestEdit videosRequestEdit) => {
            
            var videosUpdate = dal.RecoverBy(a => a.Id == videosRequestEdit.Id);
            if (videosUpdate is null)
            {
                return Results.NotFound();
            }
            videosUpdate.Title = videosRequestEdit.Title;
            videosUpdate.Description = videosRequestEdit.Description;
            videosUpdate.Url = videosRequestEdit.Url;
            dal.Update(videosUpdate);
            return Results.Ok();

        });

        groupBuilder.MapGet("{categoryTitle}/Videos", async ([FromServices] DAL<Videos> dal, string categoryTitle) =>
        {
            var videos = await dal.RetrieveVideosByCategoryAsync(categoryTitle);

            if (videos == null || !videos.Any())
            {
                return Results.NotFound();
            }

            var videosResponse = videos.Select(VideosExtensions.EntityToResponse).ToList();
            return Results.Ok(videosResponse);
        });

        app.MapGet("/videos/free", ([FromServices] DAL<Videos> dal) =>
        {
            var videoList = dal.List();

            if (videoList is null)
            {
                return Results.NotFound();
            }

            int maxFreeVideos = 3;

            var listOfVideosResponse = EntityListToResponseList(videoList)
                .Take(maxFreeVideos)
                .ToList();

            return Results.Ok(listOfVideosResponse);
        });
    }

    private static ICollection<VideosResponse> EntityListToResponseList(IEnumerable<Videos> videoList)
    {
        return videoList.Select(a => EntityToResponse(a)).ToList();
    }

    private static VideosResponse EntityToResponse(Videos videos)
    {
        return new VideosResponse(videos.Id, videos.Title, videos.Url, videos.Description, videos.CategoriesId);
    }
}