using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Level level;
    public Level Level { get => level; set => level = value; }

    public LevelSerializer levelSerializer;
    public List<NodeData> nodeDataList;
    public List<NodeData> nodeObjectDataList;
    private Dictionary<string, Node> nodePrototypes;
    private Dictionary<string, NodeMember> nodeObjectPrototypes;
    private Action onLevelCreated;

    [System.Serializable]
    public struct NodeData
    {
        public int id;
        public GameObject nodePrefab;
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            levelSerializer.SaveLevelLocal(level);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            level = levelSerializer.LoadLevelLocal(Application.persistentDataPath + "/level1");
            DestroyLevelGraphics();
            CreateLevelGraphics();
            onLevelCreated.Invoke();
        }
        
    }
    public void BuildTestLevel() {
        //level = levelSerializer.LoadLevelLocal(Application.streamingAssetsPath + "/level1");

        level = new Level(10, 10, 10);
        level.InitializeLevel();
        NodeMemberFactory fac = new NodeMemberFactory();
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                for (int z = 0; z < 10; z++) {
                    if((x == 0 || y == 0 || z == 9) && y <= 2) {
                        level.SetNode(x, y, z, 1);
                    }
                    if(x == 3 && y < 2 && z <= 4 && z >= 2) {
                        level.SetNode(x, y, z, 1);
                    }
                    if (x == 6 && y <= 8 && z <= 6 && z >= 5) {
                        level.SetNode(x, y, z, 1);
                    }
                }
            }
        }
        level.AddNodeMember(5, 1, 5, fac.CreateNodeMember(1));
        level.AddNodeMember(2, 1, 3, fac.CreateNodeMember(2));
        DestroyLevelGraphics();
        CreateLevelGraphics();
        Debug.Log("A");
        onLevelCreated.Invoke();

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
                    CreateNodeMemberGraphic(x, y, z);
                }
            }
        }
    }
    // Creates the NodeGraphic for the node at x,y,z
    private void CreateNodeGraphics(int x, int y, int z)
    {
        if (GetPrefabByNodeId(Level.GetNode(x, y, z).Id) != null)
        {
            GameObject node_go = Instantiate(GetPrefabByNodeId(Level.GetNode(x, y, z).Id), Level.GetNode(x, y, z).GetPosition(), transform.rotation);
            Level.GetNode(x, y, z).CreateGraphic(node_go);
            Level.GetNode(x, y, z).NodeGraphic.transform.parent = this.transform;
            Level.GetNode(x, y, z).SubscribeToNodeTypeChanged(() => { OnNodeTypeChanged(level.GetNode(x, y, z),node_go); });

        }
        else
        {
            Level.GetNode(x, y, z).SubscribeToNodeTypeChanged(() => { OnNodeTypeChanged(level.GetNode(x, y, z), null); });
        }
        
    }

    // Creates the moveableObject graphic for the node at x,y,z
    private void CreateNodeMemberGraphic(int x, int y, int z)
    {
        if (Level.GetNode(x, y, z).NodeMember != null)
        {
            if (GetPrefabByNodeMemberId(Level.GetNode(x, y, z).NodeMember.Id))
            {
                
                NodeMember nodeObject = Level.GetNode(x, y, z).NodeMember;
                GameObject nodeObject_GameObject = Instantiate(GetPrefabByNodeMemberId(nodeObject.Id), Level.GetNode(x,y,z).GetPosition(), Quaternion.identity);
                nodeObject_GameObject.transform.LookAt(nodeObject_GameObject.transform.position + Dir.GetVectorByDirection(nodeObject.Facing));
                nodeObject_GameObject.transform.parent = this.transform;
                NodeMemberGraphic nodeObjectGraphic= nodeObject.CreateMoveableObjectGraphic(nodeObject_GameObject);
                nodeObject.NodeObjectGraphic.Node = Level.GetNode(x,y,z);
                nodeObject.SubscribeToMoveableObjectMoved((node) => { OnNodeMemberMoved(node, nodeObjectGraphic); });
            }
        }
    }
    private void OnNodeMemberMoved(Node dest, NodeMemberGraphic nodeMemberGraphic)
    {
        Quaternion nodeMemberRotation = Quaternion.LookRotation(Dir.GetVectorByDirection(dest.NodeMember.Facing), Dir.GetVectorByDirection(dest.NodeMember.UpDirection));
       // nodeObjectGraphic.transform.rotation = nodeMemberRotation;
        StartCoroutine(GameController.Game.SmoothGraphics.RotateSmoothly(nodeMemberGraphic.transform, nodeMemberRotation, 0.5f));
        StartCoroutine(GameController.Game.SmoothGraphics.MoveSmoothly(nodeMemberGraphic.transform, dest.GetPosition(),0.5f, nodeMemberGraphic));
        nodeMemberGraphic.Node = dest;
    }

    private void OnNodeTypeChanged(Node n, GameObject node_go)
    {
        if(n.Id == 0 ) {
            Destroy(node_go);
        }
        else {
            n.ResetNodeTypeChanged();
            CreateNodeGraphics(n.X, n.Y, n.Z);
        }
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

    private GameObject GetPrefabByNodeMemberId(int nodeObjectId)
    {
        if (nodeObjectId >= 0 && nodeObjectId < nodeObjectDataList.Count)
        {
            return nodeObjectDataList[nodeObjectId].nodePrefab;
        }

        Debug.LogError("No Prefab specified for the given moveableObject ID");
        return null;
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
