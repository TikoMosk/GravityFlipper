using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    private Level level;
    public Level Level { get => level; set => level = value; }
    public NodeFactory Factory { get => factory; set => factory = value; }

    public LevelSerializer levelSerializer;
    private Action onLevelCreated;
    NodeFactory factory;
    int width = 10;
    int height = 10;
    int length = 10;

    [System.Serializable]
    public struct NodeData {
        public int id;
        public GameObject nodePrefab;
    }
    private void Awake() {
        Factory = FindObjectOfType<NodeFactory>();
    }

    public void ResizeLevel(int width, int height, int length) {
        /*this.width = width;
        this.height = height;
        this.length = length;*/
    }
    public void SaveLevelLocal() {
        levelSerializer.SaveLevelLocal("savedLevel.json", level);
    }

    public void LoadLevelFromProject(string levelName) {
        //level = levelSerializer.LoadLevelLocal(Application.streamingAssetsPath + "/level1");
        LaunchLevel(levelSerializer.LoadLevelLocal(levelName));

    }
    public void LoadLevelFromServer(int levelId) {
        LaunchLevel(levelSerializer.LoadLevelFromServer(levelId));
    }
    public void LaunchLevel(Level level) {
        this.level = level;
        DestroyLevelGraphics();
        CreateLevelGraphics();
        onLevelCreated.Invoke();
        SetUpNodeConnections();

        
        GameController.Game.CameraController.ResetCamera();
    }

    public void BuildEmptyLevel() {
        level = new Level(width, height, length);
        level.InitializeLevel();
        int minSize = 5;
        if(width < minSize || height < minSize || length < minSize) {
            Debug.Log("LEVEL SIZE TOO SMALL");
            return;
        }
        int centerX = width / 2;
        int centerY = height / 2;
        int centerZ = length / 2;
        for (int x = centerX - minSize / 2; x < centerX + minSize/2 + 1; x++) {
            for (int y = 0; y < height; y++) {
                for (int z = centerZ - minSize / 2; z < centerZ + minSize / 2 + 1; z++) {
                    if (y == 0) {
                        level.SetNode(x, y, z, 1);
                    }
                }
            }
        }
        level.AddNodeMember(5, 1, 5, 1, Node.Direction.FORWARD, Node.Direction.UP);
        level.SetNode(5, 5, 5, Factory.GetIdByName("Light Source"));
        DestroyLevelGraphics();
        CreateLevelGraphics();
        onLevelCreated.Invoke();

    }
    // Destroys the level graphics (this is called when a new level is loaded to remove the old level graphics)
    private void DestroyLevelGraphics() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);

        }
    }

    // Creates the Level Graphics
    private void CreateLevelGraphics() {
        for (int x = 0; x < level.Width; x++) {
            for (int y = 0; y < level.Height; y++) {
                for (int z = 0; z < level.Length; z++) {
                    CreateNodeGraphics(x, y, z);
                    CreateNodeMemberGraphic(x, y, z);
                }
            }
        }
    }

    //Sets the NodeConnections 
    private void SetUpNodeConnections() {
        for (int i = 0; i < level.NodeConnections.Count; i++) {
            NodeConnection.NodeCoordinate tCoord = level.NodeConnections[i].toggler;
            NodeConnection.NodeCoordinate rCoord = level.NodeConnections[i].receiver;
            NodeToggler t = GetToggler(tCoord.x, tCoord.y, tCoord.z);
            if(t != null) {
               t.ConnectNode(level.GetNode(rCoord.x, rCoord.y, rCoord.z));
            }
            level.NodeTogglers.Add(t);
        }
    }
    private NodeToggler GetToggler(int x, int y, int z) {
        Node n = level.GetNode(x, y, z);
        NodeToggler t = null;
        if (n.NodeGraphic != null) {
            if (n.NodeGraphic.GetComponent<NodeToggler>() != null) {
                t = n.NodeGraphic.GetComponent<NodeToggler>();
            }
        }
        else if (n.NodeMember != null) {
            if (n.NodeMember.NodeObjectGraphic.GetComponent<NodeToggler>() != null) {
                t = n.NodeMember.NodeObjectGraphic.GetComponent<NodeToggler>();
            }
        }
        return t;
    }
    // Creates the NodeGraphic for the node at x,y,z
    private void CreateNodeGraphics(int x, int y, int z) {
        NodeDetails nodeDetail = Factory.GetNodeDetailsById(level.GetNode(x, y, z).Id, false);
        Level.GetNode(x, y, z).Walkable = nodeDetail.walkable;
        Level.GetNode(x, y, z).CanWalkOnIt = nodeDetail.canWalkOnIt;
        if (Factory.GetNodePrefabById(Level.GetNode(x, y, z).Id) != null) {
            Level.GetNode(x, y, z).ColliderActive = nodeDetail.colliderActive;
            Quaternion nodeRotation = Quaternion.LookRotation(Dir.GetVectorByDirection(level.GetNode(x, y, z).Facing), Dir.GetVectorByDirection(level.GetNode(x, y, z).UpDirection));
            GameObject node_go = Instantiate(Factory.GetNodePrefabById(Level.GetNode(x, y, z).Id), Level.GetNode(x, y, z).GetPosition(), nodeRotation);
            Level.GetNode(x, y, z).CreateGraphic(node_go);
            Level.GetNode(x, y, z).NodeGraphic.transform.parent = this.transform;
            Level.GetNode(x, y, z).NodeGraphic.Node = Level.GetNode(x, y, z);
            Level.GetNode(x, y, z).SubscribeToNodeTypeChanged(() => { OnNodeTypeChanged(level.GetNode(x, y, z), node_go); });
            Level.GetNode(x, y, z).SubscribeToNodeRotated(() => { OnNodeRotated(level.GetNode(x, y, z), node_go); });
        }
        else {
            Level.GetNode(x, y, z).SubscribeToNodeTypeChanged(() => { OnNodeTypeChanged(level.GetNode(x, y, z), null); });
        }

    }

    // Creates the moveableObject graphic for the node at x,y,z
    public void CreateNodeMemberGraphic(int x, int y, int z) {
        if (Level.GetNode(x, y, z).NodeMember != null) {
            if (Factory.GetNodeMemberPrefabById(Level.GetNode(x, y, z).NodeMember.Id) != null) {

                NodeMember nodeObject = Level.GetNode(x, y, z).NodeMember;
                GameObject nodeObject_GameObject = Instantiate(Factory.GetNodeMemberPrefabById(nodeObject.Id), Level.GetNode(x, y, z).GetPosition(), Quaternion.identity);
                Level.GetNode(x, y, z).NodeMember.ColliderActive = Factory.GetNodeDetailsById(level.GetNode(x, y, z).NodeMember.Id, true).colliderActive;


                nodeObject_GameObject.transform.parent = this.transform;
                NodeMemberGraphic nodeObjectGraphic = nodeObject.CreateMoveableObjectGraphic(nodeObject_GameObject);
                nodeObject.NodeObjectGraphic.Node = Level.GetNode(x, y, z);
                nodeObject.NodeObjectGraphic.transform.rotation = Quaternion.LookRotation(Dir.GetVectorByDirection(nodeObject.Facing), Dir.GetVectorByDirection(nodeObject.UpDirection));
                nodeObject.LocationNode = Level.GetNode(x, y, z);
                nodeObject.SubscribeToMoveableObjectMoved((node) => { OnNodeMemberMoved(node, nodeObjectGraphic); });
                if (Level.GetNode(x, y, z).NodeMember.Id == 1) {
                    Level.Player = level.GetNode(x, y, z).NodeMember.NodeObjectGraphic.GetComponent<Player>();
                }
            }
        }
    }
    private void OnNodeMemberMoved(Node dest, NodeMemberGraphic nodeMemberGraphic) {
        Quaternion nodeMemberRotation = Quaternion.LookRotation(Dir.GetVectorByDirection(dest.NodeMember.Facing), Dir.GetVectorByDirection(dest.NodeMember.UpDirection));
        //nodeMemberGraphic.transform.rotation = nodeMemberRotation;
        //nodeMemberGraphic.transform.position = dest.GetPosition();
        nodeMemberGraphic.Node = dest;
        dest.NodeMember.LocationNode = dest;
        StartCoroutine(GameController.Game.SmoothGraphics.RotateSmoothly(nodeMemberGraphic.transform, nodeMemberRotation, 0.5f));
        StartCoroutine(GameController.Game.SmoothGraphics.MoveSmoothly(nodeMemberGraphic.transform, dest.GetPosition(), 0.5f, nodeMemberGraphic));
       
    }

    private void OnNodeTypeChanged(Node n, GameObject node_go) {
        if (n.Id == 0) {
            if (node_go != null) {
                Destroy(node_go);
            }

        }
        else {
            n.ResetNodeTypeChanged();
            n.ResetNodeRotated();
            CreateNodeGraphics(n.X, n.Y, n.Z);
        }
    }
    private void OnNodeRotated(Node n, GameObject node_go) {
        Quaternion nodeRot = Quaternion.LookRotation(Dir.GetVectorByDirection(n.Facing), Dir.GetVectorByDirection(n.UpDirection));
        StartCoroutine(GameController.Game.SmoothGraphics.RotateSmoothly(node_go.transform, nodeRot, 0.5f));
    }



    public void RegisterToLevelCreated(Action onLevelCreated) {
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
