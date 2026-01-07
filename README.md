# 💼 Consulta de Colaborador com Maior Salário por Departamento

[![SQL Server](https://img.shields.io/badge/SQL%20Server-2019%2B-CC2927.svg)](https://www.microsoft.com/en-us/sql-server/)
[![T-SQL](https://img.shields.io/badge/T--SQL-Modern-0078D4.svg)](https://docs.microsoft.com/en-us/sql/t-sql/language-reference)
[![Performance](https://img.shields.io/badge/Performance-Optimized-green.svg)](#-otimizações)

> Script T-SQL otimizado que identifica o colaborador com **maior salário em cada departamento** usando **Window Functions** e **CTEs**.

---

## 📋 Visão Geral

Este script SQL resolve um cenário comum em análise de dados: **encontrar o colaborador mais bem remunerado de cada departamento**.

### Características Principais

- 🎯 **Window Functions** (`RANK() OVER PARTITION BY`)
- 🔧 **CTEs** (Common Table Expressions) para melhor legibilidade
- ⚡ **Otimizações de performance** (`WITH (NOLOCK)`)
- 📊 **Estrutura escalável** e fácil de manter
- 🔐 **Segurança** com joins explícitos

---

## 🗂️ Estrutura do Banco de Dados

### Tabelas Envolvidas

#### `dbo.Pessoa`
```sql
CREATE TABLE dbo.Pessoa (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Salario DECIMAL(10,2) NOT NULL,
    DeptId INT NOT NULL,
    FOREIGN KEY (DeptId) REFERENCES dbo.Departamento(Id)
);
```

| Coluna | Tipo | Descrição |
|--------|------|-----------|
| `Id` | INT | Identificador único do colaborador |
| `Nome` | NVARCHAR(100) | Nome do colaborador |
| `Salario` | DECIMAL(10,2) | Salário do colaborador |
| `DeptId` | INT | ID do departamento (FK) |

#### `dbo.Departamento`
```sql
CREATE TABLE dbo.Departamento (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL
);
```

| Coluna | Tipo | Descrição |
|--------|------|-----------|
| `Id` | INT | Identificador único do departamento |
| `Nome` | NVARCHAR(100) | Nome do departamento |

---

## 🔍 Análise Detalhada

### Como Funciona

O script é dividido em **duas lógicas principais**:

#### 1️⃣ **Lógica de Preparação (CTE)**
```sql
WITH ColaboradoresRanqueados AS (
    SELECT 
        d.Nome AS Departamento,
        p.Nome AS Pessoa,
        p.Salario,
        RANK() OVER (
            PARTITION BY p.DeptId 
            ORDER BY p.Salario DESC
        ) AS TopSalario
    FROM dbo.Pessoa AS p WITH (NOLOCK)
    INNER JOIN dbo.Departamento AS d ON p.DeptId = d.Id
)
```

**O que acontece aqui:**

- 📌 **CTE (Common Table Expression)**: Define um conjunto de dados virtual chamado `ColaboradoresRanqueados`
- 🔗 **JOIN**: Combina `Pessoa` com `Departamento` para obter nomes legíveis
- 🎯 **RANK() OVER**: Cria um ranking numérico para cada colaborador dentro de seu departamento
  - `PARTITION BY p.DeptId`: Agrupa por departamento
  - `ORDER BY p.Salario DESC`: Ordena por salário em ordem decrescente
  - Resultado: colaborador com maior salário recebe `TopSalario = 1`

#### 2️⃣ **Lógica de Negócio (SELECT)**
```sql
SELECT 
    Departamento, 
    Pessoa, 
    Salario
FROM 
    ColaboradoresRanqueados
WHERE 
    TopSalario = 1;
```

**O que acontece aqui:**

- 🔎 **WHERE TopSalario = 1**: Filtra apenas o colaborador com maior salário de cada departamento
- 📤 **Retorna**: Departamento, nome e salário do colaborador mais bem remunerado

---

## 📊 Exemplos de Resultado

### Dados de Exemplo

**Tabela: dbo.Departamento**
| Id | Nome |
|---|---|
| 1 | Tecnologia |
| 2 | Vendas |
| 3 | RH |

**Tabela: dbo.Pessoa**
| Id | Nome | Salario | DeptId |
|---|---|---|---|
| 1 | João Silva | 8500.00 | 1 |
| 2 | Maria Santos | 9200.00 | 1 |
| 3 | Pedro Costa | 7800.00 | 1 |
| 4 | Ana Oliveira | 6500.00 | 2 |
| 5 | Carlos Souza | 7200.00 | 2 |
| 6 | Lucia Martins | 5500.00 | 3 |
| 7 | Roberto Alves | 6000.00 | 3 |

### Resultado da Consulta

```
Departamento | Pessoa           | Salario
-------------|------------------|----------
Tecnologia   | Maria Santos     | 9200.00
Vendas       | Carlos Souza     | 7200.00
RH           | Roberto Alves    | 6000.00
```

---

## ⚡ Otimizações de Performance

### 1️⃣ Dica de Leitura: `WITH (NOLOCK)`

```sql
FROM dbo.Pessoa AS p WITH (NOLOCK)
```

**O que faz:**
- 📖 Permite **leitura suja** (dirty reads)
- ⚡ **Evita locks** de leitura
- ✅ **Ideal** para relatórios e consultas analíticas
- ❌ **NÃO usar** em dados financeiros críticos

**Quando usar:**
```
✅ Relatórios, Dashboard, Analytics
❌ Transações críticas, dados financeiros
```

### 2️⃣ Window Functions vs. Subqueries

#### ❌ Abordagem Tradicional (Subquery)
```sql
SELECT 
    d.Nome AS Departamento,
    p.Nome AS Pessoa,
    p.Salario
FROM dbo.Pessoa p
INNER JOIN dbo.Departamento d ON p.DeptId = d.Id
WHERE p.Salario = (
    SELECT MAX(p2.Salario)
    FROM dbo.Pessoa p2
    WHERE p2.DeptId = p.DeptId
);
```
**Problema:** A subquery executa para **cada linha** (N+1 problem)

#### ✅ Abordagem Moderna (Window Function)
```sql
RANK() OVER (PARTITION BY p.DeptId ORDER BY p.Salario DESC)
```
**Vantagem:** Executa **uma única vez** em memória

### 3️⃣ Índices Recomendados

```sql
-- Índice para particionamento rápido
CREATE INDEX IDX_Pessoa_DeptId_Salario 
ON dbo.Pessoa(DeptId, Salario DESC);

-- Índice na chave estrangeira
CREATE INDEX IDX_Pessoa_DeptId 
ON dbo.Pessoa(DeptId);

-- Se houver muitas consultas por Nome
CREATE INDEX IDX_Pessoa_Nome 
ON dbo.Pessoa(Nome);
```

---

## 🚀 Como Usar

### Execução Básica

```sql
-- Copiar e executar o script
EXEC sp_executesql N'
WITH ColaboradoresRanqueados AS (
    ...
)
SELECT ...
';
```

### Salvando como Stored Procedure

```sql
CREATE PROCEDURE sp_MaiorSalarioPorDepartamento
AS
BEGIN
    SET NOCOUNT ON;
    
    WITH ColaboradoresRanqueados AS (
        SELECT 
            d.Nome AS Departamento,
            p.Nome AS Pessoa,
            p.Salario,
            RANK() OVER (
                PARTITION BY p.DeptId 
                ORDER BY p.Salario DESC
            ) AS TopSalario
        FROM dbo.Pessoa AS p WITH (NOLOCK)
        INNER JOIN dbo.Departamento AS d ON p.DeptId = d.Id
    )
    SELECT 
        Departamento, 
        Pessoa, 
        Salario
    FROM 
        ColaboradoresRanqueados
    WHERE 
        TopSalario = 1;
END;

-- Executar
EXEC sp_MaiorSalarioPorDepartamento;
```

### Usando em Aplicação C#

```csharp
using (SqlConnection connection = new SqlConnection(connectionString))
{
    using (SqlCommand command = new SqlCommand(
        "sp_MaiorSalarioPorDepartamento", 
        connection))
    {
        command.CommandType = CommandType.StoredProcedure;
        
        connection.Open();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                string departamento = reader["Departamento"].ToString();
                string pessoa = reader["Pessoa"].ToString();
                decimal salario = (decimal)reader["Salario"];
                
                Console.WriteLine($"{departamento} - {pessoa}: R$ {salario}");
            }
        }
    }
}
```

---

## 🎓 Conceitos SQL Utilizados

### Window Functions

| Função | Uso | Exemplo |
|--------|-----|---------|
| `RANK()` | Ranking com gaps | `RANK() OVER (ORDER BY Salario DESC)` |
| `ROW_NUMBER()` | Numeração sequencial | `ROW_NUMBER() OVER (ORDER BY Salario DESC)` |
| `DENSE_RANK()` | Ranking sem gaps | `DENSE_RANK() OVER (ORDER BY Salario DESC)` |
| `ROW_NUMBER()` | Primeira linha | Ideal para `WHERE rn = 1` |

**Comparação:**

```
Salário | RANK() | ROW_NUMBER() | DENSE_RANK()
--------|--------|--------------|----------
9200    |   1    |      1       |     1
8500    |   2    |      2       |     2
8500    |   2    |      3       |     2  ← Diferença aqui
7800    |   4    |      4       |     3
```

**Neste caso:** `RANK()` é a escolha correta para identificar "o colaborador com maior salário"

### CTEs (Common Table Expressions)

**Vantagens:**
- 📖 **Legibilidade**: Código mais limpo e organizado
- ♻️ **Reutilização**: CTE pode ser referenciada múltiplas vezes
- 🔧 **Manutenção**: Mais fácil de debugar

**Sintaxe:**
```sql
WITH NomeCTE AS (
    -- Sua consulta aqui
    SELECT ...
)
-- Usar CTE
SELECT * FROM NomeCTE;
```

---

## 🔐 Alternativas e Variações

### Versão com Múltiplos Rankings

```sql
WITH ColaboradoresRanqueados AS (
    SELECT 
        d.Nome AS Departamento,
        p.Nome AS Pessoa,
        p.Salario,
        RANK() OVER (PARTITION BY p.DeptId ORDER BY p.Salario DESC) AS TopSalario,
        COUNT(*) OVER (PARTITION BY p.DeptId) AS TotalPessoasDept
    FROM dbo.Pessoa AS p WITH (NOLOCK)
    INNER JOIN dbo.Departamento AS d ON p.DeptId = d.Id
)
SELECT *
FROM ColaboradoresRanqueados
WHERE TopSalario <= 3;  -- Top 3 de cada departamento
```

### Versão com Diferença para Média

```sql
WITH ColaboradoresRanqueados AS (
    SELECT 
        d.Nome AS Departamento,
        p.Nome AS Pessoa,
        p.Salario,
        AVG(p.Salario) OVER (PARTITION BY p.DeptId) AS MediaDept,
        RANK() OVER (PARTITION BY p.DeptId ORDER BY p.Salario DESC) AS TopSalario
    FROM dbo.Pessoa AS p WITH (NOLOCK)
    INNER JOIN dbo.Departamento AS d ON p.DeptId = d.Id
)
SELECT 
    Departamento,
    Pessoa,
    Salario,
    MediaDept,
    (Salario - MediaDept) AS DiferencaDaMedia
FROM ColaboradoresRanqueados
WHERE TopSalario = 1;
```

### Versão com Histórico (Temporal)

```sql
WITH ColaboradoresRanqueados AS (
    SELECT 
        d.Nome AS Departamento,
        p.Nome AS Pessoa,
        p.Salario,
        MONTH(GETDATE()) AS MesAtual,
        RANK() OVER (
            PARTITION BY p.DeptId, MONTH(GETDATE())
            ORDER BY p.Salario DESC
        ) AS TopSalario
    FROM dbo.Pessoa AS p WITH (NOLOCK)
    INNER JOIN dbo.Departamento AS d ON p.DeptId = d.Id
)
SELECT *
FROM ColaboradoresRanqueados
WHERE TopSalario = 1;
```

---

## 🐛 Tratamento de Erros

### Possíveis Problemas

#### 1. Departamento sem Colaboradores

```sql
-- Solução: LEFT JOIN em vez de INNER JOIN
SELECT 
    d.Nome AS Departamento,
    ISNULL(p.Nome, 'N/A') AS Pessoa,
    ISNULL(p.Salario, 0) AS Salario
FROM dbo.Departamento d
LEFT JOIN dbo.Pessoa p ON d.Id = p.DeptId
WHERE p.DeptId IS NOT NULL OR d.Id NOT IN (SELECT DISTINCT DeptId FROM dbo.Pessoa);
```

#### 2. Empates de Salário

```sql
-- Problema: RANK() retorna múltiplas linhas em caso de empate
-- Solução: Usar ROW_NUMBER() com quebra de empate
RANK() OVER (
    PARTITION BY p.DeptId 
    ORDER BY p.Salario DESC, p.Id ASC  -- Usa ID como critério de desempate
)
```

#### 3. Valores NULL em Salário

```sql
-- Verificar valores NULL
SELECT *
FROM dbo.Pessoa
WHERE Salario IS NULL;

-- Tratar na consulta
ISNULL(p.Salario, 0) AS Salario
```

---

## 📊 Análise de Performance

### Plano de Execução Esperado

```
Table Scan or Seek on dbo.Pessoa
    └── Nested Loops (Inner Join)
            └── Table Seek on dbo.Departamento
    └── Window Aggregate (RANK)
            └── Sort (PARTITION BY DeptId, ORDER BY Salario DESC)
Filter (TopSalario = 1)
```

### Dicas de Otimização

| Problema | Solução |
|----------|---------|
| Scan lento | Criar índice em `(DeptId, Salario DESC)` |
| JOIN lento | Verificar índices em chaves estrangeiras |
| Memory pressure | Usar `SET STATISTICS IO ON` para monitorar |

---

## 📚 Referências

- [Microsoft Docs - Window Functions](https://docs.microsoft.com/en-us/sql/t-sql/queries/select-window-function-transact-sql)
- [Microsoft Docs - Common Table Expressions (CTEs)](https://docs.microsoft.com/en-us/sql/t-sql/queries/with-common-table-expression-transact-sql)
- [RANK vs ROW_NUMBER vs DENSE_RANK](https://www.sqlshack.com/en/understanding-the-difference-between-rank-row_number-and-dense_rank-functions-in-sql-server/)
- [Query Hints - WITH (NOLOCK)](https://docs.microsoft.com/en-us/sql/t-sql/queries/hints-transact-sql-table)

---

## 🤝 Manutenção

### Checklist de Implementação

- [ ] Validar estrutura das tabelas
- [ ] Criar índices recomendados
- [ ] Converter para Stored Procedure (opcional)
- [ ] Testar com dados reais
- [ ] Monitora performance com `SET STATISTICS TIME/IO ON`
- [ ] Documentar em wiki/confluence
- [ ] Adicionar a testes de regressão

### Monitoramento

```sql
-- Ativar estatísticas
SET STATISTICS TIME ON;
SET STATISTICS IO ON;

-- Executar consulta
EXEC sp_MaiorSalarioPorDepartamento;

-- Desativar
SET STATISTICS TIME OFF;
SET STATISTICS IO OFF;
```

---

## 📝 Histórico de Versões

| Versão | Data | Alterações |
|--------|------|-----------|
| 1.0 | Jan 2026 | Versão inicial com Window Functions e CTEs |
| 1.1 | - | Adicionadas alternativas e otimizações (em planejamento) |

---

<div align="center">

**Script SQL Otimizado** | **T-SQL Moderno** | **Performance First**

[⬆ Voltar ao topo](#-consulta-de-colaborador-com-maior-salário-por-departamento)

</div>