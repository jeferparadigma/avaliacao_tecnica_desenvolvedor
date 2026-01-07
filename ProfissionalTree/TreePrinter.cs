namespace ProfissionalTree;

public static class TreePrinter
{
    public static void Print(Node? node, string prefix = "", bool isLeft = true)
    {
        if (node == null) return;

        Console.WriteLine(prefix + (isLeft ? "├── " : "└── ") + node.Value);
        Print(node.Left, prefix + (isLeft ? "│   " : "    "), true);
        Print(node.Right, prefix + (isLeft ? "│   " : "    "), false);
    }
}
