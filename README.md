# 🌳 ProfessionalTree

[![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

> Construa árvores binárias profissionais aplicando **SOLID principles** e **Design Patterns** em C#

## 📋 Visão Geral

**ProfessionalTree** é uma aplicação em C# que constrói árvores binárias a partir de arrays inteiros, seguindo regras específicas de organização:

- ✅ **Raiz** = maior valor do array
- ✅ **Galho Esquerdo** = valores à esquerda em ordem decrescente
- ✅ **Galho Direito** = valores à direita em ordem decrescente

### Características Principais

- 🏗️ Arquitetura baseada em **SOLID Principles**
- 🎯 Implementação de **Design Patterns** (Strategy, Factory, Composite)
- 🧪 Testes unitários com **xUnit**
- 📝 Documentação **XML**
- 🔒 **Type-safe** com C# moderno (.NET 10.0)
- 🔌 **Injeção de Dependência** integrada

---

## 🚀 Quick Start

### Pré-requisitos

- **.NET 10.0 SDK** ou superior
- **Visual Studio 2022** / VS Code
- **Git**

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

---

## 💡 Exemplos de Uso

### Uso Básico

```csharp
var builder = new BinaryTreeBuilder();
var printer = new ConsolePrinter();

int[] values = { 3, 2, 1, 6, 0, 5 };
var tree = builder.Build(values);

printer.Print(tree, "Cenário 1");
```

**Saída esperada:**
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

---

## 📚 Documentação

| Documento | Descrição |
|-----------|-----------|
| [ARCHITECTURE.md](./docs/ARCHITECTURE.md) | Arquitetura e padrões de design |
| [API.md](./docs/API.md) | Referência completa de API |
| [GUIDE.md](./docs/GUIDE.md) | Guia detalhado de uso |
| [TESTS.md](./docs/TESTS.md) | Estratégia de testes |
| [SOLID.md](./docs/SOLID.md) | Princípios SOLID aplicados |

---

## 📂 Estrutura do Projeto

```
ProfessionalTree/
├── Models/
│   └── TreeNode.cs              # Modelo de nó da árvore
├── Interfaces/
│   ├── ITreeBuilder.cs          # Contrato para construção
│   └── ITreePrinter.cs          # Contrato para impressão
├── Services/
│   ├── BinaryTreeBuilder.cs     # Implementação builder
│   └── ConsolePrinter.cs        # Implementação printer
├── TreePrinter.cs               # Utilitário de impressão
├── Program.cs                   # Ponto de entrada
├── ProfessionalTree.csproj      # Configuração do projeto
├── .gitignore                   # Configuração Git
└── README.md                    # Este arquivo

ProfessionalTree.Tests/
├── BinaryTreeBuilderTests.cs    # Testes unitários
└── ProfessionalTree.Tests.csproj

docs/
└── (documentação adicional)
```

---

## 🧪 Testes

### Executar Testes

```bash
# Executar todos os testes
dotnet test

# Executar teste específico
dotnet test --filter "Build_WithEmptyArray_ReturnsNull"

# Com saída detalhada
dotnet test --logger "console;verbosity=detailed"

# Com cobertura de código
dotnet test /p:CollectCoverage=true
```

### Cobertura de Testes

Todos os cenários principais são cobertos:
- ✅ Construção com array vazio
- ✅ Construção com um elemento
- ✅ Construção com múltiplos elementos
- ✅ Validação de estrutura

---

## 🏗️ Arquitetura

### Diagrama de Componentes

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

#### **S** - Single Responsibility Principle
Cada classe tem uma responsabilidade única:
- [`BinaryTreeBuilder`](ProfessionalTree/BinaryTreeBuilder.cs): apenas constrói árvores
- [`ConsolePrinter`](ProfessionalTree/ConsolePrinter.cs): apenas imprime árvores
- [`TreeNode`](ProfessionalTree/TreeNode.cs): apenas representa um nó

#### **O** - Open/Closed Principle
Classes abertas para extensão, fechadas para modificação:
- Novas implementações de [`ITreeBuilder`](ProfessionalTree/ITreeBuilder.cs) podem ser criadas
- Novas implementações de [`ITreePrinter`](ProfissionalTree/ITreePrinter.cs) podem ser criadas

#### **L** - Liskov Substitution Principle
Subtipos são substituíveis pelos tipos base:
- Qualquer `ITreeBuilder` pode substituir outro
- Qualquer `ITreePrinter` pode substituir outro

#### **I** - Interface Segregation Principle
Interfaces específicas e bem definidas:
- [`ITreeBuilder`](ProfessionalTree/ITreeBuilder.cs): apenas `Build()`
- [`ITreePrinter`](ProfessionalTree/ITreePrinter.cs): apenas `Print()`

#### **D** - Dependency Inversion Principle
Depender de abstrações, não de implementações:
- [`Program.cs`](ProfessionalTree/Program.cs) usa `ServiceCollection` para DI
- Injeção de dependências via constructor

---

## 🎯 Design Patterns

### Strategy Pattern
As interfaces `ITreeBuilder` e `ITreePrinter` implementam o padrão Strategy, permitindo diferentes estratégias de construção e impressão.

```csharp
// Permitir trocar implementações em runtime
ITreeBuilder builder = new BinaryTreeBuilder();
ITreePrinter printer = new ConsolePrinter();
```

### Composite Pattern
A estrutura [`TreeNode`](ProfessionalTree/TreeNode.cs) implementa o padrão Composite:

```csharp
public class TreeNode
{
    public int Value { get; set; }
    public TreeNode? Left { get; set; }
    public TreeNode? Right { get; set; }
}
```

### Dependency Injection Pattern
O projeto utiliza `Microsoft.Extensions.DependencyInjection` para injetar dependências:

```csharp
services.AddScoped<ITreeBuilder, BinaryTreeBuilder>();
services.AddScoped<ITreePrinter, ConsolePrinter>();
```

---

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

---

## 🔧 Tecnologias

| Tecnologia | Versão | Uso |
|-----------|--------|-----|
| .NET | 10.0 | Framework |
| C# | 12.0 | Linguagem |
| xUnit | 2.9.3 | Testes |
| Microsoft.Extensions.DependencyInjection | 10.0.0 | Injeção de Dependência |

---

## 📦 Dependências

### ProfessionalTree
```xml
<PackageReference Include="Microsoft.Extensions.DependencyInjection" 
                  Version="10.0.0" />
```

### ProfessionalTree.Tests
```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
<PackageReference Include="xunit" Version="2.9.3" />
<PackageReference Include="xunit.runner.visualstudio" Version="3.1.4" />
<PackageReference Include="coverlet.collector" Version="6.0.4" />
```

---

## 🤝 Contribuindo

1. **Fork** o repositório
2. Crie uma **branch** para sua feature (`git checkout -b feature/minha-feature`)
3. **Commit** suas mudanças (`git commit -m 'Adiciona minha feature'`)
4. **Push** para a branch (`git push origin feature/minha-feature`)
5. Abra um **Pull Request**

### Diretrizes

- Siga os padrões SOLID
- Adicione testes para novas funcionalidades
- Mantenha a documentação atualizada
- Use commits descritivos

---

## 📝 Licença

Este projeto está sob a licença **MIT**. Veja [LICENSE](LICENSE) para detalhes.

```
MIT License

Copyright (c) 2026 ProfessionalTree

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software...
```

---

## 👤 Autor

**Seu Nome**
- GitHub: [@seu-usuario](https://github.com/seu-usuario)
- Email: seu.email@example.com

---

## 📚 Referências

- [SOLID Principles - Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles)
- [Design Patterns - Refactoring Guru](https://refactoring.guru/design-patterns/csharp)
- [C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [xUnit.net](https://xunit.net/)
- [Dependency Injection - Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)

---

## 🙌 Agradecimentos

Agradecimentos especiais a:
- Microsoft por .NET e C#
- xUnit pela excelente framework de testes
- Comunidade open source

---

<div align="center">

**Última atualização**: Janeiro 2026 | **Versão**: 1.0.0

[⬆ Voltar ao topo](#-professionaltree)

</div>