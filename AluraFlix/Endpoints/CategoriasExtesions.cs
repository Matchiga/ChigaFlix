using AluraFlix.Shared.Dados.Banco;
using AluraFlix.API.Requests;
using AluraFlix.API.Responses;
using AluraFlix.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace AluraFlix.API.Endpoints;

public static class CategoriasExtesions
{
    public static void AddEndpointsCategorias(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("categorias")
            .RequireAuthorization()
            .WithTags("Categorias");

        groupBuilder.MapGet("", ([FromServices] DAL<Categorias> dal) =>
        {
            var listaDeCategorias = dal.Listar();
            if(listaDeCategorias is null)
            {
                return Results.NotFound();
            }
            var listaDeCategoriasResponse = EntityListToResponseList(listaDeCategorias);
            return Results.Ok(listaDeCategoriasResponse);
        });

        groupBuilder.MapGet("{id}", ([FromServices] DAL<Categorias> dal, int id) =>
        {
            var categorias = dal.RecuperarPor(a => a.Id == id);
            if(categorias is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(categorias));
        });

        groupBuilder.MapPost("", ([FromServices] DAL<Categorias> dal, [FromBody] CategoriasRequest categoriasRequest) =>
        {
            var categorias = new Categorias(categoriasRequest.Id, categoriasRequest.Titulo, categoriasRequest.Cor);

            if (categoriasRequest.Titulo is null || categoriasRequest.Cor is null)
            {
                return Results.BadRequest();
            }

            dal.Adicionar(categorias);
            return Results.Ok(EntityToResponse(categorias));
        });

        groupBuilder.MapDelete("{id}", ([FromServices] DAL<Categorias> dal, int id) =>
        {
            var categorias = dal.RecuperarPor(a => a.Id == id);
            if (categorias is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(categorias);
            return Results.Ok();
        });

        groupBuilder.MapPut("", ([FromServices] DAL<Categorias> dal, [FromBody] CategoriasRequest categoriasRequest) =>
        {
            var categoriasAtualizar = dal.RecuperarPor(a => a.Id == categoriasRequest.Id);
            if (categoriasAtualizar is null)
            {
                return Results.NotFound();
            }
            categoriasAtualizar.Titulo = categoriasRequest.Titulo;
            categoriasAtualizar.Cor = categoriasRequest.Cor;

            dal.Atualizar(categoriasAtualizar);
            return Results.Ok();
        });
    }

    private static ICollection<CategoriasResponse> EntityListToResponseList(IEnumerable<Categorias> listaDeCategorias)
    {
        return listaDeCategorias.Select(a => EntityToResponse(a)).ToList();
    } 

    private static CategoriasResponse EntityToResponse(Categorias categorias)
    {
        return new CategoriasResponse(categorias.Id, categorias.Titulo, categorias.Cor);
    }
}
