using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Level level;
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
    private void Update()
    {
        
    }
    private void CreateLevel()
    {
        level = new Level(worldWidth, worldHeight, worldLength);
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                for (int z = 0; z < worldLength; z++)
                {
                    if(x == 0 || y == 0 || z == worldLength - 1)
                    {
                        level.SetNode(x, y, z, 1);
                    }
                }
            }
        }
        PlacePlayer(2, 1, 2);
    }
    private void PlacePlayer(int x, int y, int z)
    {
        level.AddMoveableObject(x, y, z, MoveableObject.CreateMoveableObject(1));
        playerNode = level.GetNode(x, y, z);
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
        if (GetPrefabByNodeId(level.GetNode(x, y, z).Type) != null)
        {
            GameObject moveable_go = Instantiate(GetPrefabByNodeId(level.GetNode(x, y, z).Type), new Vector3(x, y, z), Quaternion.identity);
            moveable_go.transform.parent = this.transform;
        }
    }
    private void CreateMoveableObjectGraphics(int x, int y, int z)
    {
        if (level.GetNode(x, y, z).MoveableObject != null)
        {
            if (GetPrefabByMoveableObjectId(level.GetNode(x, y, z).MoveableObject.Id))
            {
                MoveableObject moveable = level.GetNode(x, y, z).MoveableObject;
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
