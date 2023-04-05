using System;
using System.Collections.Generic;

public class Graph
{
    private int numVertices;
    private bool[,] adjMatrix;

    public Graph(int numVertices)
    {
        this.numVertices = numVertices;
        int maxNumEdges = (numVertices * (numVertices - 1)) / 2;
        this.adjMatrix = new bool[numVertices, maxNumEdges];
    }

    public void AddEdge(int u, int v)
    {
        if (u < 0 || u >= numVertices || v < 0 || v >= numVertices)
        {
            Console.WriteLine("Error: invalid vertex indices (" + u + ", " + v + ")");
            return;
        }

        int edgeIndex = GetEdgeIndex(u, v);
        if (edgeIndex == -1)
        {
            Console.WriteLine("Error: could not add edge (" + u + ", " + v + ")");
            return;
        }

        if (adjMatrix[u, edgeIndex] == false && adjMatrix[v, edgeIndex] == false)
        {
            Console.WriteLine("Adding edge: " + u + " " + v);
            adjMatrix[u, edgeIndex] = true;
            adjMatrix[v, edgeIndex] = true;
        }
        else
        {
            Console.WriteLine("Edge already exists: " + u + " " + v);
        }
    }

    public bool HasEdge(int u, int v)
    {
        int edgeIndex = GetEdgeIndex(u, v);
        if (edgeIndex == -1)
        {
            Console.WriteLine("Error: could not find edge (" + u + ", " + v + ")");
            return false;
        }

        try
        {
            return adjMatrix[u, edgeIndex];
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Error: invalid vertex indices (" + u + ", " + v + ")");
            return false;
        }
    }

    private int GetEdgeIndex(int u, int v)
    {
        if (u > v)
        {
            int temp = u;
            u = v;
            v = temp;
        }

        int edgeIndex = ((2 * numVertices - u - 1) * u) / 2 + (v - u - 1);
        if (edgeIndex < 0 || edgeIndex >= adjMatrix.GetLength(1))
        {
            return -1;
        }

        return edgeIndex;
    }
}


public static class RandomGraphGenerator
{
    public static Graph GenerateRandomGraph(int numVertices)
    {
        Random rand = new Random();
        Graph graph = new Graph(numVertices);

        for (int u = 0; u < numVertices; u++)
        {
            // connect vertex u to its 4 neighbors: u+1, u-1, u+k, u-k (where k = numVertices/2)
            int k = numVertices / 2;
            graph.AddEdge(u, (u + 1) % numVertices);
            graph.AddEdge(u, (u - 1 + numVertices) % numVertices);
            graph.AddEdge(u, (u + k) % numVertices);
            graph.AddEdge(u, (u - k + numVertices) % numVertices);
        }

        return graph;
    }
}
