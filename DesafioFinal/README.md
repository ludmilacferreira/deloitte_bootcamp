# Desafio Final - Sistema de Monitoramento de Equipamentos Pesados (Vale)

API REST para gerenciar equipamentos de mina (caminhoes fora-de-estrada, escavadeiras, perfuratrizes, etc).

## Tecnologias

- .NET 10 / C#
- Entity Framework Core 10 + Npgsql
- PostgreSQL 16 (Docker)
- Swagger/OpenAPI
- Insomnia (testes)
- DBeaver (visualizacao do banco)

## Estrutura do Projeto

```
DesafioFinal/
├── Controllers/
│   └── EquipamentosController.cs    # CRUD completo com validacoes
├── Data/
│   ├── AppDbContext.cs               # Contexto do EF Core
│   └── criar-tabela-equipamentos.sql # Script SQL para criar tabela
├── Dtos/
│   ├── CreateEquipamentoDto.cs       # DTO de criacao
│   ├── UpdateEquipamentoDto.cs       # DTO de atualizacao
│   ├── EquipamentoResponseDto.cs     # DTO de resposta
│   └── PagedResultDto.cs             # DTO de paginacao
├── Models/
│   ├── Equipamento.cs                # Entidade principal
│   ├── TipoEquipamento.cs           # Enum de tipos
│   └── StatusOperacional.cs          # Enum de status
├── Properties/
│   └── launchSettings.json
├── appsettings.json
├── appsettings.Development.json
├── docker-compose.yml
├── DesafioFinal.csproj
├── Program.cs
├── Insomnia_DesafioFinal.json        # Colecao do Insomnia (pronta!)
└── README.md
```

---

## PASSO A PASSO COMPLETO

### Pre-requisitos

- Docker Desktop instalado e rodando
- .NET 10 SDK instalado
- DBeaver instalado
- Insomnia instalado

---

### PASSO 1 - Subir o PostgreSQL com Docker

1. Abra o **Terminal** (CMD, PowerShell ou Terminal do VS Code)
2. Navegue ate a pasta do projeto:
   ```bash
   cd DesafioFinal
   ```
3. Suba o container do PostgreSQL:
   ```bash
   docker-compose up -d
   ```
4. Verifique se o container esta rodando:
   ```bash
   docker ps
   ```
   Voce deve ver o container `pg_desafio_final` com status "Up".

5. Para verificar os logs (opcional):
   ```bash
   docker logs pg_desafio_final
   ```

> **Dados de conexao do PostgreSQL:**
> - Host: `localhost`
> - Porta: `5432`
> - Banco: `desafiofinal_db`
> - Usuario: `postgres`
> - Senha: `postgres`

---

### PASSO 2 - Conectar o DBeaver ao PostgreSQL

1. Abra o **DBeaver**
2. Clique em **Nova Conexao** (icone de tomada com +) ou `Ctrl+Shift+N`
3. Selecione **PostgreSQL** e clique em **Avançar**
4. Preencha os campos:
   - **Host:** `localhost`
   - **Port:** `5432`
   - **Database:** `desafiofinal_db`
   - **Username:** `postgres`
   - **Password:** `postgres`
5. Clique em **Test Connection** para verificar
6. Se pedir para baixar o driver, clique em **Download**
7. Clique em **Concluir**

---

### PASSO 3 - Criar a tabela no banco de dados (DBeaver)

1. No DBeaver, com a conexao aberta, clique em **SQL Editor** > **New SQL Script** (ou `Ctrl+]`)
2. Copie e cole TODO o conteudo do arquivo `Data/criar-tabela-equipamentos.sql`
3. Execute o script inteiro clicando em **Execute SQL Script** (botao de play laranja ou `Ctrl+Enter`)
4. Voce vera a mensagem de sucesso e os 5 registros de exemplo inseridos
5. Para conferir: no painel esquerdo, expanda `desafiofinal_db > Schemas > public > Tables > equipamentos`
6. Clique com botao direito na tabela > **View Data** para ver os registros

---

### PASSO 4 - Rodar a API .NET

1. No terminal, dentro da pasta `DesafioFinal`:
   ```bash
   dotnet restore
   ```
2. Rode a API:
   ```bash
   dotnet run
   ```
3. A API vai iniciar em: `http://localhost:5164`
4. Para testar rapidamente no navegador, acesse:
   ```
   http://localhost:5164/api/equipamentos?page=1&pageSize=10
   ```
   Voce deve ver o JSON com os equipamentos.

---

### PASSO 5 - Configurar e testar no Insomnia

#### 5.1 - Importar a colecao

1. Abra o **Insomnia**
2. Va em **Application** > **Preferences** > **Data** > **Import Data** > **From File**
   (Ou: Menu Hamburguer > Import/Export > Import Data > From File)
3. Selecione o arquivo `Insomnia_DesafioFinal.json` que esta na pasta do projeto
4. Clique em **Import**
5. No painel esquerdo, voce vera a collection **"Desafio Final - Equipamentos Pesados"** com a pasta **"Equipamentos"**

