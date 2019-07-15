using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private static Level _controller;
    public static Level Controller { get {return _controller; } }

    internal Node[,,] nodesArray = new Node[10, 10, 10];
    private Node playerNode;
    public GameObject playerObject;
    public GameObject cubeObj;

    //TODO: Click on a block and player moves there.
    //TODO: Static Laser

    private void Awake()
    {
        //SINGLETON
        if(_controller != null && _controller != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _controller = this;
        }
    }
    void Start()
    {
        GenerateNodeMap();
        FillNodeMap();
        InstantiateNodeMap();
    }

    //CREATES AN EMPTY NODEMAP
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
    //FILLS THE NODEMAP WITH OBJECTS( THIS IS WHERE THE LEVEL GENERATION LOGIC IS)
    private void FillNodeMap()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int z = 0; z < 10; z++)
                {
                    /*if (y == 2 && x > 3 && x < 5)
                    {
                        nodesArray[x, y, z].AddObject(cubeObj.GetComponent<INodeHolder>());
                    }*/
                    if (y == 2 && x == 3 && z == 6)
                    {

                        nodesArray[x, y, z].AddObject(playerObject.GetComponent<INodeObject>());
                        playerNode = nodesArray[x, y, z];
                        //TEMPORARY SOLUTION TO REFERANCE THE PLAYER TO THE GRAVITYVIEW
                        FindObjectOfType<GravityView>().playerPosition = playerNode.GetNodePosition();
                    }
                    if (y == 1 && x == 3 && z == 6)
                    {
                        nodesArray[x, y, z].AddObject(cubeObj.GetComponent<INodeObject>());
                    }
                    if (y == 2 && x == 2 && z == 6)
                    {
                        nodesArray[x, y, z].AddObject(cubeObj.GetComponent<INodeObject>());
                    }
                    /*if (y == 1 && x <= 3 && z > 4)
                    {
                        nodesArray[x, y, z].AddObject(cubeObj.GetComponent<INodeHolder>());
                    }*/
                }
            }
        }
    }

    //GOES THROUGH THE NODEMAP AND INSTANTIATES APPROPRIATE GAMEOBJECTS
    private void InstantiateNodeMap()
    {
        foreach (Node node in nodesArray)
        {
            if(node.nodeObject != null)
            {
                GameObject nodeGameObject = Instantiate(node.nodeObject.GetGameObject(), node.GetNodePosition(), Quaternion.identity);
                nodeGameObject.transform.parent = this.transform;
            }
        }

    }


    void Update()
    {

    }
}
