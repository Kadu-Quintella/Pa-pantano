# API TicketPrime - Documentação

Sua API está rodando em `http://localhost:5000`

## Endpoints Disponíveis

### 1. Listar Eventos
**GET** `/api/eventos`

```powershell
Invoke-WebRequest -Uri "http://localhost:5000/api/eventos" -Method GET -UseBasicParsing
```

**Resposta (200 OK):**
```json
[
  {
    "id": 1,
    "nome": "Code Summit 2026",
    "descricao": "Conferência de Programação",
    "data": "2026-04-20T19:00:00",
    "local": "Rio de Janeiro",
    "preco": 200
  }
]
```

---

### 2. Cadastrar Novo Evento
**POST** `/api/eventos`

```powershell
$evento = '{
  "nome": "Python Conference 2026",
  "descricao": "Conferência de desenvolvimento Python",
  "data": "2026-05-15",
  "local": "São Paulo",
  "preco": 250
}'

Invoke-WebRequest -Uri "http://localhost:5000/api/eventos" `
  -Method POST `
  -UseBasicParsing `
  -Body $evento `
  -ContentType "application/json"
```

**Resposta (201 Created):**
```json
{
  "id": 2,
  "nome": "Python Conference 2026",
  "descricao": "Conferência de desenvolvimento Python",
  "data": "2026-05-15T00:00:00",
  "local": "São Paulo",
  "preco": 250
}
```

---

### 3. Cadastrar Novo Cupom
**POST** `/api/cupons`

```powershell
$cupom = '{
  "codigo": "PROMO2026",
  "desconto": 30
}'

Invoke-WebRequest -Uri "http://localhost:5000/api/cupons" `
  -Method POST `
  -UseBasicParsing `
  -Body $cupom `
  -ContentType "application/json"
```

**Resposta (201 Created):**
```json
{
  "id": 1,
  "codigo": "PROMO2026",
  "desconto": 30,
  "dataCriacao": "2026-03-18T15:38:52",
  "ativo": true
}
```

---

### 4. Cadastrar Novo Usuário
**POST** `/api/usuarios`

```powershell
$usuario = '{
  "nome": "Joao Silva",
  "email": "joao@email.com",
  "cpf": "12345678901"
}'

Invoke-WebRequest -Uri "http://localhost:5000/api/usuarios" `
  -Method POST `
  -UseBasicParsing `
  -Body $usuario `
  -ContentType "application/json"
```

**Resposta (201 Created):**
```json
{
  "id": 1,
  "nome": "Joao Silva",
  "email": "joao@email.com",
  "cpf": "12345678901",
  "dataCadastro": "2026-03-18T15:39:07"
}
```

---

## Validações

### CPF Duplicado
Se tentar cadastrar um usuário com um CPF que já existe, recebe:

**Resposta (400 Bad Request):**
```json
CPF 12345678901 já está cadastrado!
```

### Campos Obrigatórios para Cupom
Desconto deve estar entre 0 e 100.

**Resposta (400 Bad Request):**
```json
Desconto deve ser entre 0 e 100
```

---

## Como Rodar

1. Abra um terminal na pasta do projeto
2. Execute:
```bash
dotnet bin/Debug/net10.0/TicketPrime.dll
```

3. Use os endpoints acima para testar a API

---

## Estrutura do Projeto

- **Program.cs** - Arquivo principal com definição da API
- **Modelos** - Evento, Cupom, Usuario
- **Serviços** - EventoService, CupomService, UsuarioService
- **Controllers** - EventosController, CuponsController, UsuariosController