#### 5.2 - Requests disponiveis na colecao

A colecao ja vem com 15 requests organizados:

| # | Request | Metodo | Resultado esperado |
|---|---------|--------|--------------------|
| 1 | Criar Equipamento | POST | 201 Created |
| 2 | Criar Escavadeira | POST | 201 Created |
| 3 | Erro: Codigo Duplicado | POST | 409 Conflict |
| 4 | Erro: Horimetro Negativo | POST | 400 Bad Request |
| 5 | Erro: Tipo Invalido | POST | 400 Bad Request |
| 6 | Listar Todos (paginado) | GET | 200 OK |
| 7 | Filtrar por Tipo=Caminhao | GET | 200 OK |
| 8 | Filtrar por Status=EmManutencao | GET | 200 OK |
| 9 | Filtrar por Codigo (parcial) | GET | 200 OK |
| 10 | Detalhe por ID | GET | 200 OK |
| 11 | Detalhe ID inexistente | GET | 404 Not Found |
| 12 | Atualizar Equipamento | PUT | 204 No Content |
| 13 | Atualizar ID inexistente | PUT | 404 Not Found |
| 14 | Remover Equipamento | DELETE | 204 No Content |
| 15 | Remover ID inexistente | DELETE | 404 Not Found |

#### 5.3 - Ordem de teste recomendada

1. **GET Listar Todos** - confirme que os 5 registros do SQL aparecem
2. **POST Criar Equipamento** - crie um novo (CAT-793F-000999)
3. **POST Criar Escavadeira** - crie outro (LIE-R9800-0099)
4. **GET Listar Todos** - agora deve ter 7 registros
5. **POST Codigo Duplicado** - tente criar com mesmo codigo (deve dar 409)
6. **POST Horimetro Negativo** - deve dar 400
7. **POST Tipo Invalido** - deve dar 400
8. **GET Detalhe por ID** - busque o id=1
9. **GET Filtrar por Tipo** - filtre por Caminhao
10. **GET Filtrar por Status** - filtre por EmManutencao
11. **GET Filtrar por Codigo** - busque por "CAT"
12. **PUT Atualizar** - atualize o id=1
13. **GET Detalhe por ID** - confirme que foi atualizado
14. **DELETE Remover** - remova o id=1
15. **GET Detalhe por ID** - confirme que retorna 404

---

### PASSO 6 - Verificar no DBeaver (apos testes)

1. Volte ao DBeaver
2. Clique com botao direito na tabela `equipamentos` > **View Data**
3. Clique em **Refresh** (F5) para ver os dados atualizados
4. Voce pode rodar queries SQL manualmente:
   ```sql
   SELECT * FROM public.equipamentos ORDER BY id;
   ```

---

## Endpoints da API

| Metodo | Rota | Descricao |
|--------|------|-----------|
| POST | `/api/equipamentos` | Criar equipamento |
| GET | `/api/equipamentos` | Listar com paginacao e filtros |
| GET | `/api/equipamentos/{id}` | Detalhe por ID |
| PUT | `/api/equipamentos/{id}` | Atualizar equipamento |
| DELETE | `/api/equipamentos/{id}` | Remover equipamento |

### Parametros de query (GET lista):
- `page` (int, default: 1)
- `pageSize` (int, default: 10, max: 50)
- `tipo` (string: Caminhao, Escavadeira, Perfuratriz, Carregadeira, Trator)
- `status` (string: Operacional, EmManutencao, Parado)
- `codigo` (string: busca parcial, case-insensitive)

### Payload de exemplo (POST/PUT):

```json
{
  "codigo": "CAT-793F-000123",
  "tipo": "Caminhao",
  "modelo": "Caterpillar 793F",
  "horimetro": 18234.5,
  "statusOperacional": "Operacional",
  "dataAquisicao": "2019-03-15",
  "localizacaoAtual": "Mina Carajas N4E"
}
```

---

## Regras de Negocio Implementadas

- **Codigo** obrigatorio, unico, sem espacos nas extremidades (trim)
- **Horimetro** nao pode ser negativo
- **Tipo** aceita apenas: Caminhao, Escavadeira, Perfuratriz, Carregadeira, Trator
- **StatusOperacional** aceita apenas: Operacional, EmManutencao, Parado
- **Modelo** obrigatorio
- Codigo duplicado retorna **409 Conflict**
- ID inexistente retorna **404 Not Found**
- Validacao invalida retorna **400 Bad Request**

---

## Comandos uteis

```bash
# Subir o PostgreSQL
docker-compose up -d

# Parar o PostgreSQL
docker-compose down

# Parar e apagar os dados do volume
docker-compose down -v

# Restaurar pacotes .NET
dotnet restore

# Rodar a API
dotnet run

# Rodar a API com hot-reload
dotnet watch run
```
