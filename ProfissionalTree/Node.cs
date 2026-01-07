namespace ProfissionalTree;

public class Node
{
    public int Value { get; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }

    public Node(int value) => Value = value;
}
