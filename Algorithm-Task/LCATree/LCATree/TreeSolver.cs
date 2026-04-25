namespace LCATree;

public class TreeSolver
{
    public static Node BuildTree()
    {
        var nodes = new Dictionary<int, Node>();
        Node Get(int v) => nodes.TryGetValue(v, out var n) ? n : (nodes[v] = new Node(v));

        Get(1).Children = new List<Node> { Get(4), Get(3), Get(6) };
        Get(4).Children = new List<Node> { Get(8), Get(5), Get(7) };
        Get(6).Children = new List<Node> { Get(9) };

        return Get(1);
    }
    
    public static int? LowestCommonAncestor(Node root, IEnumerable<int> values)
    {
        var paths = new List<List<int>>();

        foreach (var v in values)
        {
            var p = FindPath(root, v, new List<int>());
            if (p is null)
                return null;
            paths.Add(p);
        }

        int? ancestor = null;
        var minLen = paths.Min(p => p.Count);

        for (var i = 0; i < minLen; i++)
        {
            if (paths.All(p => p[i] == paths[0][i]))
                ancestor = paths[0][i];
            else
                break;
        }

        return ancestor;
    }
    
    private static List<int>? FindPath(Node? node, int target, List<int> path)
    {
        if (node is null) return null;

        path.Add(node.Value);

        if (node.Value == target)
            return path;

        for (var index = 0; index < node.Children.Count; index++)
        {
            var child = node.Children[index];
            var result = FindPath(child, target, path);
            if (result is not null)
                return result;
        }

        path.RemoveAt(path.Count - 1);
        return null;
    }
}