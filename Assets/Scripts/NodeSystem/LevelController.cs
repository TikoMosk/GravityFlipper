using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Level level;
    public Level Level { get => level; set => level = value; }
    public LevelSerializer levelSerializer;
    public List<NodeData> nodeDataList = new List<NodeData>();
    public List<NodeData> moveableObjectDataList = new List<NodeData>();
    [System.Serializable]
    public struct NodeData
    {
        public int id;
        public GameObject nodePrefab;
    }
    public int levelWidth;
    public int levelHeight;
    public int levelLength;
    private Node playerNode;
    private void Start()
    {

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            levelSerializer.SaveLevel(level, "level1");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            DestroyLevelGraphics();
            BuildLevel(levelSerializer.LoadLevel("level1"));
            CreateLevelGraphics();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            DestroyLevelGraphics();
            BuildTestLevel();
            CreateLevelGraphics();
        }
    }
    public void TestLevel()
    {
        DestroyLevelGraphics();
        BuildTestLevel();
        CreateLevelGraphics();
    }
    // Fills in the level with empty Nodes
    private void CreateLevel()
    {
        Node[,,] nodeMap = new Node[levelWidth, levelHeight, levelLength];
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                for (int z = 0; z < levelLength; z++)
                {
                    nodeMap[x, y, z] = new Node(level, x, y, z);
                }
            }
        }
        Level = new Level(levelWidth, levelHeight, levelLength,nodeMap);
    }
    // Destroys the level graphics (this is called when a new level is loaded to remove the old level graphics)
    private void DestroyLevelGraphics()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Light")
            {

            }
            else
            {
                Destroy(transform.GetChild(i).gameObject);
            }
                
        }
    }
    // Sets the Nodes to their IDs given the level data

    private void BuildLevel(LevelData levelData)
    {
        levelWidth = levelData.levelWidth;
        levelHeight = levelData.levelHeight;
        levelLength = levelData.levelLength;
        CreateLevel();
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                for (int z = 0; z < levelLength; z++)
                {
                    level.SetNode(x, y, z, levelData.nodeDataMap[x, y, z].blockId);
                    if(levelData.nodeDataMap[x,y,z].moveableId != 0)
                    {
                        level.AddMoveableObject(x, y, z, new MoveableObject(levelData.nodeDataMap[x, y, z].moveableId));
                    }
                }
            }
        }
        
    }

    private void BuildTestLevel()
    {
        CreateLevel();
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                for (int z = 0; z < levelLength; z++)
                {
                    if(x == 0 || y == 0 || z == levelLength - 1)
                    {
                        level.SetNode(x, y, z, 1);
                    }
                    if(x == 3 && y == 1 && z == 4)
                    {
                        level.AddMoveableObject(x, y, z, new MoveableObject(1));
                    }
                }
            }
        }
    }
 

    // Creates the Level Graphics

    private void CreateLevelGraphics()
    {
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                for (int z = 0; z < levelLength; z++)
                {
                    CreateNodeGraphics(x, y, z);
                    CreateMoveableObjectGraphics(x, y, z);
                }
            }
        }
    }

    // Creates the NodeGraphic for the node at x,y,z
    private void CreateNodeGraphics(int x, int y, int z)
    {
        if (GetPrefabByNodeId(Level.GetNode(x, y, z).Type) != null)
        {
            GameObject node_go = Instantiate(GetPrefabByNodeId(Level.GetNode(x, y, z).Type), Level.GetNode(x, y, z).GetPosition(), transform.rotation);
            Level.GetNode(x, y, z).CreateGraphic(node_go);
            Level.GetNode(x, y, z).NodeGraphic.transform.parent = this.transform;
            Level.GetNode(x, y, z).SubscribeToNodeTypeChanged(() => { OnNodeTypeChanged(level.GetNode(x, y, z),node_go); });

        }
        else
        {
            Level.GetNode(x, y, z).SubscribeToNodeTypeChanged(() => { OnNodeTypeChanged(level.GetNode(x, y, z)); });
        }
        
    }

    // Creates the moveableObject graphic for the node at x,y,z
    private void CreateMoveableObjectGraphics(int x, int y, int z)
    {
        if (Level.GetNode(x, y, z).MoveableObject != null)
        {
            if (GetPrefabByMoveableObjectId(Level.GetNode(x, y, z).MoveableObject.Id))
            {
                MoveableObject moveable = Level.GetNode(x, y, z).MoveableObject;
                GameObject moveable_go = Instantiate(GetPrefabByMoveableObjectId(moveable.Id), Level.GetNode(x,y,z).GetPosition(), transform.rotation);
                moveable.CreateMoveableObjectGraphic(moveable_go);
                moveable.MoveableObjectGraphic.Node = Level.GetNode(x,y,z);
                moveable_go.transform.parent = this.transform;
                moveable.MoveableGameObject = moveable_go;
                moveable.SubscribeToMoveableObjectMoved((node) => { OnObjectMoved(node, moveable_go, x, y, z); });
            }
        }
    }
    private void OnObjectMoved(Node dest, GameObject moveable_go, int x, int y, int z)
    {
        moveable_go.transform.position = dest.GetPosition();
        moveable_go.GetComponent<MoveableObjectGraphic>().Node = dest;
        
    }
    private void OnNodeTypeChanged(Node n, GameObject node_go)
    {
        Debug.Log("CHANGE");
        Destroy(node_go);
        node_go = Instantiate(GetPrefabByNodeId(n.Type), n.GetPosition(), transform.rotation);
        n.CreateGraphic(node_go);
        n.NodeGraphic.transform.parent = this.transform;
    }
    private void OnNodeTypeChanged(Node n)
    {
        GameObject node_go = Instantiate(GetPrefabByNodeId(n.Type), n.GetPosition(), transform.rotation);
        n.CreateGraphic(node_go);
        n.NodeGraphic.transform.parent = this.transform;
    }

    //Gets the block prefab by the given ID from the list

    private GameObject GetPrefabByNodeId(int nodeId)
    {
        if(nodeId >= 0 && nodeId < nodeDataList.Count)
        {
            return nodeDataList[nodeId].nodePrefab;
        }

        Debug.LogError("No Prefab specified for the given node ID");
        return null;
    }

    // Gets the moveableObject prefab by given ID from the list

    private GameObject GetPrefabByMoveableObjectId(int moveableObjectId)
    {
        if (moveableObjectId >= 0 && moveableObjectId < nodeDataList.Count)
        {
            return moveableObjectDataList[moveableObjectId].nodePrefab;
        }

        Debug.LogError("No Prefab specified for the given moveableObject ID");
        return null;
    }
}
