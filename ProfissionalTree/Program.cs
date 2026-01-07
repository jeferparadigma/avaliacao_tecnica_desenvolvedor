using ProfissionalTree;

class Program
{
    static void Main()
    {
        ITreeBuilder builder = new BinaryTreeBuilder();

        int[] scenario1 = { 3, 2, 1, 6, 0, 5 };
        var tree1 = builder.Build(scenario1);
        Console.WriteLine("Cenário 1:");
        TreePrinter.Print(tree1);

        Console.WriteLine();

        int[] scenario2 = { 7, 5, 13, 9, 1, 6, 4 };
        var tree2 = builder.Build(scenario2);
        Console.WriteLine("Cenário 2:");
        TreePrinter.Print(tree2);
    }
}
