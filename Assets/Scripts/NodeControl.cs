using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeControl : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject obj;
    //ToDo //public GameObject staticLaserObject;

    void Start()
    {
        Node node = new Node(obj, 0, 0, 0);
        Debug.Log("start");
        node.InstantiateNode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
