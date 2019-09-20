using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int Width { get { return width; } }
    public int Height { get { return height; } }
    public int Length { get { return length; } }
    public Node[,,] NodeMap { get => nodeMap; }
    public Player Player { get => player; set => player = value; }
    public List<NodeToggler> NodeTogglers { get => nodeTogglers;}
    public List<NodeConnection> NodeConnections { get => nodeConnections; set => nodeConnections = value; }

    private Node[,,] nodeMap;
    private int width;
    private int height;
    private int length;
    private Node startNode;
    private Node exitNode;
    private Player player;
    private List<NodeMember> enemies;
    private List<NodeToggler> nodeTogglers;
    private List<NodeConnection> nodeConnections;

    public Level(int width, int height, int length)
    {
        this.width = width;
        this.height = height;
        this.length = length;
    }
    public void ResizeLevel(int width, int height, int length) {
        this.width = width;
        this.height = height;
        this.length = length;
        Node[,,] newMap = new Node[width, height, length];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                for (int z = 0; z < length; z++) {
                    if (x < nodeMap.GetLength(0) && y < nodeMap.GetLength(1) && z < nodeMap.GetLength(2)) {
                        newMap[x, y, z] = nodeMap[x, y, z];
                    }
                    else {
                        newMap[x, y, z] = new Node(this, x, y, z, 0);
                    }
                    
                }
            }
        }
        
        nodeMap = newMap;
        for (int i = 0; i < nodeTogglers.Count; i++) {
            if (GetNode(nodeTogglers[i].GetPos()) == null || GetNode(nodeTogglers[i].GetConnectNodePosition()) == null) {
                nodeTogglers.Remove(nodeTogglers[i]);
            }
        }
        for (int i = 0; i < nodeConnections.Count; i++) {
            if(GetNode(nodeConnections[i].receiver.x, nodeConnections[i].receiver.y, nodeConnections[i].receiver.z) == null || GetNode(nodeConnections[i].toggler.x, nodeConnections[i].toggler.y, nodeConnections[i].toggler.z) == null) {
                nodeConnections.Remove(nodeConnections[i]);
            }
        }
        GameController.Game.LevelController.LaunchLevel(this);
    }
    public void InitializeLevel()
    {
        nodeTogglers = new List<NodeToggler>();
        nodeConnections = new List<NodeConnection>();
        nodeMap = new Node[width, height, length];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < length; z++)
                {
                    Node n = new Node(this, x, y, z, 0);
                    nodeMap[x, y, z] = n;
                }
            }
        }
    }
    public void AddNodeConnection(NodeToggler toggler) {

        NodeTogglers.Add(toggler);
    }
    public void ResetNodeConnections() {
        nodeTogglers = null;
    }
    public void RemoveNodeConnection(NodeToggler toggler) {
        if(NodeTogglers.Contains(toggler)) {
            NodeTogglers.Remove(toggler);
        }
    }

    public bool AddNodeMember(int x, int y, int z, int id, Node.Direction facing, Node.Direction upDirection)
    {
        if (IsInLevelBounds(x, y, z))
        {
            if (nodeMap[x, y, z].NodeMember == null)
            {
                NodeMember nodeMember = new NodeMember(id);
                nodeMap[x, y, z].SetNodeMember(nodeMember);
                nodeMember.Facing = facing;
                nodeMember.UpDirection = upDirection;
                return true;
            }
        }
        return false;
    }

    public void MoveObject(Node node, Node dest)
    {
        if (dest.Walkable) {
            MoveMemberLogically(node, dest);
            dest.NodeMember.NodeObjectMoved(dest);
        }
    }
    public void MoveMemberLogically(Node node, Node dest) {
        if (!node.HasSamePosition(dest) && dest.NodeMember == null) {
            dest.NodeMember = node.NodeMember;
            dest.NodeMember.SetPosition(dest.X, dest.Y, dest.Z);
            node.NodeMember = null;
            dest.NodeMember.NodeObjectGraphic.Node = dest;
        }
    }
    public void MoveMemberNoAnimation(Node node, Node dest) {
        MoveMemberLogically(node, dest);
        dest.NodeMember.NodeObjectGraphic.transform.position = dest.GetPosition();
    }
    public bool CanMoveObject(Node node, Node dest)
    {
        if (node.HasSamePosition(dest))
        {
            return true;
        }
        else if (!node.HasSamePosition(dest) && dest.NodeMember == null)
        {
            return true;
        }
        return false;
    }

    public void SetNode(int x, int y, int z, int type)
    {
        if (IsInLevelBounds(x, y, z))
        {
            nodeMap[x, y, z].SetNodeType(type);
        }
        else
        {
            Debug.LogError("Trying to set a Node that is out of level bounds");
        }
    }
    public void SetNode(int x, int y, int z, int type, Node.Direction facing, Node.Direction up)
    {
        if (IsInLevelBounds(x, y, z))
        {
            nodeMap[x, y, z].SetNodeType(type);
            nodeMap[x, y, z].SetRotation(facing, up);
        }
        else
        {
            Debug.LogError("Trying to set a Node that is out of level bounds");
        }
    }

    public void GetNeighbourNodes(Node n, Node.Direction memberFacing, Node.Direction memberUp)
    {
        List<Node> neighbours = new List<Node>();
    }



    public Node GetNode(int x, int y, int z)
    {

        if (IsInLevelBounds(x, y, z))
        {
            return nodeMap[x, y, z];
        }
        else
        {
            Debug.Log("Trying to get a Node that is out of level bounds");
            return null;
        }
    }
    public Node GetNode(Vector3 pos)
    {
        return GetNode(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
    }

    public int GetNodeDistance(Node a, Node b)
    {
        int distance = Mathf.Abs(b.X - a.X) + Mathf.Abs(b.Y - a.Y) + Mathf.Abs(b.Z - a.Z);
        return distance;
    }
    public void DestroyNodeMember(Node a) {
        a.NodeMember = null;
    }

    private bool IsInLevelBounds(int x, int y, int z)
    {
        if (x >= 0 && x < width && y >= 0 && y < height && z >= 0 && z < length)
        {
            return true;
        }
        return false;
    }

    public Node GetNodeInTheDirection(Node n, Node.Direction dir)
    {
        if (dir == Node.Direction.RIGHT) return GetNode(n.X + 1, n.Y, n.Z);
        else if (dir == Node.Direction.LEFT) return GetNode(n.X - 1, n.Y, n.Z);
        else if (dir == Node.Direction.UP) return GetNode(n.X, n.Y + 1, n.Z);
        else if (dir == Node.Direction.DOWN) return GetNode(n.X, n.Y - 1, n.Z);
        else if (dir == Node.Direction.FORWARD) return GetNode(n.X, n.Y, n.Z + 1);
        else if (dir == Node.Direction.BACK) return GetNode(n.X, n.Y, n.Z - 1);
        else return null;
    }

    public void PlayerMoved()
    {
        GameController.Game.NextTurn();
    }


}