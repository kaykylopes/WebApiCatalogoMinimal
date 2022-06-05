using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApiCatalogoMinimal.ApiEndPoints;
using WebApiCatalogoMinimal.AppServicesExtensions;
using WebApiCatalogoMinimal.Context;
using WebApiCatalogoMinimal.Models;
using WebApiCatalogoMinimal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJWT();


builder.Services.AddAuthorization();

var app = builder.Build();


//endpoint para login
app.MapAtenticacaoEndPoits();



//app.MapGet("/", () => "Catalogo de Produtos - 2022").ExcludeFromDescription();

app.MapCategoriasEndPoits();

app.MapProdutosEndPoits();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

var environment = app.Environment;
app.UseExceptionHandling(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

