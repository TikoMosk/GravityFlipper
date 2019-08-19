using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    private Level level;
    public Level Level { get => level; set => level = value; }

    public LevelSerializer levelSerializer;
    private Action onLevelCreated;
    int width = 20;
    int height = 20 ;
    int length = 20;

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
            SaveLevelLocal();
        }
        
    }
    public void SaveLevelLocal() {
        levelSerializer.SaveLevelLocal("level1.json", level);
    }
    
    public void LoadLevelFromProject(string levelName) {
        //level = levelSerializer.LoadLevelLocal(Application.streamingAssetsPath + "/level1");
        
        level = levelSerializer.LoadLevelLocal(levelName);
        DestroyLevelGraphics();
        CreateLevelGraphics();
        onLevelCreated.Invoke();
    }
    public void BuildEmptyLevel() {
        level = new Level(width, height, length);
        level.InitializeLevel();
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                for (int z = 0; z < length; z++) {
                    if(y == 0) {
                        level.SetNode(x, y, z, 1);
                    }
                }
            }
        }
        level.AddNodeMember(5, 1, 5, 1,Node.Direction.FORWARD,Node.Direction.UP);
        level.AddNodeMember(5, 5, 5, 2, Node.Direction.FORWARD, Node.Direction.UP);
        DestroyLevelGraphics();
        CreateLevelGraphics();
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
        if (NodeFactory.Factory.GetNodePrefabById(Level.GetNode(x, y, z).Id) != null)
        {
            Quaternion nodeRotation = Quaternion.LookRotation(Dir.GetVectorByDirection(level.GetNode(x, y, z).Facing), Dir.GetVectorByDirection(level.GetNode(x, y, z).UpDirection));
            GameObject node_go = Instantiate(NodeFactory.Factory.GetNodePrefabById(Level.GetNode(x, y, z).Id), Level.GetNode(x, y, z).GetPosition(), nodeRotation);
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
    public void CreateNodeMemberGraphic(int x, int y, int z)
    {
        if (Level.GetNode(x, y, z).NodeMember != null)
        {
            if (NodeFactory.Factory.GetNodeMemberPrefabById(Level.GetNode(x, y, z).NodeMember.Id) != null)
            {
                
                NodeMember nodeObject = Level.GetNode(x, y, z).NodeMember;
                GameObject nodeObject_GameObject = Instantiate(NodeFactory.Factory.GetNodeMemberPrefabById(nodeObject.Id), Level.GetNode(x,y,z).GetPosition(), Quaternion.identity);
                nodeObject_GameObject.transform.LookAt(nodeObject_GameObject.transform.position + Dir.GetVectorByDirection(nodeObject.Facing));
                nodeObject_GameObject.transform.parent = this.transform;
                NodeMemberGraphic nodeObjectGraphic= nodeObject.CreateMoveableObjectGraphic(nodeObject_GameObject);
                nodeObject.NodeObjectGraphic.Node = Level.GetNode(x,y,z);
                nodeObject.LocationNode = Level.GetNode(x, y, z);
                nodeObject.SubscribeToMoveableObjectMoved((node) => { OnNodeMemberMoved(node, nodeObjectGraphic); });
                if (Level.GetNode(x, y, z).NodeMember.Id == 1) {
                    Level.Player = level.GetNode(x, y, z).NodeMember.NodeObjectGraphic.GetComponent<Player>();
                }
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
