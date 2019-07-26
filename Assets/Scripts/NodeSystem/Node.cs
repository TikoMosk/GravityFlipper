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

    private MoveableObject moveableObject;
    public MoveableObject MoveableObject { get { return moveableObject; } set { moveableObject = value; } }
    private Action nodeTypeChanged;

    private NodeGraphic nodeGraphic;
    public NodeGraphic NodeGraphic { get => nodeGraphic; set => nodeGraphic = value; }

    public void CreateGraphic(GameObject node_go)
    {
        NodeGraphic = node_go.AddComponent<NodeGraphic>();
        node_go.transform.position = GetPosition();
        NodeGraphic.onClick += OnClickNode;
    }

    private void OnClickNode(Direction dir)
    {
        GameController.Game.OnClick(this, dir);
    }
    public Node(Level level, int x, int y, int z)
    {
        this.level = level;
        this.x = x;
        this.y = y;
        this.z = z;
        this.type = 0;
        this.moveableObject = null;
    }
    public Node(Level level, int x, int y, int z, int type, MoveableObject moveAbleObject)
    {
        this.level = level;
        this.x = x;
        this.y = y;
        this.z = z;
        this.type = type;
        this.moveableObject = moveAbleObject;
    }
    public void SetNodeType(int id)
    {
        type = id;
        if(nodeTypeChanged != null)
        {
            nodeTypeChanged();
        }
    }
    public void MoveObjectTo(Node destination)
    {
        if (moveableObject.objectMoved != null)
        {
            moveableObject.objectMoved(destination);
        }
        if(destination != this)
        {
            destination.moveableObject = this.moveableObject;
            this.moveableObject = null;
        }
        
    }
    public Vector3 GetPosition()
    {
        Vector3 pos = GameController.Game.levelController.transform.TransformPoint(new Vector3(x, y, z));
        return pos;
        
    }
    public Vector3 GetNodePosition()
    {
        Vector3 pos = new Vector3(x, y, z);
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

}
