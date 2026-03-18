using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<EventoService>();
builder.Services.AddSingleton<CupomService>();
builder.Services.AddSingleton<UsuarioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

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
    public decimal Desconto { get; set; } // Em percentual
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
    private List<Evento> _eventos = new();
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

    public List<Evento> ListarEventos()
    {
        return _eventos;
    }
}

public class CupomService
{
    private List<Cupom> _cupons = new();
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

    public List<Cupom> ListarCupons()
    {
        return _cupons;
    }
}

public class UsuarioService
{
    private List<Usuario> _usuarios = new();
    private int _idCounter = 1;

    public Usuario CriarUsuario(CreateUsuarioRequest request)
    {
        // Validar se CPF já existe
        if (_usuarios.Any(u => u.CPF == request.CPF))
        {
            throw new InvalidOperationException($"CPF {request.CPF} já está cadastrado!");
        }

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

    public List<Usuario> ListarUsuarios()
    {
        return _usuarios;
    }
}

// ==================== CONTROLLERS ====================
[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly EventoService _eventoService;

    public EventosController(EventoService eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpPost]
    public ActionResult<Evento> CriarEvento(CreateEventoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest("Nome do evento é obrigatório");

        var evento = _eventoService.CriarEvento(request);
        return CreatedAtAction(nameof(CriarEvento), new { id = evento.Id }, evento);
    }

    [HttpGet]
    public ActionResult<List<Evento>> ListarEventos()
    {
        return Ok(_eventoService.ListarEventos());
    }
}

[ApiController]
[Route("api/[controller]")]
public class CuponsController : ControllerBase
{
    private readonly CupomService _cupomService;

    public CuponsController(CupomService cupomService)
    {
        _cupomService = cupomService;
    }

    [HttpPost]
    public ActionResult<Cupom> CriarCupom(CreateCupomRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Codigo))
            return BadRequest("Código do cupom é obrigatório");

        if (request.Desconto <= 0 || request.Desconto > 100)
            return BadRequest("Desconto deve ser entre 0 e 100");

        var cupom = _cupomService.CriarCupom(request);
        return CreatedAtAction(nameof(CriarCupom), new { id = cupom.Id }, cupom);
    }

    [HttpGet]
    public ActionResult<List<Cupom>> ListarCupons()
    {
        return Ok(_cupomService.ListarCupons());
    }
}

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public ActionResult<Usuario> CriarUsuario(CreateUsuarioRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest("Nome é obrigatório");

        if (string.IsNullOrWhiteSpace(request.Email))
            return BadRequest("Email é obrigatório");

        if (string.IsNullOrWhiteSpace(request.CPF))
            return BadRequest("CPF é obrigatório");

        try
        {
            var usuario = _usuarioService.CriarUsuario(request);
            return CreatedAtAction(nameof(CriarUsuario), new { id = usuario.Id }, usuario);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Usuario>> ListarUsuarios()
    {
        return Ok(_usuarioService.ListarUsuarios());
    }
}

