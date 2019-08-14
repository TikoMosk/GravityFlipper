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
    public Node[,,] NodeMap { get => nodeMap;}
    public Player Player { get => player; }



    private Node[,,] nodeMap;
    private int width;
    private int height;
    private int length;
    private Node startNode;
    private Node exitNode;
    private Player player;
    private List<NodeMember> enemies;

    public Level(int width,int height, int length)
    {
        this.width = width;
        this.height = height;
        this.length = length;
    }
    public void InitializeLevel() {
        nodeMap = new Node[width, length, height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                for (int z = 0; z < length; z++) {
                    Node n = new Node(this, x, y, z, 0);
                    nodeMap[x, y, z] = n;
                }
            }
        }
    }

    public bool AddNodeMember(int x, int y, int z, NodeMember nodeMember, Node.Direction facing, Node.Direction upDirection)
    {
        if(IsInLevelBounds(x,y,z))
        {
            if(nodeMap[x,y,z].NodeMember == null)
            {
                nodeMap[x, y, z].SetNodeMember(nodeMember);
                nodeMember.Facing = facing;
                nodeMember.UpDirection = upDirection;
                if(nodeMember.Id == 1) {
                    player = (Player)nodeMember;
                }
                return true;
            }
        }
        return false;
    }

    public void MoveObject(Node node, Node dest)
    {
        if(!node.HasSamePosition(dest) && dest.NodeMember == null)
        {
            dest.NodeMember = node.NodeMember;
            dest.NodeMember.SetPosition(dest.X,dest.Y,dest.Z);
            node.NodeMember = null;
            dest.NodeMember.NodeObjectMoved(dest);
            
        }
        
    }

    public void SetNode(int x, int y, int z, int type)
    {
        if(IsInLevelBounds(x,y,z))
        {
            nodeMap[x, y, z].SetNodeType(type);
        }
        else
        {
            Debug.LogError("Trying to set a Node that is out of level bounds");
        }
    }
    public void SetNode(int x, int y, int z, int type, Node.Direction facing, Node.Direction up) {
        if (IsInLevelBounds(x, y, z)) {
            nodeMap[x, y, z].SetNodeType(type);
            nodeMap[x, y, z].SetRotation(facing, up);
        }
        else {
            Debug.LogError("Trying to set a Node that is out of level bounds");
        }
    }



    public Node GetNode(int x, int y, int z)
    {
        if (IsInLevelBounds(x, y, z))
        {
            return nodeMap[x, y, z];
        }
        else
        {
            Debug.Log("Trying to get a Node that is out of level bounds" + x + " : " + y + " : " + z);
            return nodeMap[0,0,0];
        }
    }

    public int GetNodeDistance(Node a, Node b)
    {
        int distance = Mathf.Abs(b.X - a.X) + Mathf.Abs(b.Y - a.Y) + Mathf.Abs(b.Z - a.Z);
        return distance;
    }
    
    private bool IsInLevelBounds(int x, int y, int z)
    {
        if(x >= 0 && x < width && y >= 0 && y < height && z >= 0 && z < length)
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
        else return n;
    }
    
    public void PlayerMoved() {
        GameController.Game.NextTurn();
    }


}
