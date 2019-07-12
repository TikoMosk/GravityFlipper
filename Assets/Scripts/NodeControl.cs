using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeControl : MonoBehaviour
{
    internal List<Node> nodeArray = new List<Node>(1000);
    public GameObject playerObject;
    public GameObject obj;
    //ToDo //public GameObject staticLaserObject;

    void Start()
    {
        Node n = new Node(playerObject, 1, 1, 1);
        n.InstantiateNode();
        Node node;
        for (int i = 4; i > -4; i--)
        {
            for (int j = -4; j < 4; j++)
            {
                node = new Node(obj, i, 0, j);
                node.InstantiateNode();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
