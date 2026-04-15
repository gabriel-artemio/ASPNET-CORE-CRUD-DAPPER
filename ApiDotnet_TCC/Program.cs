using ApiDotnet_TCC.Data;
using ApiDotnet_TCC.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var db = new Db();

db.Initialize();

app.MapPost("/api/geladeira/data", (DadosGeladeira data) =>
{
    data.hora = DateTime.Now.Hour;
    db.Insert(data);

    return Results.Ok(new
    {
        message = "Dados salvos"
    });
});

app.MapGet("/api/geladeira/data", () =>
{
    return db.GetLast();
});

app.Run();