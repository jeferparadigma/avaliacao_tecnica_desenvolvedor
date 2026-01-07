using System;
using System.Linq;

namespace ProfissionalTree;

public class BinaryTreeBuilder : ITreeBuilder
{
    public Node Build(int[] values)
    {
        if (values == null || values.Length == 0)
            throw new ArgumentException("Array não pode ser vazio.");

        int maxValue = values.Max();
        int rootIndex = Array.IndexOf(values, maxValue);

        var root = new Node(maxValue);

        var leftBranch = values.Take(rootIndex).OrderByDescending(x => x).ToArray();
        var rightBranch = values.Skip(rootIndex + 1).OrderByDescending(x => x).ToArray();

        root.Left = BuildBranch(leftBranch);
        root.Right = BuildBranch(rightBranch);

        return root;
    }

    private Node? BuildBranch(int[] branchValues)
    {
        if (!branchValues.Any()) return null;

        var branchRoot = new Node(branchValues.First());
        var current = branchRoot;

        foreach (var value in branchValues.Skip(1))
        {
            current.Right = new Node(value);
            current = current.Right;
        }

        return branchRoot;
    }
}
