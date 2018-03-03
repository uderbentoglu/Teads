using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine()); // the number of adjacency relations
        Dictionary<int, Node> nodes = new Dictionary<int, Node>();
        for (int i = 0; i < n; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int xi = int.Parse(inputs[0]); // the ID of a person which is adjacent to yi
            int yi = int.Parse(inputs[1]); // the ID of a person which is adjacent to xi
            Console.Error.WriteLine(xi + " " + yi);

            Node n1 = nodes.ContainsKey(xi) ? nodes[xi] : new Node(xi);
            Node n2 = nodes.ContainsKey(yi) ? nodes[yi] : new Node(yi);

            if (!nodes.ContainsKey(xi))
            {
                nodes.Add(xi, n1);
            }

            if (!nodes.ContainsKey(yi))
            {
                nodes.Add(yi, n2);
            }
            n1.Neighbors.Add(n2);
            n2.Neighbors.Add(n1);
        }

        Console.WriteLine(calculateHours(nodes));
    }

    private static int calculateHours(Dictionary<int, Node> graph)
    {
        int minHour = 0;
        while (graph.Count() > 1)
        {
            removeLeaves(graph);
            minHour++;
        }
        return minHour;
    }

    static void removeLeaves(Dictionary<int, Node> graph)
    {
        var leaves = graph.ToList().Where(x => x.Value.IsLeaf()).ToList();
        for (int i = leaves.Count() - 1; i >= 0; i--)
        {
            var leaf = leaves[i];
            graph.Remove(leaf.Key);
            leaf.Value.RemoveFromAdjacentNodes();
        }
    }
}

public class Node
{
    public int ID { get; set; }
    public List<Node> Neighbors { get; set; }

    public Node(int id)
    {
        Neighbors = new List<Node>();
        this.ID = id;
    }

    public bool IsLeaf()
    {
        return Neighbors.Count == 1;
    }

    public void RemoveFromAdjacentNodes()
    {
        Neighbors.ForEach(x => x.Neighbors.Remove(this));
    }
}
