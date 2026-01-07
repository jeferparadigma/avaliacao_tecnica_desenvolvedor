SELECT 
        d.Nome AS Departamento,
        p.Nome AS Pessoa,
        p.Salario,
        RANK() OVER (
            PARTITION BY p.DeptId 
            ORDER BY p.Salario DESC
        ) AS TopSalario