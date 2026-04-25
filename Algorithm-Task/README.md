# Algorith and datastructures task

[Back](https://github.com/zlang-wq/Interview-Questions/blob/main/README.md)

## Lowest common ancestor
### Task

This is an expanded version of the LCA task where we use a tree instead of a binary tree and we can also search for the ancestors of multiple nodes.

```
          1
        / | \
       4  3  6
      /|\     \
     8 5 7     9
```

Our tree has `n` nodes where each node can have a value and `k` child nodes (k can also be 0). It is similar to a graph except it does not loops.

LCA(5,8) = 4, think of it like both yours and your sister's lowest common ancestor is your mother
LCA(5,4,8) = 4, you, your sister and your mother's lowest common ancestor is your mother, since she also counts as an ancestor herself
LCA(5,8,9) = 1, because you, your sister and your cousin's lowest common ancestor is your grandmother

### Implementation
For a simple implementation, we should first find the path for each target.

```c#
List<int>? FindPath(Node? node, int target, IList<int> path)
```
We pass the starting node to the method (at the start it will be the root of the tree), the target we are searching and a list of node we have already passed (it will be empty at the start). The return value is either the path of nodes or `null`. 
```c#
if (node is null) return null;

path.Add(node.Value);
```
First we check if our node is `null`, if not then we add our current node's value to our path.
Next there are 3 possible situations:
**1.** Our current node is the target
```c#
if (node.Value == target) return new List<int>(path);
```
This is the best possible scenario, where we just need to return the path we have.

The second one is when our node isn't the target:
```c#
for (var index = 0; index < node.Children.Count; index++)
        {
            var child = node.Children[index];
            var result = FindPath(child, target, path);
            if (result is not null)
                return result;
        }
```
Here we search the children of our node and the children's children recursively until we have found our target node. We also pass our current path.
Lastly when the target is not in any of the child nodes:
```c#
path.RemoveAt(path.Count - 1);
return null;
```
Here we simply remove our last visited node in our path and return `null` value. Think of it when you cleared all the rooms so you go back to the hallway to check the next set of rooms as well.
For example we are searching for 5, we first start from the root (1).
```
path = [1]
```
We go to the first child (4)
```
path = [1,4]
```
Still not 5, so we go forward down to the next child (8).
```
path = [1,4,8]
```
We have reached the end, 8 has no children and it's not 5 either so we go back one (4) and then try the next child (5).
```
path = [1,4]

path = [1,4,5]
```
Found it. Now we return the path to 5 which is 1,4,5.
Now for the main method:
```c#
public static int? LowestCommonAncestor(Node root, int[] values)
{
    var paths = new List<List<int>>();
    foreach (var v in values)
    {
        var p = FindPath(root, v, new List<int>());
        if (p is null)
           return null;
        paths.Add(p);
    }
```
The method will get the root of the tree and our target values. We create a list of paths and put the path of each of our targets in it.
```c#
int? ancestor = null;
int minLen = paths.Min(p => p.Count);

for (int i = 0; i < minLen; i++)
{
   if (paths.All(p => p[i] == paths[0][i]))
      ancestor = paths[0][i];
   else
      break;
}
return ancestor;
```
Next we create a variable for our common ancestor and find the length of the shortest path in the list (there is no point in checking deeper than the shortest path since we are searching for the common ancestor). Then we iterate and compare the values from each path and if all have the same value we update the ancestor to that value. If one path has a different value than the others then we stop iterating and return the ancestor variable's value.
For example we have 5,8
```
5: [1, 4, 5]
    ↕ 
8: [1, 4, 8]
ancestor: 1

5: [1, 4, 5]
       ↕     
8: [1, 4, 8]
ancestor: 4

5: [1, 4, 5]
          ↕     
8: [1, 4, 8]
ancestor: still 4, since 5 != 8
```
Let's try 4,8
```
8: [1, 4, 8]
    ↕ 
4: [1, 4]
ancestor: 1

8: [1, 4, 8]
       ↕ 
4: [1, 4]
ancestor: 4, we won't search further since we have reached the shortest path's end

```
Now let's try 8,3
```
8: [1, 4, 8]
    ↕
3: [1, 3]
ancestor: 1

8: [1, 4, 8]
       ↕
3: [1, 3]
ancestor: still 1 since 4 != 3

```