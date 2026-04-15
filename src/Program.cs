using Dapper;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UseCors();
app.UseDefaultFiles();
app.UseStaticFiles();

string connectionString = "Server=.\\SQLEXPRESS;Database=TicketPrime;Trusted_Connection=True;TrustServerCertificate=True;";

// EVENTOS

// BUSCAR eventos (GET)
app.MapGet("/api/eventos", async () =>
{
    using var db = new SqlConnection(connectionString);
    var sql = "SELECT Nome, CapacidadeTotal, DataEvento, PrecoPadrao FROM Eventos";
    var eventos = await db.QueryAsync<Evento>(sql);
    return Results.Ok(eventos);
});

// CADASTRAR evento (POST)
app.MapPost("/api/eventos", async (Evento evento) =>
{
    using var db = new SqlConnection(connectionString);
    var sql = @"INSERT INTO Eventos (Nome, CapacidadeTotal, DataEvento, PrecoPadrao) 
                VALUES (@Nome, @CapacidadeTotal, @DataEvento, @PrecoPadrao)";
    await db.ExecuteAsync(sql, evento);
    return Results.Created("/api/eventos", evento);
});

// USUÁRIOS

// BUSCAR usuários (GET)
app.MapGet("/api/usuarios", async () => {
    using var db = new SqlConnection(connectionString);
    var usuarios = await db.QueryAsync<Usuario>("SELECT Cpf, Nome, Email FROM Usuarios");
    return Results.Ok(usuarios);
});

// CADASTRAR usuário (POST)
app.MapPost("/api/usuarios", async (Usuario usuario) =>
{
    using var db = new SqlConnection(connectionString);
    var existe = await db.QueryFirstOrDefaultAsync<int>(
        "SELECT 1 FROM Usuarios WHERE Cpf = @Cpf",
        new { Cpf = usuario.Cpf }
    );
    if (existe == 1) return Results.BadRequest("CPF já cadastrado");

    var sql = "INSERT INTO Usuarios (Cpf, Nome, Email) VALUES (@Cpf, @Nome, @Email)";
    await db.ExecuteAsync(sql, usuario);
    return Results.Created($"/api/usuarios/{usuario.Cpf}", usuario);
});

// CUPONS

// BUSCAR cupons (GET) - Necessário para aparecer na tabela do site
app.MapGet("/api/cupons", async () => {
    using var db = new SqlConnection(connectionString);
    var cupons = await db.QueryAsync<Cupom>("SELECT codigo, PorcentagemDesconto, valorMinimoregra FROM Cupons");
    return Results.Ok(cupons);
});

// CADASTRAR cupom (POST)
app.MapPost("/api/cupons", async (Cupom cupom) =>
{
    using var db = new SqlConnection(connectionString);
    var sql = @"INSERT INTO Cupons (codigo, PorcentagemDesconto, valorMinimoregra) 
                VALUES (@codigo, @PorcentagemDesconto, @valorMinimoregra)";
    await db.ExecuteAsync(sql, cupom);
    return Results.Ok();
});

app.Run();

// MODELS (Records)

public record Evento(string Nome, int CapacidadeTotal, DateTime DataEvento, decimal PrecoPadrao);
public record Usuario(string Cpf, string Nome, string Email);
public record Cupom(string codigo, decimal PorcentagemDesconto, decimal valorMinimoregra);
