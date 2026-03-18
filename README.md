# 🎟️ API de Gestão de Eventos(TicketPrime) - (Pa Pântano)

## 📌 Visão Geral

Esta **API** é uma aplicação backend desenvolvida em **C#**, com o objetivo de gerenciar eventos e a comercialização de ingressos.

A plataforma permite que clientes (organizadores) criem e administrem eventos, enquanto usuários podem visualizar, adquirir ingressos e utilizar cupons de desconto.

---

## 🎯 Objetivos do Projeto

* Desenvolver uma API RESTful utilizando C#
* Aplicar conceitos de arquitetura backend
* Implementar regras de negócio para eventos e ingressos
* Trabalhar autenticação lógica entre diferentes tipos de usuários
* Praticar versionamento com Git e GitHub

---

## 👥 Histórias de Usuário

* Como cliente, quero criar eventos para disponibilizar ingressos.
* Como cliente, quero gerenciar meus eventos para manter as informações atualizadas.
* Como usuário, quero visualizar eventos disponíveis para escolha.
* Como usuário, quero comprar ingressos para garantir minha participação.
* Como usuário, quero aplicar cupons de desconto para pagar menos.

---

## 🛠️ Tecnologias Utilizadas

* **C#**
* **.NET (ASP.NET Core)** *(caso tenha usado, recomendo deixar)*
* Git & GitHub

---

## ⚙️ Funcionalidades

### 👤 Usuário

* Visualizar eventos disponíveis
* Comprar ingressos
* Aplicar cupons de desconto

### 🧑‍💼 Cliente (Organizador)

* Criar eventos
* Editar eventos
* Excluir eventos

### 🎟️ Sistema

* Gerenciamento de ingressos
* Aplicação de cupons
* Controle de disponibilidade

---

## 📂 Estrutura do Projeto

```bash
📁 Pa-pantano
 ┣ 📂 Controllers
 ┣ 📂 Models
 ┣ 📂 Services
 ┣ 📂 Repositories
 ┣ 📜 Program.cs
 ┗ 📜 README.md
```

---

## ▶️ Como Executar o Projeto

1. Clone o repositório:

```bash
git clone https://github.com/Kadu-Quintella/Pa-pantano.git
```

2. Acesse a pasta do projeto:

```bash
cd Pa-pantano
```

3. Restaure as dependências:

```bash
dotnet restore
```

4. Execute a aplicação:

```bash
dotnet run
```

---

## 🔗 Endpoints (Exemplo)

### Eventos

* `GET /eventos` → Lista todos os eventos
* `POST /eventos` → Cria um novo evento
* `PUT /eventos/{id}` → Atualiza um evento
* `DELETE /eventos/{id}` → Remove um evento

### Ingressos

* `POST /ingressos/comprar` → Compra de ingresso

### Cupons

* `POST /cupons/aplicar` → Aplicação de cupom

---

## 🚧 Status do Projeto

🟡 Em Análise

---

## 💡 Melhorias Futuras

* Implementação de autenticação (JWT)
* Integração com banco de dados relacional
* Criação de interface frontend
* Deploy em nuvem

---

## 👨‍💻 Autores

Arthur Martins
Carlos Eduardo
Caio de Paiva
Emanuel de Oliveira
Laura Lima

🔗 GitHub: https://github.com/Kadu-Quintella

---

## 📄 Licença

Este projeto é destinado para fins acadêmicos e aprendizado.
