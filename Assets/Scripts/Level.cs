using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private static Level _instance;
    public static Level Instance { get {return _instance; } }

    public int worldWidth;
    public int worldHeight;
    public int worldLength;
    private Node[,,] nodeMap;


    //SINGLETON
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        GenerateNodeMap();
        FillNodeMap();
        InstantiateNodeMap();
    }

    /// <summary>
    /// Creates a map of Nodes
    /// </summary>
    private void GenerateNodeMap()
    {
        nodeMap = new Node[worldWidth, worldHeight, worldLength];
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                for (int z = 0; z < worldLength; z++)
                {
                     nodeMap[x, y, z] = new Node(x, y, z);
                }
            }
        }
    }

    /// <summary>
    /// Fills in the NodeMap with NodeObjects
    /// </summary>
    private void FillNodeMap()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            { 
                for (int z = 0; z < worldLength; z++)
                {
                    if (y == 0 || x == 0 || z == 0)
                    {
                        nodeMap[x, y, z].AddNodeObject("Block");
                    }
                    if(y == 1  && x == 1 && z == 1)
                    {
                        nodeMap[x, y, z].AddNodeObject("Player");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Instantiates the GameObjects for NodeMap
    /// </summary>
    private void InstantiateNodeMap()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                for (int z = 0; z < worldLength; z++)
                {
                    if(nodeMap[x,y,z].GetNodeObject() != null)
                    {
                        GameObject prefab = NodeObjectTypes.Instance.GetPrefabById(nodeMap[x, y, z].GetNodeObject().GetNodeObjectType());
                        GameObject nodeGameObject = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
                        nodeGameObject.transform.parent = this.transform;
                        nodeMap[x, y, z].GetNodeObject().SetGameObject(nodeGameObject);
                    }
                    
                }
            }
        }
    }

    void Update()
    {

    }
}
