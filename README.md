## VideoShare API  

[![.NET](https://github.com/Matchiga/VideoShare-API/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Matchiga/VideoShare-API/actions/workflows/dotnet.yml)

API RESTful desenvolvida em C# para uma plataforma de compartilhamento de vídeos, permitindo aos usuários criar e gerenciar seus vídeos favoritos.

### Funcionalidades

- **Gerenciamento de Vídeos:**
    - Criação de playlists com título, url e descrição do vídeo.
    - Adição e remoção de vídeos utilizando Id.
    - Listagem de todas os vídeo.
    - Busca do vídeo por Id.
- **Gerenciamento de Vídeos:**
    - Adição de vídeos à plataforma através de links (URLs).
    - Listagem de vídeos de uma playlist.

### Tecnologias Utilizadas

- **Linguagem de Programação:** C#
- **Framework:** .NET 8
- **Banco de Dados:** SQL Server
- **ORM:** Entity Framework
- **Ferramentas:**
    - Visual Studio
    - Git

### Como Executar o Projeto

1. **Pré-requisitos:**
    - Ter o .NET SDK 8.0 instalado.
    - Ter o  (especificar dependências como o banco de dados) instalado e configurado.
2. **Clonar o Repositório:**
   ```bash
   git clone https://github.com/Matchiga/AluraFlix.git
   ```
3. **Configurar o Banco de Dados:**
    - Atualizar a string de conexão com o banco de dados no arquivo `appsettings.json`.
4. **Executar as Migrações (se aplicável):**
    - Abrir o console do gerenciador de pacotes e navegar até a pasta do projeto.
    - Executar o comando `Update-Database` para aplicar as migrações.
5. **Iniciar a Aplicação:**
    - Executar o comando `dotnet run` na pasta do projeto.

### Rotas da API

**Playlists:**

- `GET /Videos`: Retorna todas as playlists do usuário autenticado.
- `GET /Videos/{id}`: Retorna uma playlist específica pelo ID.
- `POST /Videos`: Cria uma nova playlist.
- `PUT /Videos`: Atualiza uma playlist existente.
- `DELETE /Videos/{id}`: Exclui uma playlist.

### Autor

- [Matheus Chiga](https://github.com/Matchiga/)
