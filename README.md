# API RESTful de Blog

API para gerenciar posts de blog e comentários, desenvolvida em C# .NET com Entity Framework Core.

## ⚙️ Configuração e Execução

### 1. Clone o repositório

```bash
git clone https://github.com/TavaresLuc/APIBlogPost
```

```bash
cd APIBlogPost
```

```bash
cd APIBlogPost
```

### 2. Restaurar dependências

```bash
dotnet restore
```

### 3. Criar o banco de dados

```bash
dotnet ef database update
```

### 4. Executar a aplicação

```bash
dotnet run
```
### 4. Executar a aplicação

<img width="1082" height="107" alt="image" src="https://github.com/user-attachments/assets/f94f56d3-3a03-496a-bf8c-c8a405c3a015" />

```bash
Agora basta acessar http://localhost:5175/swagger e realizar os testes na API. 
```

<img width="1509" height="683" alt="image" src="https://github.com/user-attachments/assets/ea14913b-adc6-4453-bf52-409c489d3648" />


Ou pressione **F5** no Visual Studio.

## 📡 Endpoints da API

### Posts

#### `GET /api/posts`
Lista todos os posts com contagem de comentários.

**Resposta:**
```json
[
  {
    "id": 1,
    "title": "Meu primeiro post",
    "content": "Conteúdo do post",
    "createdAt": "2024-11-05T10:30:00",
    "commentCount": 3
  }
]
```

#### `POST /api/posts`
Cria um novo post.

**Body:**
```json
{
  "title": "Título do post",
  "content": "Conteúdo do post"
}
```

#### `GET /api/posts/{id}`
Retorna um post específico com todos os comentários.

**Resposta:**
```json
{
  "id": 1,
  "title": "Meu primeiro post",
  "content": "Conteúdo do post",
  "createdAt": "2024-11-05T10:30:00",
  "commentCount": 2,
  "comments": [
    {
      "id": 1,
      "content": "Ótimo post!",
      "createdAt": "2024-11-05T11:00:00"
    }
  ]
}
```

#### `POST /api/posts/{id}/comments`
Adiciona um comentário a um post.

**Body:**
```json
{
  "content": "Meu comentário"
}
```

## 🗄️ Estrutura do Banco de Dados

### BlogPosts
- `Id` (int, PK, Identity)
- `Title` (string, 250)
- `Content` (string, max)
- `CreatedAt` (datetime)

### Comments
- `Id` (int, PK, Identity)
- `Content` (string, 500)
- `BlogPostId` (int, FK)
- `CreatedAt` (datetime)

## 🛠️ Solução de Problemas

### Erro: "Cannot open database"
Execute: `dotnet ef database update`

### Erro: "dotnet-ef not found"
Instale a ferramenta globalmente:
```bash
dotnet tool install --global dotnet-ef
```

### Erro ao conectar no SQL Server
Verifique se o LocalDB está instalado:
```bash
SqlLocalDB info
```

## 📁 Estrutura do Projeto

```
APIBlogPost/
├── Controllers/      # Endpoints da API
├── Data/            # DbContext e configurações do EF Core
├── DTOs/            # Data Transfer Objects
├── Models/          # Entidades do banco de dados
├── Migrations/      # Migrações do Entity Framework
├── Services/        # Lógica de negócios (futuro)
├── Program.cs       # Configuração da aplicação
└── appsettings.json # Configurações e connection string
```

## 📝 Notas

- A API usa SQL Server LocalDB (sem necessidade de instalar SQL Server completo)
- O banco de dados é criado automaticamente na primeira execução
- Swagger está habilitado apenas em ambiente de desenvolvimento
- A autenticação usa Windows Authentication, então não precisa configurar login e senha utilizado no SSMS
