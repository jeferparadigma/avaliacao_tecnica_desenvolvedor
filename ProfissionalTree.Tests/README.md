# 🌳 ProfessionalTree

[![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Tests](https://img.shields.io/badge/Tests-4%2F4%20Passing-brightgreen.svg)](./docs/TESTS.md)

> Construa árvores binárias profissionais aplicando SOLID principles e Design Patterns em C#

## 📋 Visão Geral

**ProfessionalTree** é uma aplicação C# que constrói árvores binárias a partir de arrays inteiros seguindo regras específicas:

- ✅ **Raiz** = maior valor do array
- ✅ **Galho Esquerdo** = valores à esquerda em ordem decrescente
- ✅ **Galho Direito** = valores à direita em ordem decrescente

### Características

- 🏗️ Arquitetura baseada em **SOLID Principles**
- 🎯 Implementação de **Design Patterns** (Strategy, Factory, Composite)
- 🧪 Testes unitários com **Xunit**
- 📝 Documentação **XML**
- 🔒 **Type-safe** com C# moderno (.NET 10.0)
- 🔌 **Injeção de Dependência** integrada

## 🚀 Quick Start

### Pré-requisitos

- .NET 10.0 SDK ou superior
- Visual Studio 2022 / VS Code

### Instalação

```bash
# Clonar repositório
git clone https://github.com/seu-usuario/ProfessionalTree.git
cd ProfessionalTree

# Restaurar dependências
dotnet restore

# Compilar
dotnet build

# Executar
dotnet run --project ProfessionalTree

# Testes
dotnet test
```

## 💡 Exemplos de Uso

### Uso Básico

```csharp
var builder = new BinaryTreeBuilder();
var printer = new ConsolePrinter();

int[] values = { 3, 2, 1, 6, 0, 5 };
var tree = builder.Build(values);

printer.Print(tree, "Cenário 1");
```

**Saída:**
```
==================================================
Cenário 1
==================================================
Raiz: 6
Galhos da esquerda: 3, 2, 1
Galhos da direita: 5, 0
```

### Com Injeção de Dependência

```csharp
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped<ITreeBuilder, BinaryTreeBuilder>();
services.AddScoped<ITreePrinter, ConsolePrinter>();

var provider = services.BuildServiceProvider();
var builder = provider.GetRequiredService<ITreeBuilder>();
var printer = provider.GetRequiredService<ITreePrinter>();

var tree = builder.Build(new int[] { 3, 2, 1, 6, 0, 5 });
printer.Print(tree, "Minha Árvore");
```

## 📚 Documentação

| Documento | Descrição |
|-----------|-----------|
| [ARCHITECTURE.md](./docs/ARCHITECTURE.md) | Arquitetura e padrões |
| [API.md](./docs/API.md) | Referência completa de API |
| [GUIDE.md](./docs/GUIDE.md) | Guia de uso e exemplos |
| [TESTS.md](./docs/TESTS.md) | Estratégia de testes |
| [SOLID.md](./docs/SOLID.md) | Princípios SOLID aplicados |
| [TROUBLESHOOTING.md](./docs/TROUBLESHOOTING.md) | Problemas e soluções |

## 📂 Estrutura do Projeto

```
ProfessionalTree/
├── Models/
│   └── TreeNode.cs              # Modelo de nó
├── Interfaces/
│   ├── ITreeBuilder.cs          # Contrato builder
│   └── ITreePrinter.cs          # Contrato printer
├── Services/
│   ├── BinaryTreeBuilder.cs     # Implementação builder
│   └── ConsolePrinter.cs        # Implementação printer
├── Program.cs                    # Ponto de entrada
├── ProfessionalTree.csproj      # Configuração
└── README.md                     # Este arquivo

ProfessionalTree.Tests/
├── BinaryTreeBuilderTests.cs    # Testes builder
└── BinaryTreeBuilderTests.cs    # Arquivo de teste

docs/
├── ARCHITECTURE.md              # Arquitetura
├── API.md                       # API Reference
├── GUIDE.md                     # Guia de uso
├── TESTS.md                     # Testes
├── SOLID.md                     # SOLID Principles
└── TROUBLESHOOTING.md          # Troubleshooting
```

## 🧪 Testes

```bash
# Executar todos os testes
dotnet test

# Executar teste específico
dotnet test --filter "Build_WithEmptyArray_ReturnsNull"

# Com saída detalhada
dotnet test --logger "console;verbosity=detailed"
```

**Resultado:**
```
Total Tests: 4
Passed: 4
Failed: 0
Result: ✅ PASSED
```

## 🏗️ Arquitetura

```
┌─────────────────────────────────────┐
│        Program.cs (Orquestrador)    │
└────────────┬────────────────────────┘
             │
    ┌────────┼──────────┐
    │        │          │
    ▼        ▼          ▼
 TreeNode  ITreeBuilder ITreePrinter
    │        │          │
    │        ▼          ▼
    │    BinaryTree   Console
    │    Builder      Printer
    │        │        │
    └────────┴────────┘
```

### Princípios SOLID Aplicados

- ✅ **S**RP: Cada classe tem responsabilidade única
- ✅ **O**CP: Aberto para extensão, fechado para modificação
- ✅ **L**SP: Substituição de Liskov respeitada
- ✅ **I**SP: Interfaces segregadas
- ✅ **D**IP: Injeção de dependência

## 🎯 Design Patterns

- **Strategy Pattern**: `ITreeBuilder` e `ITreePrinter`
- **Factory Pattern**: Criação de impressoras
- **Composite Pattern**: Estrutura de árvore
- **Dependency Injection**: Via `ServiceCollection`

## 📋 Cenários de Teste

### Cenário 1
```
Entrada: [3, 2, 1, 6, 0, 5]
Raiz: 6
Esquerda: 3, 2, 1
Direita: 5, 0
```

### Cenário 2
```
Entrada: [7, 5, 13, 9, 1, 6, 4]
Raiz: 13
Esquerda: 7, 5
Direita: 9, 6, 4, 1
```

## 🔧 Tecnologias

- **Linguagem**: C# 12.0
- **Framework**: .NET 10.0
- **Testes**: Xunit
- **Injeção de Dependência**: Microsoft.Extensions.DependencyInjection

## 📦 Dependências

```xml
<PackageReference Include="Microsoft.Extensions.DependencyInjection" 
                  Version="10.0.0" />
```

## 🤝 Contribuindo

1. Fork o repositório
2. Crie uma branch (`git checkout -b feature/minha-feature`)
3. Commit suas mudanças (`git commit -m 'Adiciona minha feature'`)
4. Push para a branch (`git push origin feature/minha-feature`)
5. Abra um Pull Request

## 📝 Licença

Este projeto está sob a licença **MIT**. Veja [LICENSE](LICENSE) para detalhes.

## 👤 Autor

**Seu Nome**
- GitHub: [@seu-usuario](https://github.com/seu-usuario)
- Email: seu.email@example.com

## 📚 Referências

- [SOLID Principles](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles)
- [Design Patterns](https://refactoring.guru/design-patterns/csharp)
- [C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [xUnit.net](https://xunit.net/)

## 🙌 Agradecimentos

Agradecimentos especiais a todos que contribuem para melhorar este projeto!

---

**Última atualização**: Janeiro 2026 | **Versão**: 1.0.0