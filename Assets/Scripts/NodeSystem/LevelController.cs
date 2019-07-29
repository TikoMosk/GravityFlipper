using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Level level;
    public Level Level { get => level; set => level = value; }
    private Node playerNode;
    public Node PlayerNode { get => playerNode; }

    public LevelSerializer levelSerializer;
    public List<NodeData> nodeDataList;
    public List<NodeData> nodeObjectDataList;
    private Dictionary<string, Node> nodePrototypes;
    private Dictionary<string, NodeObject> nodeObjectPrototypes;
    private Action onLevelCreated;

    private GameController gameController;
    [System.Serializable]
    public struct NodeData
    {
        public int id;
        public GameObject nodePrefab;
    }
    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }
    
    private void InitializeLevel(LevelData levelData)
    {
        level = new Level(levelData.levelWidth, levelData.levelHeight, levelData.levelLength);
        for (int x = 0; x < levelData.levelWidth; x++)
        {
            for (int y = 0; y < levelData.levelHeight; y++)
            {
                for (int z = 0; z < levelData.levelLength; z++)
                {
                    level.NodeMap[x, y, z] = new Node(level, x, y, z);
                    level.SetNode(x, y, z, levelData.nodeDataMap[x, y, z].blockId);
                    if (levelData.nodeDataMap[x, y, z].moveableId != 0)
                    {
                        level.AddMoveableObject(x, y, z, new NodeObject(levelData.nodeDataMap[x, y, z].moveableId));
                    }
                    if(levelData.nodeDataMap[x, y, z].moveableId == 1)
                    {
                        playerNode = level.GetNode(x, y, z);
                    }
                }
            }
        }
        level.RegisterToPlayerMoved(PlayerMoved);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            levelSerializer.SaveLevel(level, "level1");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            InitializeLevel(levelSerializer.LoadLevel("level1"));
            DestroyLevelGraphics();
            CreateLevelGraphics();
            onLevelCreated.Invoke();
        }
    }

    // Destroys the level graphics (this is called when a new level is loaded to remove the old level graphics)
    private void DestroyLevelGraphics()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Level")
            {
                Destroy(transform.GetChild(i).gameObject);
            }
                
        }
    }

    // Creates the Level Graphics
    private void CreateLevelGraphics()
    {
        for (int x = 0; x < level.Width; x++)
        {
            for (int y = 0; y < level.Height; y++)
            {
                for (int z = 0; z < level.Length; z++)
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
        if (Level.GetNode(x, y, z).NodeObject != null)
        {
            if (GetPrefabByMoveableObjectId(Level.GetNode(x, y, z).NodeObject.Id))
            {
                NodeObject nodeObject = Level.GetNode(x, y, z).NodeObject;
                GameObject nodeObject_GameObject = Instantiate(GetPrefabByMoveableObjectId(nodeObject.Id), Level.GetNode(x,y,z).GetPosition(), transform.rotation);
                nodeObject_GameObject.transform.parent = this.transform;
                NodeObjectGraphic nodeObjectGraphic= nodeObject.CreateMoveableObjectGraphic(nodeObject_GameObject);
                nodeObject.NodeObjectGraphic.Node = Level.GetNode(x,y,z);
                nodeObject.SubscribeToMoveableObjectMoved((node) => { OnObjectMoved(node, nodeObjectGraphic); });
            }
        }
    }
    private void OnObjectMoved(Node dest, NodeObjectGraphic nodeObjectGraphic)
    {
        nodeObjectGraphic.transform.position = dest.GetPosition();
        nodeObjectGraphic.Node = dest;
    }
    private void OnNodeTypeChanged(Node n, GameObject node_go)
    {
        Destroy(node_go);
        node_go = Instantiate(GetPrefabByNodeId(n.Type), n.GetPosition(), transform.rotation);
        n.CreateGraphic(node_go);
        n.NodeGraphic.transform.parent = this.transform;
    }
    private void OnNodeTypeChanged(Node n)
    {
       
        GameObject node_GameObject = Instantiate(GetPrefabByNodeId(n.Type), n.GetPosition(), transform.rotation);
        n.CreateGraphic(node_GameObject);
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

    private GameObject GetPrefabByMoveableObjectId(int nodeObjectId)
    {
        if (nodeObjectId >= 0 && nodeObjectId < nodeDataList.Count)
        {
            return nodeObjectDataList[nodeObjectId].nodePrefab;
        }

        Debug.LogError("No Prefab specified for the given moveableObject ID");
        return null;
    }
    private void PlayerMoved(Node playerNode)
    {
        this.playerNode = playerNode;
        GameController.Game.NextTurn();
    }

    public void RegisterToLevelCreated(Action onLevelCreated)
    {
        this.onLevelCreated += onLevelCreated;
    }

    /*private void CreateNodePrototypes()
    {
        nodePrototypes = new Dictionary<string, Node>();
        nodePrototypes.Add("Block",Node.CreatePrototype(1));
        nodePrototypes.Add("Laser", Node.CreatePrototype(2));
    }
    private void CreateNodeObjectPrototypes()
    {
        nodeObjectPrototypes = new Dictionary<string, NodeObject>();

       // nodeObjectPrototypes.Add("Player" , )
    }*/
}
