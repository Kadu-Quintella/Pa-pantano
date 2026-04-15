using Dapper;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
var app = builder.Build();
app.UseCors();          
app.UseDefaultFiles();  
app.UseStaticFiles();
app.UseDefaultFiles(); 
app.UseStaticFiles();  

string connectionString = "Server=SEU_SERVIDOR;Database=TicketPrime;User Id=SA;Password=SUA_SENHA;";

app.MapPost("/api/eventos", async (Evento evento) => {
    using var db = new SqlConnection(connectionString);
    var sql = @"INSERT INTO Eventos (Nome, CapacidadeTotal, DataEvento, PrecoPadrao) 
                VALUES (@Nome, @CapacidadeTotal, @DataEvento, @PrecoPadrao)";
    await db.ExecuteAsync(sql, evento);
    return Results.Ok();
});

app.MapGet("/api/eventos", async () => {
    using var db = new SqlConnection(connectionString);
    var eventos = await db.QueryAsync<Evento>("SELECT * FROM Eventos");
    return Results.Ok(eventos);
});

app.MapPost("/api/usuarios", async (Usuario usuario) => {
    using var db = new SqlConnection(connectionString);
    
    var existe = await db.QueryFirstOrDefaultAsync<int>("SELECT 1 FROM Usuarios WHERE Cpf = @Cpf", new { usuario.Cpf });
    if (existe > 0) return Results.BadRequest("CPF já cadastrado");

    var sql = "INSERT INTO Usuarios (Cpf, Nome, Email) VALUES (@Cpf, @Nome, @Email)";
    await db.ExecuteAsync(sql, usuario);
    return Results.Created($"/api/usuarios/{usuario.Cpf}", usuario);
});

app.MapPost("/api/cupons", async (Cupom cupom) => {
    using var db = new SqlConnection(connectionString);
    var sql = @"INSERT INTO Cupons (codigo, PorcentagemDesconto, valorMinimoregra) 
                VALUES (@codigo, @PorcentagemDesconto, @valorMinimoregra)";
    await db.ExecuteAsync(sql, cupom);
    return Results.Ok();
});

app.Run();

public record Evento(string Nome, int CapacidadeTotal, DateTime DataEvento, decimal PrecoPadrao);
public record Usuario(string Cpf, string Nome, string Email);
public record Cupom(string codigo, decimal PorcentagemDesconto, decimal valorMinimoregra);
