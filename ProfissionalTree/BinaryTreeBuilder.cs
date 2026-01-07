namespace ProfissionalTree.Services;

using ProfissionalTree.Interfaces;
using ProfissionalTree.Models;

/// <summary>
/// Implementa a construção de uma árvore binária a partir de um array.
/// Princípio: Single Responsibility (SOLID S)
/// </summary>
public class BinaryTreeBuilder : ITreeBuilder
{
    /// <summary>
    /// Constrói árvore usando representação em array (índice 0 = raiz).
    /// </summary>
    public TreeNode? Build(int[] values)
    {
        if (values == null || values.Length == 0)
            return null;

        return BuildRecursive(values, 0);
    }

    private TreeNode? BuildRecursive(int[] values, int index)
    {
    if (values == null || values.Length == 0)
            return null;

        int max = values.Max();
        int idx = Array.IndexOf(values, max);

        var root = new TreeNode(max);

        var leftVals = values.Take(idx).OrderByDescending(x => x).ToArray();
        var rightVals = values.Skip(idx + 1).OrderByDescending(x => x).ToArray();

        // construir cadeia à esquerda usando Left
        TreeNode? prev = root;
        if (leftVals.Length > 0)
        {
            prev.Left = new TreeNode(leftVals[0]);
            prev = prev.Left;
            for (int i = 1; i < leftVals.Length; i++)
            {
                prev.Left = new TreeNode(leftVals[i]);
                prev = prev.Left;
            }
        }

        // construir cadeia à direita usando Right
        prev = root;
        if (rightVals.Length > 0)
        {
            prev.Right = new TreeNode(rightVals[0]);
            prev = prev.Right;
            for (int i = 1; i < rightVals.Length; i++)
            {
                prev.Right = new TreeNode(rightVals[i]);
                prev = prev.Right;
            }
        }

        return root;    }
}