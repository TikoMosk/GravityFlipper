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
    /// <summary>
    /// Fills in the level with empty Nodes
    /// </summary>
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
    /// <summary>
    /// Destroys the level graphics (this is called when a new level is loaded to remove the old level graphics)
    /// </summary>
    private void DestroyLevelGraphics()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    /// <summary>
    /// Sets the Nodes to their IDs given the level data
    /// </summary>
    /// <param name="levelData"></param>
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
                        level.AddMoveableObject(x,y,z,new MoveableObject(1));
                    }
                }
            }
        }
    }
 
    /// <summary>
    /// Creates the Level Graphics
    /// </summary>
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
    /// <summary>
    /// Creates the NodeGraphic for the node at x,y,z
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    private void CreateNodeGraphics(int x, int y, int z)
    {
        if (GetPrefabByNodeId(Level.GetNode(x, y, z).Type) != null)
        {
            GameObject node_go = Instantiate(GetPrefabByNodeId(Level.GetNode(x, y, z).Type), Level.GetNode(x, y, z).GetPosition(), Quaternion.identity);
            Level.GetNode(x, y, z).CreateGraphic(node_go);
            Level.GetNode(x, y, z).NodeGraphic.transform.parent = this.transform;
        }
    }
    /// <summary>
    /// Creates the moveableObject graphic for the node at x,y,z
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    private void CreateMoveableObjectGraphics(int x, int y, int z)
    {
        if (Level.GetNode(x, y, z).MoveableObject != null)
        {
            if (GetPrefabByMoveableObjectId(Level.GetNode(x, y, z).MoveableObject.Id))
            {
                MoveableObject moveable = Level.GetNode(x, y, z).MoveableObject;
                GameObject moveable_go = Instantiate(GetPrefabByMoveableObjectId(moveable.Id), new Vector3(x, y, z), Quaternion.identity);
                moveable_go.transform.parent = this.transform;
                moveable.SubscribeToMoveableObjectMoved((node) => { OnObjectMoved(node, moveable_go, x, y, z); });
            }
        }
    }
    private void OnObjectMoved(Node dest, GameObject moveable_go, int x, int y, int z)
    {
        moveable_go.transform.position = dest.GetPosition();
        
    }
    /// <summary>
    /// Gets the block prefab by the given ID from the list
    /// </summary>
    /// <param name="nodeId"></param>
    /// <returns></returns>
    private GameObject GetPrefabByNodeId(int nodeId)
    {
        if(nodeId >= 0 && nodeId < nodeDataList.Count)
        {
            return nodeDataList[nodeId].nodePrefab;
        }

        Debug.LogError("No Prefab specified for the given node ID");
        return null;
    }
    /// <summary>
    /// Gets the moveableObject prefab by given ID from the list
    /// </summary>
    /// <param name="moveableObjectId"></param>
    /// <returns></returns>
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
