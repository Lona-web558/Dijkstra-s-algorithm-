using System;
using System.Collections.Generic;

class Graph
{
    private int V;
    private List<List<Tuple<int, int>>> adj;

    public Graph(int v)
    {
        V = v;
        adj = new List<List<Tuple<int, int>>>(V);
        for (int i = 0; i < V; i++)
            adj.Add(new List<Tuple<int, int>>());
    }

    public void AddEdge(int u, int v, int w)
    {
        adj[u].Add(new Tuple<int, int>(v, w));
        adj[v].Add(new Tuple<int, int>(u, w));
    }

    public void Dijkstra(int src)
    {
        int[] dist = new int[V];
        bool[] sptSet = new bool[V];

        for (int i = 0; i < V; i++)
        {
            dist[i] = int.MaxValue;
            sptSet[i] = false;
        }

        dist[src] = 0;

        for (int count = 0; count < V - 1; count++)
        {
            int u = MinDistance(dist, sptSet);
            sptSet[u] = true;

            foreach (var v in adj[u])
            {
                if (!sptSet[v.Item1] && dist[u] != int.MaxValue &&
                    dist[u] + v.Item2 < dist[v.Item1])
                {
                    dist[v.Item1] = dist[u] + v.Item2;
                }
            }
        }

        PrintSolution(dist);
    }

    private int MinDistance(int[] dist, bool[] sptSet)
    {
        int min = int.MaxValue, minIndex = -1;

        for (int v = 0; v < V; v++)
        {
            if (sptSet[v] == false && dist[v] <= min)
            {
                min = dist[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    private void PrintSolution(int[] dist)
    {
        Console.WriteLine("Vertex \t\t Distance from Source");
        for (int i = 0; i < V; i++)
            Console.WriteLine($"{i} \t\t {dist[i]}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Graph g = new Graph(4);
        g.AddEdge(0, 1, 1);
        g.AddEdge(0, 2, 4);
        g.AddEdge(1, 2, 2);
        g.AddEdge(1, 3, 5);
        g.AddEdge(2, 3, 1);

        Console.WriteLine("Dijkstra's algorithm results (starting from vertex 0):");
        g.Dijkstra(0);
    }
}