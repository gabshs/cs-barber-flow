# 💈 BarberFlow

Um sistema de gestão completo para barbearias, desenvolvido em .NET 8 com arquitetura limpa e boas práticas de desenvolvimento.

## 📋 Sobre o Projeto

BarberFlow é uma API REST para gerenciamento de barbearias que permite:

- ✅ **Gestão de Faturamentos**: Registro, consulta, atualização e exclusão de faturamentos
- 📊 **Relatórios em PDF**: Geração automática de relatórios de faturamento
- 💰 **Controle Financeiro**: Acompanhamento de valores e métodos de pagamento
- 🔍 **Busca e Filtros**: Sistema de busca paginada com filtros
- 📱 **API RESTful**: Interface moderna e bem documentada

## 🛠️ Tecnologias Utilizadas

### Backend

- **[.NET 8](https://dotnet.microsoft.com/)** - Framework principal
- **[Entity Framework Core](https://docs.microsoft.com/ef/)** - ORM para acesso a dados
- **[MySQL](https://www.mysql.com/)** - Banco de dados
- **[AutoMapper](https://automapper.org/)** - Mapeamento de objetos
- **[MigraDoc](http://www.pdfsharp.net/wiki/migradoc-overview.aspx)** - Geração de PDFs
- **[Swagger/OpenAPI](https://swagger.io/)** - Documentação da API

### Arquitetura

- **Clean Architecture** - Separação clara de responsabilidades
- **SOLID Principles** - Princípios de desenvolvimento limpo
- **Repository Pattern** - Abstração da camada de dados
- **Use Cases Pattern** - Lógica de negócio isolada

## 🏗️ Estrutura do Projeto

```
BarberFlow/
├── src/
│   ├── BarberFlow.API/              # 🎯 Camada de apresentação
│   │   ├── Controllers/             # Controladores da API
│   │   ├── Filters/                 # Filtros de exceção
│   │   └── Program.cs               # Configuração da aplicação
│   │
│   ├── BarberFlow.Application/      # 📋 Camada de aplicação
│   │   ├── UseCases/               # Casos de uso
│   │   ├── AutoMapper/             # Configurações de mapeamento
│   │   └── DependencyInjection...  # Injeção de dependência
│   │
│   ├── BarberFlow.Communication/    # 📡 Contratos de comunicação
│   │   ├── Requests/               # DTOs de entrada
│   │   ├── Responses/              # DTOs de saída
│   │   └── Enums/                  # Enumeradores
│   │
│   ├── BarberFlow.Domain/          # 🧠 Camada de domínio
│   │   ├── Entities/               # Entidades do negócio
│   │   ├── Enums/                  # Enumeradores do domínio
│   │   └── Repositories/           # Contratos de repositório
│   │
│   ├── BarberFlow.Infrastructure/   # 🔧 Camada de infraestrutura
│   │   ├── DataAccess/             # Contexto e repositórios
│   │   └── DependencyInjection...  # Configurações de infraestrutura
│   │
│   └── BarberFlow.Exception/       # ⚠️ Tratamento de exceções
│       └── ExceptionBase/          # Exceções customizadas
│
└── README.md
```

## 🚀 Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL 8.0+](https://dev.mysql.com/downloads/)
- [Git](https://git-scm.com/)

### 1️⃣ Clone o repositório

```bash
git clone https://github.com/seu-usuario/BarberFlow.git
cd BarberFlow
```

### 2️⃣ Configure o banco de dados

1. Crie um banco MySQL local
2. Configure a string de conexão no `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BarberFlowDB;Uid=root;Pwd=sua_senha;"
  }
}
```

### 3️⃣ Execute as migrações

```bash
dotnet ef migrations add InitialCreate --project src/BarberFlow.Infrastructure --startup-project src/BarberFlow.API
dotnet ef database update --project src/BarberFlow.Infrastructure --startup-project src/BarberFlow.API
```

### 4️⃣ Execute a aplicação

```bash
dotnet run --project src/BarberFlow.API
```

### 5️⃣ Acesse a documentação

- **Swagger UI**: https://localhost:7115/swagger
- **API Base URL**: https://localhost:7115/api

## 📚 Endpoints da API

### 💰 Faturamentos (`/api/billings`)

| Método   | Endpoint                                  | Descrição                    |
| -------- | ----------------------------------------- | ---------------------------- |
| `GET`    | `/api/billings`                           | Lista faturamentos paginados |
| `GET`    | `/api/billings/{id}`                      | Busca faturamento por ID     |
| `POST`   | `/api/billings`                           | Cria novo faturamento        |
| `PUT`    | `/api/billings/{id}`                      | Atualiza faturamento         |
| `DELETE` | `/api/billings/{id}`                      | Remove faturamento           |
| `GET`    | `/api/billings/reports/pdf?month=2025-10` | Gera relatório PDF           |

### 📋 Exemplo de Requisição (POST)

```json
{
  "date": "2025-10-22",
  "barberName": "João Silva",
  "clientName": "Maria Santos",
  "serviceName": "Corte + Barba",
  "amount": 45.0,
  "paymentMethod": "Cartão",
  "status": "Pago",
  "notes": "Cliente preferencial"
}
```

### 📋 Exemplo de Resposta (GET)

```json
{
  "id": "123e4567-e89b-12d3-a456-426614174000",
  "date": "2025-10-22",
  "barberName": "João Silva",
  "clientName": "Maria Santos",
  "serviceName": "Corte + Barba",
  "amount": 45.0,
  "paymentMethod": "Cartão",
  "status": "Pago",
  "notes": "Cliente preferencial",
  "createdAt": "2025-10-22T14:30:00",
  "updatedAt": "2025-10-22T14:30:00"
}
```

## 🎨 Funcionalidades

### ✨ Principais Features

- **📊 Dashboard Financeiro**: Acompanhamento de faturamento total
- **🔍 Busca Inteligente**: Filtros por barbeiro, cliente ou serviço
- **📄 Relatórios PDF**: Relatórios automáticos com design profissional
- **💳 Múltiplos Pagamentos**: Suporte a Cartão, Dinheiro, PIX, etc.
- **📱 API RESTful**: Interface moderna e bem documentada
- **🔒 Validações**: Validação robusta de dados de entrada
- **⚡ Performance**: Consultas otimizadas com paginação

### 🎯 Enums Disponíveis

**Métodos de Pagamento:**

- `Cartão`
- `Dinheiro`
- `Pix`
- `Outro`

**Status do Faturamento:**

- `Pendente`
- `Pago`
- `Cancelado`

## 🧪 Testes

```bash
# Executar todos os testes
dotnet test

# Executar testes com cobertura
dotnet test --collect:"XPlat Code Coverage"
```

## 🔧 Configuração Avançada

### 🎨 Personalização de Relatórios PDF

Os relatórios PDF podem ser customizados editando os arquivos em:

```
src/BarberFlow.Application/UseCases/Billings/Reports/Pdf/
```

### 🔗 Configuração de CORS

Para desenvolvimento frontend, configure CORS no `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

## 🤝 Contribuição

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 👨‍💻 Autor

**Gabriel Henrique**

- GitHub: [@gabshs](https://github.com/gabrielhenrique)
- LinkedIn: [Gabriel Henrique](https://linkedin.com/in/gabshs)

## 🌟 Agradecimentos

- [Microsoft](https://microsoft.com) pelo .NET
- [Pomelo Foundation](https://github.com/PomeloFoundation) pelo Entity Framework MySQL Provider
- [AutoMapper Team](https://automapper.org/) pela biblioteca de mapeamento
- [PDFsharp Team](http://www.pdfsharp.net/) pela geração de PDFs

---

⭐ Se este projeto te ajudou, considere dar uma estrela no repositório!
