using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMember
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
    private Action nodeObjectDestroyed;
    public NodeMemberGraphic NodeObjectGraphic { get => nodeObjectGraphic; set => nodeObjectGraphic = value; }
    public Action<Node> NodeObjectMoved { get => nodeObjectMoved; set => nodeObjectMoved = value; }

    private NodeMemberGraphic nodeObjectGraphic;
    private bool walkthrough;

    protected Node.Direction facing = Node.Direction.FORWARD;
    protected Node.Direction upDirection = Node.Direction.UP;

    private Node locationNode;

    public Node.Direction Facing { get => facing; set { facing = value; } }
    public Node.Direction UpDirection { get => upDirection; set { upDirection = value; } }
    public Node LocationNode { get => locationNode; set => locationNode = value; }
    public bool Walkthrough { get => walkthrough; set => walkthrough = value; }

    public NodeMember(int id)
    {
        this.id = id;
    }
    public void Destroy() {
        if (nodeObjectDestroyed != null) {
            nodeObjectDestroyed.Invoke();
        }
        locationNode.DestroyNodeMember();
        
        
    }
    public void SetRotation(Node.Direction forward, Node.Direction up) {
        facing = forward;
        upDirection = up;
        if(nodeObjectMoved != null) {
            nodeObjectMoved.Invoke(locationNode);
        }
       

    }

    public void SetPosition(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void SubscribeToMoveableObjectMoved(Action<Node> objectMoved)
    {
        NodeObjectMoved += objectMoved;
    }
    public void SubscribeToNodeObjectDestroyed(Action dead) {
        nodeObjectDestroyed += dead;
    }

    public NodeMemberGraphic CreateMoveableObjectGraphic(GameObject nodeObject_GameObject)
    {
        nodeObjectGraphic = nodeObject_GameObject.AddComponent<NodeMemberGraphic>();
        nodeObjectGraphic.RegisterToClick(OnClickNode);
        return nodeObjectGraphic;
    }
    private void OnClickNode(Node.Direction dir) {
        GameController.Game.ClickNode(locationNode, dir);
    }
}
