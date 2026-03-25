using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Registrar serviços de negócio
builder.Services.AddSingleton<EventoService>();
builder.Services.AddSingleton<CupomService>();
builder.Services.AddSingleton<UsuarioService>();

var app = builder.Build();

app.UseHttpsRedirection();

// Endpoints de eventos
app.MapPost("/api/eventos", (CreateEventoRequest request, EventoService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Nome))
        return Results.BadRequest("Nome do evento é obrigatório");

    var evento = service.CriarEvento(request);
    return Results.Created($"/api/eventos/{evento.Id}", evento);
});

app.MapGet("/api/eventos", (EventoService service) =>
{
    return Results.Ok(service.ListarEventos());
});

// Endpoints de cupons
app.MapPost("/api/cupons", (CreateCupomRequest request, CupomService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Codigo))
        return Results.BadRequest("Código do cupom é obrigatório");

    if (request.Desconto <= 0 || request.Desconto > 100)
        return Results.BadRequest("Desconto deve ser entre 0 e 100");

    var cupom = service.CriarCupom(request);
    return Results.Created($"/api/cupons/{cupom.Id}", cupom);
});

app.MapGet("/api/cupons", (CupomService service) =>
{
    return Results.Ok(service.ListarCupons());
});

// Endpoints de usuários
app.MapPost("/api/usuarios", (CreateUsuarioRequest request, UsuarioService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Nome))
        return Results.BadRequest("Nome é obrigatório");

    if (string.IsNullOrWhiteSpace(request.Email))
        return Results.BadRequest("Email é obrigatório");

    if (string.IsNullOrWhiteSpace(request.CPF))
        return Results.BadRequest("CPF é obrigatório");

    try
    {
        var usuario = service.CriarUsuario(request);
        return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapGet("/api/usuarios", (UsuarioService service) =>
{
    return Results.Ok(service.ListarUsuarios());
});

app.Run();

// ==================== MODELOS ====================
public class Evento
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string Local { get; set; } = string.Empty;
    public decimal Preco { get; set; }
}

public class Cupom
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public decimal Desconto { get; set; }
    public DateTime DataCriacao { get; set; }
    public bool Ativo { get; set; }
}

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
}

// ==================== DTOs ====================
public class CreateEventoRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string Local { get; set; } = string.Empty;
    public decimal Preco { get; set; }
}

public class CreateCupomRequest
{
    public string Codigo { get; set; } = string.Empty;
    public decimal Desconto { get; set; }
}

public class CreateUsuarioRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
}

// ==================== SERVIÇOS ====================
public class EventoService
{
    private readonly List<Evento> _eventos = new();
    private int _idCounter = 1;

    public Evento CriarEvento(CreateEventoRequest request)
    {
        var evento = new Evento
        {
            Id = _idCounter++,
            Nome = request.Nome,
            Descricao = request.Descricao,
            Data = request.Data,
            Local = request.Local,
            Preco = request.Preco
        };

        _eventos.Add(evento);
        return evento;
    }

    public List<Evento> ListarEventos() => _eventos;
}

public class CupomService
{
    private readonly List<Cupom> _cupons = new();
    private int _idCounter = 1;

    public Cupom CriarCupom(CreateCupomRequest request)
    {
        var cupom = new Cupom
        {
            Id = _idCounter++,
            Codigo = request.Codigo,
            Desconto = request.Desconto,
            DataCriacao = DateTime.Now,
            Ativo = true
        };

        _cupons.Add(cupom);
        return cupom;
    }

    public List<Cupom> ListarCupons() => _cupons;
}

public class UsuarioService
{
    private readonly List<Usuario> _usuarios = new();
    private int _idCounter = 1;

    public Usuario CriarUsuario(CreateUsuarioRequest request)
    {
        if (_usuarios.Any(x => x.CPF == request.CPF))
            throw new InvalidOperationException($"CPF {request.CPF} já está cadastrado!");

        var usuario = new Usuario
        {
            Id = _idCounter++,
            Nome = request.Nome,
            Email = request.Email,
            CPF = request.CPF,
            DataCadastro = DateTime.Now
        };

        _usuarios.Add(usuario);
        return usuario;
    }

    public List<Usuario> ListarUsuarios() => _usuarios;
}
