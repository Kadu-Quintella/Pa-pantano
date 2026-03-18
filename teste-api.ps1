# TicketPrime API - Teste Rápido

Salve este arquivo como `teste-api.ps1` e execute para testar todos os endpoints.

```powershell
# Configuração
$baseUrl = "http://localhost:5000/api"

Write-Host "=== TESTE DA API TicketPrime ===" -ForegroundColor Green

# 1. GET - Listar Eventos
Write-Host "`n1. Listando eventos..." -ForegroundColor Yellow
$eventos = Invoke-WebRequest -Uri "$baseUrl/eventos" -Method GET -UseBasicParsing
Write-Host "Resposta: $($eventos.Content)" -ForegroundColor Cyan

# 2. POST - Criar Evento
Write-Host "`n2. Criando novo evento..." -ForegroundColor Yellow
$eventoBody = '{
  "nome": "Workshop C#",
  "descricao": "Aprenda C# avançado",
  "data": "2026-06-01",
  "local": "Belo Horizonte",
  "preco": 180
}'

$novoEvento = Invoke-WebRequest -Uri "$baseUrl/eventos" -Method POST -UseBasicParsing `
  -Body $eventoBody -ContentType "application/json"
Write-Host "Resposta: $($novoEvento.Content)" -ForegroundColor Green

# 3. POST - Criar Cupom
Write-Host "`n3. Criando cupom de desconto..." -ForegroundColor Yellow
$cupomBody = '{
  "codigo": "BLACKFRIDAY",
  "desconto": 50
}'

$novoCupom = Invoke-WebRequest -Uri "$baseUrl/cupons" -Method POST -UseBasicParsing `
  -Body $cupomBody -ContentType "application/json"
Write-Host "Resposta: $($novoCupom.Content)" -ForegroundColor Green

# 4. POST - Criar Usuário
Write-Host "`n4. Criando novo usuário..." -ForegroundColor Yellow
$usuarioBody = '{
  "nome": "Pedro Costa",
  "email": "pedro@email.com",
  "cpf": "98765432100"
}'

$novoUsuario = Invoke-WebRequest -Uri "$baseUrl/usuarios" -Method POST -UseBasicParsing `
  -Body $usuarioBody -ContentType "application/json"
Write-Host "Resposta: $($novoUsuario.Content)" -ForegroundColor Green

# 5. POST - Testar CPF duplicado (erro 400)
Write-Host "`n5. Testando validação de CPF duplicado..." -ForegroundColor Yellow
$usuarioDuplicado = '{
  "nome": "Outro Usuario",
  "email": "outro@email.com",
  "cpf": "98765432100"
}'

try {
  $response = Invoke-WebRequest -Uri "$baseUrl/usuarios" -Method POST -UseBasicParsing `
    -Body $usuarioDuplicado -ContentType "application/json"
} catch {
  $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
  $errorBody = $reader.ReadToEnd()
  Write-Host "Erro 400 (esperado): $errorBody" -ForegroundColor Red
}

Write-Host "`n=== TESTES CONCLUIDOS ===" -ForegroundColor Green
```

## Executar

```bash
# No PowerShell
.\teste-api.ps1
```

Certifique-se de que a API está rodando antes de executar o script:
```bash
dotnet bin/Debug/net10.0/TicketPrime.dll
```
