using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject nodePrefab; 
    public GameObject linePrefab; 

    private List<GameObject> nodeGameObjects = new List<GameObject>();
    private List<GameObject> lineGameObjects = new List<GameObject>();

    void Start()
    {
        Initialize();
    }

    void Update()
    {

    }

    private void Initialize()
    {
        Node node1 = new Node(3);
        Node node2 = new Node(3);
        Node node3 = new Node(2);
        Node node4 = new Node(10);
        Node node5 = new Node(5);
        Node node6 = new Node(4);

        Edge edge13 = new Edge(ref node1, ref node3);
        Edge edge23 = new Edge(ref node2, ref node3);
        Edge edge26 = new Edge(ref node2, ref node6);
        Edge edge34 = new Edge(ref node3, ref node4);
        Edge edge36 = new Edge(ref node3, ref node6);
        Edge edge45 = new Edge(ref node4, ref node5);
        Edge edge56 = new Edge(ref node5, ref node6);

        node1.AddEdge(ref edge13);
        node3.AddEdge(ref edge13);
        node2.AddEdge(ref edge23);
        node3.AddEdge(ref edge23);
        node2.AddEdge(ref edge26);
        node6.AddEdge(ref edge26);
        node3.AddEdge(ref edge34);
        node4.AddEdge(ref edge34);
        node3.AddEdge(ref edge36);
        node6.AddEdge(ref edge36);
        node4.AddEdge(ref edge45);
        node5.AddEdge(ref edge45);
        node5.AddEdge(ref edge56);
        node6.AddEdge(ref edge56);

        Graph graph = new Graph();
        graph.AddNode(node1);
        graph.AddNode(node2);
        graph.AddNode(node3);
        graph.AddNode(node4);
        graph.AddNode(node5);
        graph.AddNode(node6);

        CreateGraph(graph);
    }

    private void CreateGraph(Graph graph)
    {
        Dictionary<Node, GameObject> nodeObjectMap = new Dictionary<Node, GameObject>();

        foreach (Node node in graph.GetNodes())
        {
            Vector3 position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
            GameObject nodeGO = Instantiate(nodePrefab, position, Quaternion.identity);
            nodeObjectMap[node] = nodeGO;
            nodeGameObjects.Add(nodeGO);
        }

        foreach (Node node in graph.GetNodes())
        {
            foreach (Edge edge in node.GetEdges())
            {
                if (!nodeObjectMap.ContainsKey(edge.GetFromNode()) || !nodeObjectMap.ContainsKey(edge.GetToNode()))
                    continue;

                GameObject lineGO = Instantiate(linePrefab);
                LineRenderer lr = lineGO.GetComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetPosition(0, nodeObjectMap[edge.GetFromNode()].transform.position);
                lr.SetPosition(1, nodeObjectMap[edge.GetToNode()].transform.position);
                lineGameObjects.Add(lineGO);
            }
        }
    }
}
