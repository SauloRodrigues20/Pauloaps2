# Firebase Integration Guide

## Configuração do Firebase

Este projeto está integrado com o Firebase para:
- **Firebase Authentication**: Autenticação de usuários
- **Firebase Storage**: Armazenamento de imagens de capas de livros

## Configuração Atual

As credenciais do Firebase estão configuradas em `appsettings.json`:

```json
{
  "Firebase": {
    "ApiKey": "AIzaSyA4XviYC0EnRmuYQc4ljJj5KRaG2svdKCQ",
    "AuthDomain": "aps-livraria.firebaseapp.com",
    "ProjectId": "aps-livraria",
    "StorageBucket": "aps-livraria.firebasestorage.app",
    "MessagingSenderId": "696453801817",
    "AppId": "1:696453801817:web:1ad0c3ed545a3784170e48"
  }
}
```

## Para Produção

Para usar o Firebase em produção, você precisa:

1. **Baixar o arquivo de credenciais da conta de serviço**:
   - Acesse o [Firebase Console](https://console.firebase.google.com/)
   - Selecione seu projeto: `aps-livraria`
   - Vá em **Configurações do Projeto** > **Contas de Serviço**
   - Clique em **Gerar nova chave privada**
   - Salve o arquivo JSON baixado como `firebase-credentials.json` na raiz do projeto

2. **Configure a variável de ambiente**:
   ```bash
   export GOOGLE_APPLICATION_CREDENTIALS="/path/to/firebase-credentials.json"
   ```

3. **Ou atualize o Program.cs** para carregar o arquivo diretamente:
   ```csharp
   var credential = GoogleCredential.FromFile("firebase-credentials.json");
   ```

## Serviços Disponíveis

### IFirebaseAuthService
- `CreateUserAsync`: Criar novo usuário
- `VerifyIdTokenAsync`: Verificar token de autenticação
- `DeleteUserAsync`: Deletar usuário
- `GeneratePasswordResetLinkAsync`: Gerar link de recuperação de senha
- `UpdateUserAsync`: Atualizar informações do usuário

### IFirebaseStorageService
- `UploadFileAsync`: Fazer upload de imagem
- `DeleteFileAsync`: Deletar imagem
- `GetFileUrlAsync`: Obter URL da imagem

## Exemplo de Uso no Controller

```csharp
public class BooksController : Controller
{
    private readonly IBookService _bookService;
    private readonly IFirebaseStorageService _storageService;

    public BooksController(IBookService bookService, IFirebaseStorageService storageService)
    {
        _bookService = bookService;
        _storageService = storageService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookViewModel model, IFormFile? coverImage)
    {
        if (coverImage != null)
        {
            using var stream = coverImage.OpenReadStream();
            model.ImageUrl = await _storageService.UploadFileAsync(
                stream, 
                coverImage.FileName, 
                coverImage.ContentType
            );
        }

        // Resto da lógica...
        return View();
    }
}
```

## Banco de Dados

A entidade `Book` foi atualizada para incluir a propriedade `ImageUrl`:

```csharp
public string? ImageUrl { get; set; }
```

Uma migration foi criada para adicionar esta coluna ao banco de dados.

## Aplicar a Migration

Para aplicar a migration ao banco de dados, execute:

```bash
dotnet ef database update --project src/Library.Infrastructure/Library.Infrastructure.csproj --startup-project src/Library.Web/Library.Web.csproj
```

## Configurar Regras do Firebase Storage

No Firebase Console, configure as regras de segurança do Storage:

```
rules_version = '2';
service firebase.storage {
  match /b/{bucket}/o {
    match /books/{allPaths=**} {
      allow read: if true; // Público para leitura
      allow write: if request.auth != null; // Somente usuários autenticados podem fazer upload
    }
  }
}
```

## Próximos Passos

1. Baixar as credenciais da conta de serviço do Firebase
2. Aplicar a migration no banco de dados
3. Atualizar o controller de Books para suportar upload de imagens
4. Criar interface de usuário para upload de capas de livros
