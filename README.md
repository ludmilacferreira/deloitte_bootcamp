# Deloitte Bootcamp

Repositorio com os exercicios e projetos desenvolvidos durante o Bootcamp da Deloitte, utilizando C# e .NET.

## Estrutura

| Pasta | Descricao |
|---|---|
| `dia01/` | Introducao ao C# e primeira API piloto |
| `dia02/` | Case de check-in |
| `dia03/` | Validacao de visitantes com POO |
| `dia05/` | Gestao de visitantes em coworking |
| `case03/` | Controle de estoque com validacao de produtos |
| `MinhaApi/` | API REST completa de lotes de minerio (CRUD) com Entity Framework Core e PostgreSQL |
| `MinhaApi.Tests/` | Testes unitarios da API, incluindo fila de processamento com Redis |

## Tecnologias

- **Linguagem:** C# / .NET
- **Banco de dados:** PostgreSQL 16 (via Docker)
- **ORM:** Entity Framework Core
- **Fila:** Redis
- **Testes:** xUnit

## Como executar a API

```bash
# Subir o banco de dados
docker-compose up -d

# Executar a API
cd MinhaApi
dotnet run
```

A API estara disponivel em `https://localhost:5001/api/LotesMinerio`.
