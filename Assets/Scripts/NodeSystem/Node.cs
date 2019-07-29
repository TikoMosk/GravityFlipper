using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Level level;
    private int x;
    private int y;
    private int z;
    public int X { get => x;  }
    public int Y { get => y; }
    public int Z { get => z; }
    private int type;
    public int Type { get { return type; } set { type = value; } }

    public enum Direction { UP, DOWN , LEFT, RIGHT , FORWARD, BACK };

    private NodeObject nodeObject;
    public NodeObject NodeObject { get { return nodeObject; } set { nodeObject = value; } }

    private Action nodeTypeChanged;

    private NodeGraphic nodeGraphic;
    public NodeGraphic NodeGraphic { get => nodeGraphic; set => nodeGraphic = value; }

    public void CreateGraphic(GameObject node_go)
    {
        nodeGraphic = node_go.AddComponent<NodeGraphic>();
        node_go.transform.position = GetPosition();
        nodeGraphic.Node = this;
        nodeGraphic.onClick += OnClickNode;
    }

    private void OnClickNode(Direction dir, int button)
    {
        GameController.Game.ClickNode (this, dir, button);
    }
    public Node(Level level, int x, int y, int z)
    {
        this.level = level;
        this.x = x;
        this.y = y;
        this.z = z;
        this.type = 0;
        this.nodeObject = null;
    }
    public Node()
    {
        
    }
    public void SetNodeType(int id)
    {
        type = id;
        if(nodeTypeChanged != null)
        {
            nodeTypeChanged();
        }
    }
    public Vector3 GetPosition()
    {
        Vector3 pos = GameController.Game.levelController.transform.TransformPoint(new Vector3(x, y, z));
        return pos;
    }
    public void SubscribeToNodeTypeChanged(Action nodeTypeChanged)
    {
        this.nodeTypeChanged += nodeTypeChanged;
    }
    public bool HasSamePosition(Node a)
    {
        if(x == a.X && y == a.Y && z == a.Z)
        {
            return true;
        }
        return false;
    }
    public static Node CreatePrototype(int id)
    {
        Node n = new Node();
        n.Type = id;
        return n;
    }

}
