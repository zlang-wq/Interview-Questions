// See https://aka.ms/new-console-template for more information

using LCATree;

var tree = TreeSolver.BuildTree();

var result = TreeSolver.LowestCommonAncestor(tree, new []{ 7, 3});

Console.WriteLine(result);