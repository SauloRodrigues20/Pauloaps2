# Biblioteca Digital - Sistema de Gerenciamento

Sistema completo de gerenciamento de biblioteca desenvolvido com **ASP.NET Core MVC** seguindo os princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**.

## 📋 Sobre o Projeto

Este é um sistema web para gerenciamento de uma biblioteca digital que permite:
- Cadastro e gerenciamento de livros
- Cadastro e gerenciamento de autores
- Controle de empréstimos e devoluções
- Dashboard com estatísticas da biblioteca
- Validação de disponibilidade de livros
- Identificação de empréstimos atrasados

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture** com separação clara em 4 camadas:

### 1. **Library.Domain** (Camada de Domínio)
- Entidades de negócio: `Book`, `Author`, `Loan`, `LoanStatus`
- Lógica de domínio rica (não anêmica)
- Validações de negócio com DataAnnotations
- Métodos de domínio: `BorrowCopy()`, `ReturnCopy()`, `ValidateISBN()`, `IsOverdue()`

### 2. **Library.Application** (Camada de Aplicação)
- ViewModels para transferência de dados
- Interfaces de serviços: `IBookService`, `IAuthorService`, `ILoanService`
- Implementação de serviços com lógica de aplicação
- Mapeamento entre entidades e ViewModels

### 3. **Library.Infrastructure** (Camada de Infraestrutura)
- `LibraryDbContext` com Entity Framework Core
- Repositórios: `BookRepository`, `AuthorRepository`, `LoanRepository`
- Configurações do banco de dados com Fluent API
- Factory para migrations (`LibraryDbContextFactory`)

### 4. **Library.Web** (Camada de Apresentação)
- Controllers MVC: `HomeController`, `BooksController`, `AuthorsController`, `LoansController`
- Views Razor com Bootstrap 5
- Injeção de Dependências configurada
- Validação client-side e server-side

## 🛠️ Tecnologias Utilizadas

- **.NET 9.0**
- **ASP.NET Core MVC**
- **Entity Framework Core 9.0**
- **SQL Server** (LocalDB para desenvolvimento)
- **Bootstrap 5** (UI responsiva)
- **jQuery** (validação client-side)

## 📦 Estrutura de Pastas

```
Pauloaps2/
├── src/
│   ├── Library.Domain/
│   │   ├── Entities/
│   │   │   ├── Author.cs
│   │   │   ├── Book.cs
│   │   │   ├── Loan.cs
│   │   │   └── LoanStatus.cs
│   │   └── Library.Domain.csproj
│   │
│   ├── Library.Application/
│   │   ├── ViewModels/
│   │   │   ├── AuthorViewModel.cs
│   │   │   ├── BookViewModel.cs
│   │   │   └── LoanViewModel.cs
│   │   ├── Interfaces/
│   │   │   ├── IAuthorService.cs
│   │   │   ├── IBookService.cs
│   │   │   └── ILoanService.cs
│   │   ├── Services/
│   │   │   ├── AuthorService.cs
│   │   │   ├── BookService.cs
│   │   │   └── LoanService.cs
│   │   └── Library.Application.csproj
│   │
│   ├── Library.Infrastructure/
│   │   ├── Data/
│   │   │   └── LibraryDbContext.cs
│   │   ├── Repositories/
│   │   │   ├── AuthorRepository.cs
│   │   │   ├── BookRepository.cs
│   │   │   ├── IAuthorRepository.cs
│   │   │   ├── IBookRepository.cs
│   │   │   ├── ILoanRepository.cs
│   │   │   └── LoanRepository.cs
│   │   ├── Factory/
│   │   │   └── LibraryDbContextFactory.cs
│   │   ├── Migrations/
│   │   └── Library.Infrastructure.csproj
│   │
│   └── Library.Web/
│       ├── Controllers/
│       │   ├── AuthorsController.cs
│       │   ├── BooksController.cs
│       │   ├── HomeController.cs
│       │   └── LoansController.cs
│       ├── Views/
│       │   ├── Authors/ (Index, Details, Create, Edit, Delete)
│       │   ├── Books/ (Index, Details, Create, Edit, Delete)
│       │   ├── Loans/ (Index, Details, Create, Return)
│       │   ├── Home/ (Index, Privacy)
│       │   └── Shared/ (_Layout, _ValidationScriptsPartial)
│       ├── wwwroot/
│       ├── Program.cs
│       ├── appsettings.json
│       └── Library.Web.csproj
│
├── Library.sln
├── .gitignore
└── README.md
```

## 🚀 Como Executar

### Pré-requisitos
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb) ou SQL Server

### Passo a Passo

1. **Clone o repositório**
```bash
git clone https://github.com/SauloRodrigues20/Pauloaps2.git
cd Pauloaps2
```

2. **Restaure as dependências**
```bash
dotnet restore
```

