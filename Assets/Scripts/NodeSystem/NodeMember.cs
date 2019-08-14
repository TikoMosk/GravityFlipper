using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeMember
{
    protected int x;
    protected int y;
    protected int z;
    public int X { get => x; }
    public int Y { get => y; }
    public int Z { get => z; }
    private int id;
    public int Id { get => id; set => id = value; }

    private Action<Node> nodeObjectMoved;
    public NodeMemberGraphic NodeObjectGraphic { get => nodeObjectGraphic; set => nodeObjectGraphic = value; }
    public Action<Node> NodeObjectMoved { get => nodeObjectMoved; set => nodeObjectMoved = value; }

    private NodeMemberGraphic nodeObjectGraphic;
    protected Node.Direction facing = Node.Direction.FORWARD;
    protected Node.Direction upDirection = Node.Direction.UP;
    public Node.Direction Facing { get { return facing; } set => facing = value; }
    public Node.Direction UpDirection { get => upDirection; set => upDirection = value; }

    public NodeMember(int id)
    {
        this.id = id;
    }
    public void SetPosition(int x, int y, int z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public void SubscribeToMoveableObjectMoved(Action<Node> objectMoved)
    {
        NodeObjectMoved += objectMoved;
    }

    public NodeMemberGraphic CreateMoveableObjectGraphic(GameObject nodeObject_GameObject)
    {
        nodeObjectGraphic = nodeObject_GameObject.AddComponent<NodeMemberGraphic>();
        return nodeObjectGraphic;
    }
}
