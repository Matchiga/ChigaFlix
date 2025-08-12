using ChigaFlix.Shared.Data.Bank;
using ChigaFlix.API.Requests;
using ChigaFlix.API.Responses;
using ChigaFlix.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace ChigaFlix.API.Endpoints;

public static class CategoriesExtesions
{
    public static void AddEndpointsCategories(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("categories")
            .RequireAuthorization()
            .WithTags("Catergories");

        groupBuilder.MapGet("", ([FromServices] DAL<Categories> dal) =>
        {
            var categoryList = dal.List();
            if(categoryList is null)
            {
                return Results.NotFound();
            }
            var listOfCategoriesResponse = EntityListToResponseList(categoryList);
            return Results.Ok(listOfCategoriesResponse);
        });

        groupBuilder.MapGet("{id}", ([FromServices] DAL<Categories> dal, int id) =>
        {
            var categories = dal.RecoverBy(a => a.Id == id);
            if(categories is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(categories));
        });

        groupBuilder.MapPost("", ([FromServices] DAL<Categories> dal, [FromBody] CategoriesRequest categoriesRequest) =>
        {
            var categories = new Categories(categoriesRequest.Id, categoriesRequest.Title, categoriesRequest.Color);

            if (categoriesRequest.Title is null || categoriesRequest.Color is null)
            {
                return Results.BadRequest();
            }

            dal.Add(categories);
            return Results.Ok(EntityToResponse(categories));
        });

        groupBuilder.MapDelete("{id}", ([FromServices] DAL<Categories> dal, int id) =>
        {
            var categories = dal.RecoverBy(a => a.Id == id);
            if (categories is null)
            {
                return Results.NotFound();
            }
            dal.Remove(categories);
            return Results.Ok();
        });

        groupBuilder.MapPut("", ([FromServices] DAL<Categories> dal, [FromBody] CategoriesRequest categoriesRequest) =>
        {
            var categoriesUpdate = dal.RecoverBy(a => a.Id == categoriesRequest.Id);
            if (categoriesUpdate is null)
            {
                return Results.NotFound();
            }
            categoriesUpdate.Title = categoriesRequest.Title;
            categoriesUpdate.Color = categoriesRequest.Color;

            dal.Update(categoriesUpdate);
            return Results.Ok();
        });
    }

    private static ICollection<CategoriesResponse> EntityListToResponseList(IEnumerable<Categories> categoryList)
    {
        return categoryList.Select(a => EntityToResponse(a)).ToList();
    } 

    private static CategoriesResponse EntityToResponse(Categories categories)
    {
        return new CategoriesResponse(categories.Id, categories.Title, categories.Color);
    }
}
