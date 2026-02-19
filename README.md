Este repositório reúne os exercícios e projetos que desenvolvi durante o Bootcamp da Deloitte, utilizando C# e .NET. Aqui está minha evolução ao longo das aulas, desde os primeiros conceitos até a construção de uma API completa com testes automatizados.
Estrutura do Projeto
PastaDescriçãodia01/Introdução ao C# e criação da primeira API pilotodia02/Desenvolvimento de um case de check-india03/Validação de visitantes aplicando conceitos de POOdia05/Sistema de gestão de visitantes para coworkingcase03/Controle de estoque com validação de produtosMinhaApi/API REST completa para gerenciamento de lotes de minério (CRUD) com Entity Framework Core e PostgreSQLMinhaApi.Tests/Testes unitários da API, incluindo simulação de fila de processamento com Redis
Tecnologias Utilizadas

Linguagem: C# / .NET
Banco de dados: PostgreSQL 16 (via Docker)
ORM: Entity Framework Core
Fila de processamento: Redis
Testes: xUnit

Como executar a API
bash# Subir o banco de dados
docker-compose up -d

# Acessar a pasta da API
cd MinhaApi

# Executar o projeto
dotnet run
Após iniciar, a API estará disponível em: https://localhost:5001/api/LotesMinerio
