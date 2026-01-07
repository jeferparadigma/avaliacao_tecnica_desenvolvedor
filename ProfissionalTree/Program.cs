using Microsoft.Extensions.DependencyInjection;
using ProfissionalTree.Interfaces;
using ProfissionalTree.Models;
using ProfissionalTree.Services;

var services = new ServiceCollection();

// Injeção de Dependência (SOLID D)
services.AddScoped<ITreeBuilder, BinaryTreeBuilder>();
services.AddScoped<ITreePrinter, ConsolePrinter>();

var provider = services.BuildServiceProvider();

var builder = provider.GetRequiredService<ITreeBuilder>();
var printer = provider.GetRequiredService<ITreePrinter>();

int[][] scenarios = new[]
{
    new int[] { 3, 2, 1, 6, 0, 5 },
    new int[] { 7, 5, 13, 9, 1, 6, 4 }
};

for (int i = 0; i < scenarios.Length; i++)
{
    var arr = scenarios[i];
    var tree = builder.Build(arr);

    Console.WriteLine($"Cenario {i + 1}");
    Console.WriteLine($"Array de entrada: [{string.Join(", ", arr)}]");

    if (tree == null)
    {
        Console.WriteLine("Árvore vazia\n");
        continue;
    }

    Console.WriteLine($"Raiz: {tree.Value}");
    var left = CollectBranch(tree, goLeft: true);
    var right = CollectBranch(tree, goLeft: false);

    Console.WriteLine($"Galhos da esquerda: {(left.Length > 0 ? string.Join(", ", left) : "")}");
    Console.WriteLine($"Galhos da direita: {(right.Length > 0 ? string.Join(", ", right) : "")}");
    Console.WriteLine();
}

static int[] CollectBranch(TreeNode root, bool goLeft)
{
    var list = new System.Collections.Generic.List<int>();
    var node = goLeft ? root.Left : root.Right;
    while (node != null)
    {
        list.Add(node.Value);
        node = goLeft ? node.Left : node.Right;
    }
    return list.ToArray();
}