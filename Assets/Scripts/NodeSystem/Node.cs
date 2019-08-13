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
    private int id;
    public int Id { get { return id; } set { id = value; } }
    private Direction facing = Node.Direction.FORWARD;
    private Direction upDirection = Node.Direction.UP;
    public Direction Facing { get { return facing; } }
    public Direction UpDirection { get { return upDirection; } }

    public enum Direction { UP, FORWARD, DOWN , LEFT, RIGHT , BACK };

    private NodeMember nodeMember;
    public NodeMember NodeMember { get { return nodeMember; } set { nodeMember = value; } }

    private Action nodeTypeChanged;

    private NodeGraphic nodeGraphic;
    public NodeGraphic NodeGraphic { get => nodeGraphic; set => nodeGraphic = value; }

    public Node(Level level, int x, int y, int z, int type) {
        this.level = level;
        this.x = x;
        this.y = y;
        this.z = z;
        this.id = 0;
        this.nodeMember = null;
    }
    public Node(int id) {
        this.id = id;
    }
    public void SetRotation(Node.Direction forward, Node.Direction up) {
        facing = forward;
        upDirection = up;
    }
    public void SetNodeMember(NodeMember nodeMember) {
        this.nodeMember = nodeMember;
        this.nodeMember.SetPosition(x, y, z);
    }
    public void CreateGraphic(GameObject node_go)
    {
        nodeGraphic = node_go.AddComponent<NodeGraphic>();
        node_go.transform.position = GetPosition();
        nodeGraphic.Node = this;
        nodeGraphic.onClick += OnClickNode;
    }

    private void OnClickNode(Direction dir)
    {
        GameController.Game.ClickNode (this, dir);
    }
    public void SetNodeType(int id)
    {
        this.id = id;
        if(nodeTypeChanged != null)
        {
            nodeTypeChanged();
        }
    }
    public Vector3 GetPosition()
    {
        Vector3 pos = GameController.Game.LevelController.transform.TransformPoint(new Vector3(x, y, z));
        return pos;
    }
    public void SubscribeToNodeTypeChanged(Action nodeTypeChanged)
    {
        this.nodeTypeChanged += nodeTypeChanged;
    }
    public void ResetNodeTypeChanged() {
        if(this.nodeTypeChanged != null) {
            this.nodeTypeChanged = null;
        }
        
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
