using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Level level;
    public Level Level { get => level; set => level = value; }
    private static LevelController _instance;
    public static LevelController Instance { get { return _instance; } }


    public List<NodeData> nodeDataList = new List<NodeData>();
    public List<NodeData> moveableObjectDataList = new List<NodeData>();
    [System.Serializable]
    public struct NodeData
    {
        public int id;
        public GameObject nodePrefab;
    }
    public int worldWidth;
    public int worldHeight;
    public int worldLength;
    private Node playerNode;

    private void Start()
    {
        CreateLevel();
        CreateLevelGraphics();
    }
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
    private void CreateLevel()
    {
        Level = new Level(worldWidth, worldHeight, worldLength);
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                for (int z = 0; z < worldLength; z++)
                {
                    if(x == 0 || y == 0 || z == worldLength - 1)
                    {
                        Level.SetNode(x, y, z, 1);
                    }
                }
            }
        }
        PlacePlayer(2, 1, 2);
    }
    private void PlacePlayer(int x, int y, int z)
    {
        Level.AddMoveableObject(x, y, z, new MoveableObject(1));
        playerNode = Level.GetNode(x, y, z);
    }
    public void MovePlayer(Node dest)
    {
        playerNode.MoveObjectTo(dest);
        playerNode = dest;
    }
    public void MovePlayer(int x, int y, int z)
    {
        Node destNode = Level.GetNode(playerNode.X + x, playerNode.Y + y, playerNode.Z + z);
        if(destNode != null)
        {
            playerNode.MoveObjectTo(destNode);
            playerNode = destNode;
        }
        

    }
    public Node GetPlayerNode()
    {
        return playerNode;
    }
    private void CreateLevelGraphics()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                for (int z = 0; z < worldLength; z++)
                {
                    CreateNodeGraphics(x, y, z);
                    CreateMoveableObjectGraphics(x, y, z);
                }
            }
        }
    }
    private void CreateNodeGraphics(int x, int y, int z)
    {
        if (GetPrefabByNodeId(Level.GetNode(x, y, z).Type) != null)
        {
            GameObject node_go = Instantiate(GetPrefabByNodeId(Level.GetNode(x, y, z).Type), Level.GetNode(x, y, z).GetPosition(), Quaternion.identity);
            Level.GetNode(x, y, z).CreateGraphic(node_go);
            Level.GetNode(x, y, z).NodeGraphic.transform.parent = this.transform;
        }
    }
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
    private GameObject GetPrefabByNodeId(int nodeId)
    {
        if(nodeId >= 0 && nodeId < nodeDataList.Count)
        {
            return nodeDataList[nodeId].nodePrefab;
        }

        Debug.LogError("No Prefab specified for the given node ID");
        return null;
    }
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
