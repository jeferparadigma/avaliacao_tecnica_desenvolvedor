namespace ProfissionalTree.Services;

using ProfissionalTree.Interfaces;
using ProfissionalTree.Models;
using System.Text;

/// <summary>
/// Implementa impressão em console com formatação visual.
/// Princípio: Single Responsibility (SOLID S)
/// </summary>
public class ConsolePrinter : ITreePrinter
{
    private const int NodeSpacing = 4;

    public void Print(TreeNode? root, string title = "")
    {
        if (!string.IsNullOrWhiteSpace(title))
            Console.WriteLine($"\n{'=', 50}\n{title}\n{'=', 50}");

        if (root == null)
        {
            Console.WriteLine("Árvore vazia");
            return;
        }

        var levels = new List<List<TreeNode?>>();
        CollectLevels(root, 0, levels);
        PrintLevels(levels);
    }

    private void CollectLevels(TreeNode? node, int level, List<List<TreeNode?>> levels)
    {
        if (node == null)
            return;

        if (levels.Count <= level)
            levels.Add(new List<TreeNode?>());

        levels[level].Add(node);
        CollectLevels(node.Left, level + 1, levels);
        CollectLevels(node.Right, level + 1, levels);
    }

    private void PrintLevels(List<List<TreeNode?>> levels)
    {
        foreach (var level in levels)
        {
            var line = new StringBuilder();
            foreach (var node in level)
            {
                line.Append(node?.Value.ToString() ?? " ");
                line.Append(new string(' ', NodeSpacing));
            }
            Console.WriteLine(line.ToString());
        }
    }
}