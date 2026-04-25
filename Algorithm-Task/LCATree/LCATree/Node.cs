namespace LCATree;

public class Node
{
    public int Value { get; set; }
    
    public List<Node> Children { get; set; } = new();

    public Node(int value)
    {
        Value = value;
    }
}