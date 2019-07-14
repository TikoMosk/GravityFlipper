using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeControl : MonoBehaviour
{
    internal Node[,,] nodesArray = new Node[10, 10, 10];
    public GameObject playerObject;
    public GameObject cubeObj;

    //ToDo
    //public GameObject staticLaserObject;

    void Start()
    {
        GenerateNodeMap();
        InstantiateNodeMap();
    }

    private void GenerateNodeMap()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int z = 0; z < 10; z++)
                {
                    nodesArray[x, y, z] = new Node(x, y, z);
                }
            }
        }
    }

    private void InstantiateNodeMap()
    {
        foreach (Node node in nodesArray)
        {
            node.AddObject(cubeObj.GetComponent<INodeHolder>());
            node.InstantiateNode();
        }
    }

    private void InstantiateGroundMap()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                nodesArray[x, 0, z].AddObject(cubeObj.GetComponent<INodeHolder>());
                nodesArray[x, 0, z].InstantiateNode();
            }
        }
    }

    void Update()
    {

    }
}