3. **Configure a connection string** (opcional)
   
   Edite `src/Library.Web/appsettings.json` se necessário:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

4. **Aplique as migrations para criar o banco de dados**
```bash
dotnet ef database update --project src/Library.Infrastructure --startup-project src/Library.Web
```

5. **Execute a aplicação**
```bash
dotnet run --project src/Library.Web
```

6. **Acesse no navegador**
   - HTTPS: `https://localhost:5001`
   - HTTP: `http://localhost:5000`

## 📱 Funcionalidades

### Dashboard
- Total de livros cadastrados
- Total de autores
- Empréstimos ativos
- Empréstimos atrasados
- Ações rápidas para adicionar livros, autores e realizar empréstimos

### Gerenciamento de Livros
- ✅ Listar todos os livros com busca por título
- ✅ Visualizar detalhes de um livro
- ✅ Adicionar novo livro com validações
- ✅ Editar livro existente
- ✅ Excluir livro (com validação de empréstimos ativos)
- ✅ Controle de cópias disponíveis e total

### Gerenciamento de Autores
- ✅ Listar todos os autores
- ✅ Visualizar detalhes e livros do autor
- ✅ Adicionar novo autor
- ✅ Editar autor existente
- ✅ Excluir autor (com validação de livros associados)

### Gerenciamento de Empréstimos
- ✅ Realizar novo empréstimo (decrementar cópias disponíveis)
- ✅ Registrar devolução (incrementar cópias disponíveis)
- ✅ Listar todos os empréstimos
- ✅ Visualizar empréstimos ativos
- ✅ Visualizar empréstimos atrasados
- ✅ Validação de disponibilidade antes de emprestar

## 🎯 Princípios Aplicados

### Clean Architecture
- ✅ Separação clara de responsabilidades por camada
- ✅ Inversão de dependência (camadas externas dependem de interfaces das internas)
- ✅ Independência de frameworks na camada de domínio
- ✅ Testabilidade facilitada

### SOLID
- **S** - Single Responsibility: Cada classe tem uma única responsabilidade
- **O** - Open/Closed: Aberto para extensão, fechado para modificação
- **L** - Liskov Substitution: Uso de interfaces e contratos
- **I** - Interface Segregation: Interfaces específicas por funcionalidade
- **D** - Dependency Inversion: Inversão de controle com DI

### DDD (Domain-Driven Design)
- ✅ Entidades com lógica de negócio rica
- ✅ Validações de domínio dentro das entidades
- ✅ Linguagem ubíqua (termos do domínio)
- ✅ Agregados e relações bem definidas

### Boas Práticas
- ✅ Async/await em todas as operações I/O
- ✅ Try-catch para tratamento de exceções
- ✅ Logging com ILogger
- ✅ Validação client-side e server-side
- ✅ Mensagens em português para o usuário
- ✅ Comentários XML em métodos públicos
- ✅ ViewModels/DTOs para trafegar dados entre camadas

## 🗄️ Banco de Dados

### Entidades e Relacionamentos

- **Authors** (1:N) **Books**: Um autor pode ter vários livros
- **Books** (1:N) **Loans**: Um livro pode ter vários empréstimos
- Restrições de integridade referencial (DeleteBehavior.Restrict)
- Índice único na coluna ISBN

### Migrations

Para criar uma nova migration:
```bash
dotnet ef migrations add NomeDaMigration --project src/Library.Infrastructure --startup-project src/Library.Web
```

Para aplicar migrations:
```bash
dotnet ef database update --project src/Library.Infrastructure --startup-project src/Library.Web
```

Para remover a última migration:
```bash
dotnet ef migrations remove --project src/Library.Infrastructure --startup-project src/Library.Web
```

## 📚 Regras de Negócio

1. **Livros**
   - ISBN deve ser único
   - Número de cópias disponíveis não pode exceder o total
   - Livros com empréstimos ativos não podem ser excluídos

2. **Autores**
   - Nome e sobrenome são obrigatórios
   - Autores com livros associados não podem ser excluídos

3. **Empréstimos**
   - Apenas livros com cópias disponíveis podem ser emprestados
   - Ao emprestar, decrementa o número de cópias disponíveis
   - Ao devolver, incrementa o número de cópias disponíveis
   - Empréstimos são marcados como atrasados quando passam da data de devolução

## 🧪 Testes

Para compilar o projeto:
```bash
dotnet build
```

Para rodar em modo de desenvolvimento:
```bash
dotnet watch run --project src/Library.Web
```

## 📄 Licença

Este projeto foi desenvolvido para fins educacionais como parte do trabalho da disciplina de Programação de Aplicações.

## 👥 Autor

Desenvolvido por SauloRodrigues20

---

**Nota**: Este projeto demonstra a aplicação prática de Clean Architecture, DDD e princípios SOLID em uma aplicação ASP.NET Core MVC completa e funcional.