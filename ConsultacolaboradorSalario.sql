WITH ColaboradoresRanqueados AS (
    /* LÓGICA DE PREPARAÇÃO: 
       Aqui  SQL resolve a partição e o ranking em memória.
    */
    SELECT 
        d.Nome AS Departamento,
        p.Nome AS Pessoa,
        p.Salario,
        RANK() OVER (
            PARTITION BY p.DeptId 
            ORDER BY p.Salario DESC
        ) AS TopSalario
    FROM dbo.Pessoa AS p WITH (NOLOCK) -- Dica de performance para leitura
    INNER JOIN dbo.Departamento AS d ON p.DeptId = d.Id
)
/* LÓGICA DE NEGÓCIO: 
   Apenas filtra o resultado desejado.
*/
SELECT 
    Departamento, 
    Pessoa, 
    Salario
FROM 
    ColaboradoresRanqueados
WHERE 
    TopSalario = 1;