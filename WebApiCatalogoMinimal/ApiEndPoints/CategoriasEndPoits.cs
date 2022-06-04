﻿using Microsoft.EntityFrameworkCore;
using WebApiCatalogoMinimal.Context;
using WebApiCatalogoMinimal.Models;

namespace WebApiCatalogoMinimal.ApiEndPoints
{
    public static class CategoriasEndPoits
    {
        public static void MapCategoriasEndPoits(this WebApplication app)
        {
            app.MapPost("/categorias", async (Categoria categoria, AppDbContext db)
    =>
            {
                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();
                return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);

            });

            app.MapGet("/categorias", async (AppDbContext db) => await db.Categorias.ToListAsync()).WithTags("Categorias").
                RequireAuthorization();

            app.MapGet("/categorias/{id:int}", async (int id, AppDbContext db)
                =>
            {
                return await db.Categorias.FindAsync(id)
                    is Categoria categoria
                    ? Results.Ok(categoria)
                    : Results.NotFound();
            });

            app.MapPut("/categorias/{id:int}", async (int id, Categoria categoria, AppDbContext db) =>
            {
                if (categoria.CategoriaId != id)
                {
                    return Results.BadRequest();
                }

                var categoriaDB = await db.Categorias.FindAsync(id);
                if (categoriaDB is null) return Results.NotFound();

                categoriaDB.Nome = categoria.Nome;
                categoriaDB.Descricao = categoria.Descricao;

                await db.SaveChangesAsync();

                return Results.Ok(categoriaDB);
            });

            app.MapDelete("/categorias/{id:int}", async (int id, AppDbContext db) =>
            {
                var categoria = await db.Categorias.FindAsync(id);

                if (categoria is null)
                {
                    return Results.NotFound();
                }

                db.Categorias.Remove(categoria);
                await db.SaveChangesAsync();

                return Results.NoContent();

            });

        }
    }
}
