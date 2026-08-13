using ApiWeb_Dapper.BLL;
using ApiWeb_Dapper.DAL;
using MySqlConnector;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IDbConnection>(_ => new MySqlConnection(connectionString));

builder.Services.AddScoped<FuncionarioDAL>();
builder.Services.AddScoped<FuncionarioBLL>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();